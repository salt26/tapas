using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using BeardedManStudios.Forge.Networking.Lobby;
using System;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BeardedManStudios.Forge.Networking.Unity.Lobby
{
	public class LobbyManager : MonoBehaviour, ILobbyMaster
    {
        public static LobbyManager lm;

        public GameObject PlayerItem;
		public LobbyPlayerItem Myself;
        public InputField ChatInputBox;
		public Text Chatbox;
		public Transform[] Grid;
        public List<GameObject> chosen1s;
        public List<GameObject> chosen2s;
        public List<GameObject> chosen3s;
        public List<GameObject> chosen4s;
        public List<Color> chosenColors;    // 0번째 인덱스는 타인의 Chosen 색, 1번째 인덱스는 자신의 Chosen 색
        public Button startButton;
        public List<Button> teamChangeButtons;
        public List<Button> otherButtons;
        public List<InputField> otherInputFields;
        public GameObject disconnectedCanvas;

        private const int BUFFER_PLAYER_ITEMS = 10;
		private List<LobbyPlayerItem> _lobbyPlayersInactive = new List<LobbyPlayerItem>();
		private List<LobbyPlayerItem> _lobbyPlayersPool = new List<LobbyPlayerItem>();
		private LobbyPlayer _myself;
		private NetworkObject _networkObjectReference;
        private bool isSetupCompleted = false;
        private bool isStarted = false;
        private float startTimer = 0f;

		#region Interface Members
		private List<IClientMockPlayer> _lobbyPlayers = new List<IClientMockPlayer>();
		public List<IClientMockPlayer> LobbyPlayers
		{
			get
			{
				return _lobbyPlayers;
			}
		}

		private Dictionary<uint, IClientMockPlayer> _lobbyPlayersMap = new Dictionary<uint, IClientMockPlayer>();
		public Dictionary<uint, IClientMockPlayer> LobbyPlayersMap
		{
			get
			{
				return _lobbyPlayersMap;
			}
		}

		private Dictionary<int, List<IClientMockPlayer>> _lobbyTeams = new Dictionary<int, List<IClientMockPlayer>>();
		public Dictionary<int, List<IClientMockPlayer>> LobbyTeams
		{
			get
			{
				return _lobbyTeams;
			}
		}

        private Dictionary<int, uint> _lobbyPlayersStarted = new Dictionary<int, uint>();   // <TeamID, NetworkID>
        public Dictionary<int, uint> LobbyPlayersStarted
        {
            get
            {
                return _lobbyPlayersStarted;
            }
        }
		#endregion

		public void Awake()
		{
            /* My custom code */
            if (lm != null)
            {
                Destroy(gameObject);
                return;
            }
            lm = this;
            DontDestroyOnLoad(this);
            GetComponent<Canvas>().enabled = true;
            /* My custom code ends */

            if (NetworkManager.Instance.IsServer)
            {
                startButton.interactable = true;
                foreach (Button b in teamChangeButtons)
                {
                    b.interactable = false;
                }
                SetupComplete();
				return;
			}

            startButton.interactable = false;
            foreach (Button b in teamChangeButtons)
            {
                b.interactable = false;
            }
            foreach (Button b in otherButtons)
            {
                b.interactable = false;
            }
            foreach (InputField f in otherInputFields)
            {
                f.interactable = false;
            }

            NetworkManager.Instance.Networker.objectCreateRequested += CheckForService;
            NetworkManager.Instance.Networker.factoryObjectCreated += FactoryObjectCreated;
            NetworkManager.Instance.Networker.disconnected += DisconnectedFromServer;


            for (int i = 0; i < NetworkObject.NetworkObjects.Count; ++i)
            {
                NetworkObject n = NetworkObject.NetworkObjects[i];
                if (n is LobbyService.LobbyServiceNetworkObject)
                {
                    SetupService(n);
                    return;
                }
            }
        }

        void Update()
        {
            if (!SceneManager.GetActiveScene().name.Equals("Lobby") || NetworkManager.Instance == null)
            {
                foreach (InputField f in otherInputFields)
                {
                    f.enabled = false;
                }
            }
            foreach (InputField f in otherInputFields)
            {
                f.enabled = true;
            }

            if (isSetupCompleted && NetworkManager.Instance.IsServer)
                StartGame(2);

            if (!GetComponent<Canvas>().enabled)
            {
                ReturnToLobby();
            }
        }

		private void CheckForService(NetWorker networker, int identity, uint id, Frame.FrameStream frame, Action<NetworkObject> callback)
		{
			if (identity != LobbyService.LobbyServiceNetworkObject.IDENTITY)
			{
				return;
			}
            
            NetworkManager.Instance.Networker.objectCreateRequested -= CheckForService;
			NetworkObject obj = new LobbyService.LobbyServiceNetworkObject(networker, id, frame);
			if (callback != null)
				callback(obj);
			SetupService(obj);
		}

		private void FactoryObjectCreated(NetworkObject obj)
        {
            if (obj.UniqueIdentity != LobbyService.LobbyServiceNetworkObject.IDENTITY)
				return;

            NetworkManager.Instance.Networker.factoryObjectCreated -= FactoryObjectCreated;
            SetupService(obj);
        }

		private void SetupService(NetworkObject obj)
		{
			LobbyService.Instance.Initialize(obj);
			SetupComplete();
		}

        private void DisconnectedFromServer(NetWorker sender)
        {
            NetworkManager.Instance.Networker.disconnected -= DisconnectedFromServer;

            MainThreadManager.Run(() =>
            {
                NetworkManager.Instance.Disconnect();
                Instantiate(disconnectedCanvas);
            });
        }

        #region Public API
        public void ChangeName(LobbyPlayerItem item, string newName)
		{
			LobbyService.Instance.SetName(newName);
		}

		public void KickPlayer(LobbyPlayerItem item)
		{
			LobbyPlayer playerKicked = item.AssociatedPlayer;
			LobbyService.Instance.KickPlayer(playerKicked.NetworkId);
		}

		public void ChangeAvatarID(LobbyPlayerItem item, int nextID)
		{
			LobbyService.Instance.SetAvatar(nextID);
		}

		public void ChangeTeam(LobbyPlayerItem item, int nextTeam)
		{
            // TODO: Host cannot change team! Host's team number is always 0.
            if (NetworkManager.Instance.IsServer && nextTeam != 0)
                return;

            if (!NetworkManager.Instance.IsServer && nextTeam == 0)
                return;

			LobbyService.Instance.SetTeamId(nextTeam);
        }

        public void DisconnectLobby()
        {
            NetworkManager.Instance.Networker.Disconnect(false);
            foreach (Button b in GameObject.FindObjectsOfType<Button>())
            {
                b.interactable = false;
            }
            foreach (InputField f in GameObject.FindObjectsOfType<InputField>())
            {
                f.interactable = false;
            }
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void SendPlayersMessage()
		{
			string chatMessage = ChatInputBox.text;
			if (string.IsNullOrEmpty(chatMessage))
				return;

			LobbyService.Instance.SendPlayerMessage(chatMessage);
			ChatInputBox.text = string.Empty;
		}

        public void StartGame(int sceneID)
        {
            /* My custom code */
            if (NetworkManager.Instance.IsServer)
            {
                if (LobbyService.Instance.MasterLobby.LobbyPlayers.Count != 5)
                {
                    BMSLogger.DebugLog("Player number must be 5!");
                    startTimer = 0f;
                    return;
                }
                bool[] teams = new bool[5] { false, false, false, false, false };
                foreach (var p in LobbyService.Instance.MasterLobby.LobbyPlayers)
                {
                    if (p.TeamID < 0 || p.TeamID > 4)
                    {
                        BMSLogger.DebugLog("Player TeamID must be in range of [0, 4]!");
                        startTimer = 0f;
                        return;
                    }

                    if (!teams[p.TeamID])
                    {
                        teams[p.TeamID] = true;
                    }
                    else
                    {
                        BMSLogger.DebugLog("Player TeamID must not be duplicated!");
                        startTimer = 0f;
                        return;
                    }
                }
                if (Myself.AssociatedPlayer.TeamID != 0)
                {
                    BMSLogger.DebugLog("Host TeamID must be 0!");
                    return;
                }

                startTimer += Time.deltaTime;

                if (startTimer >= 3f && !isStarted)
                {
                    isStarted = true;
                    ((IServer)NetworkManager.Instance.Networker).StopAcceptingConnections();
                    foreach (Button b in GameObject.FindObjectsOfType<Button>())
                    {
                        b.interactable = false;
                    }

                    foreach (var p in LobbyService.Instance.MasterLobby.LobbyPlayers)
                    {
                        // Copy and archive LobbyPlayers' information to use it in game
                        if (p.TeamID == 0) continue;
                        _lobbyPlayersStarted.Add(p.TeamID, p.NetworkId);
                    }

#if UNITY_5_6_OR_NEWER
                    SceneManager.LoadScene(sceneID);
#else
                    Application.LoadLevel(sceneID);
#endif
                }
            }
        }

        public void ReturnToLobby()
        {
            GetComponent<Canvas>().enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (NetworkManager.Instance.IsServer)
            {
                startTimer = 0f;
                isStarted = false;
                isSetupCompleted = false;
                ((IServer)NetworkManager.Instance.Networker).StartAcceptingConnections();
                _lobbyPlayersStarted = new Dictionary<int, uint>();

                SetupComplete();
            }
            else
            {
                isSetupCompleted = false;
                SetupComplete();
            }
        }
#endregion

#region Private API
        private LobbyPlayerItem GetNewPlayerItem()
		{
			LobbyPlayerItem returnValue = null;

			if (_lobbyPlayersInactive.Count > 0)
			{
				returnValue = _lobbyPlayersInactive[0];
				_lobbyPlayersInactive.Remove(returnValue);
				_lobbyPlayersPool.Add(returnValue);
			}
			else
			{
				//Generate more!
				for (int i = 0; i < BUFFER_PLAYER_ITEMS - 1; ++i)
				{
					LobbyPlayerItem item = CreateNewPlayerItem();
					item.ToggleObject(false);
					item.SetParent(Grid[0]);
					_lobbyPlayersInactive.Add(item);
				}
				returnValue = CreateNewPlayerItem();
				_lobbyPlayersPool.Add(returnValue);
			}
			returnValue.ToggleObject(true);

			return returnValue;
		}

		private void PutBackToPool(LobbyPlayerItem item)
		{
			item.ToggleInteractables(false);
			item.ToggleObject(false);
			_lobbyPlayersPool.Remove(item);
			_lobbyPlayersInactive.Add(item);
		}

		private LobbyPlayerItem CreateNewPlayerItem()
		{
			LobbyPlayerItem returnValue = Instantiate(PlayerItem).GetComponent<LobbyPlayerItem>();
			returnValue.Init(this);
			return returnValue;
		}

		private LobbyPlayerItem GrabLobbyPlayerItem(IClientMockPlayer player)
		{
			LobbyPlayerItem returnValue = null;

			for (int i = 0; i < _lobbyPlayersPool.Count; ++i)
			{
				if (_lobbyPlayersPool[i].AssociatedPlayer.NetworkId == player.NetworkId)
				{
					returnValue = _lobbyPlayersPool[i];
				}
			}

			return returnValue;
		}

		private LobbyPlayer GrabPlayer(IClientMockPlayer player)
		{
			LobbyPlayer returnValue = player as LobbyPlayer;
			if (returnValue == null)
			{
				for (int i = 0; i < LobbyPlayers.Count; ++i)
				{
					if (LobbyPlayers[i].NetworkId == player.NetworkId)
					{
						LobbyPlayer tPlayer = LobbyPlayers[i] as LobbyPlayer;
						if (tPlayer == null)
						{
							tPlayer = new LobbyPlayer();
							tPlayer.Name = player.Name;
							tPlayer.NetworkId = player.NetworkId;
                            tPlayer.AvatarID = player.AvatarID;
                            tPlayer.TeamID = player.TeamID;
							LobbyPlayers[i] = tPlayer;
						}
						returnValue = tPlayer;
						returnValue.Name = player.Name;
						break;
					}
				}

				if (returnValue == null)
				{
					returnValue = new LobbyPlayer();
					returnValue.Name = player.Name;
					returnValue.NetworkId = player.NetworkId;
                    returnValue.AvatarID = player.AvatarID;
                    returnValue.TeamID = player.TeamID;
				}
			}

			return returnValue;
		}

        // My custom code
        /// <summary>
        /// 팻말 표시를 현재 팀 구성에 맞게 업데이트합니다.
        /// </summary>
        private void UpdateChosens()
        {
            List<List<string>> playerNames = new List<List<string>>();
            for (int i = 0; i <= 4; i++)
            {
                playerNames.Add(new List<string>());
            }

            foreach (var p in LobbyService.Instance.MasterLobby.LobbyPlayers)
            {
                if (p.TeamID >= 1 && p.TeamID <= 4)
                {
                    if (p.NetworkId == _myself.NetworkId)
                    {
                        playerNames[p.TeamID].Add("m" + p.Name);    // 내 이름
                    }
                    else
                    {
                        playerNames[p.TeamID].Add("o" + p.Name);    // 다른 플레이어의 이름
                    }
                }
            }

            int j;
            for (j = 0; j < playerNames[1].Count && j < chosen1s.Count; j++)
            {
                chosen1s[j].SetActive(true);
                chosen1s[j].GetComponentInChildren<Text>().text = playerNames[1][j].Substring(1);
                if (playerNames[1][j].Substring(0, 1).Equals("o"))
                {
                    chosen1s[j].GetComponent<Image>().color = chosenColors[0];
                }
                else if (playerNames[1][j].Substring(0, 1).Equals("m"))
                {
                    chosen1s[j].GetComponent<Image>().color = chosenColors[1];
                }
            }
            for (; j < 4; j++)
            {
                chosen1s[j].GetComponentInChildren<Text>().text = "";
                chosen1s[j].SetActive(false);
            }

            for (j = 0; j < playerNames[2].Count && j < chosen2s.Count; j++)
            {
                chosen2s[j].SetActive(true);
                chosen2s[j].GetComponentInChildren<Text>().text = playerNames[2][j].Substring(1);
                if (playerNames[2][j].Substring(0, 1).Equals("o"))
                {
                    chosen2s[j].GetComponent<Image>().color = chosenColors[0];
                }
                else if (playerNames[2][j].Substring(0, 1).Equals("m"))
                {
                    chosen2s[j].GetComponent<Image>().color = chosenColors[1];
                }
            }
            for (; j < 4; j++)
            {
                chosen2s[j].GetComponentInChildren<Text>().text = "";
                chosen2s[j].SetActive(false);
            }

            for (j = 0; j < playerNames[3].Count && j < chosen3s.Count; j++)
            {
                chosen3s[j].SetActive(true);
                chosen3s[j].GetComponentInChildren<Text>().text = playerNames[3][j].Substring(1);
                if (playerNames[3][j].Substring(0, 1).Equals("o"))
                {
                    chosen3s[j].GetComponent<Image>().color = chosenColors[0];
                }
                else if (playerNames[3][j].Substring(0, 1).Equals("m"))
                {
                    chosen3s[j].GetComponent<Image>().color = chosenColors[1];
                }
            }
            for (; j < 4; j++)
            {
                chosen3s[j].GetComponentInChildren<Text>().text = "";
                chosen3s[j].SetActive(false);
            }


            for (j = 0; j < playerNames[4].Count && j < chosen4s.Count; j++)
            {
                chosen4s[j].SetActive(true);
                chosen4s[j].GetComponentInChildren<Text>().text = playerNames[4][j].Substring(1);
                if (playerNames[4][j].Substring(0, 1).Equals("o"))
                {
                    chosen4s[j].GetComponent<Image>().color = chosenColors[0];
                }
                else if (playerNames[4][j].Substring(0, 1).Equals("m"))
                {
                    chosen4s[j].GetComponent<Image>().color = chosenColors[1];
                }
            }
            for (; j < 4; j++)
            {
                chosen4s[j].GetComponentInChildren<Text>().text = "";
                chosen4s[j].SetActive(false);
            }
        }
#endregion

#region Interface API
		public void OnFNPlayerConnected(IClientMockPlayer player)
		{
			LobbyPlayer convertedPlayer = GrabPlayer(player);
			if (convertedPlayer == _myself || _myself == null)
				return; //Ignore re-adding ourselves

            if (convertedPlayer == null) return;
            
            bool playerCreated = false;
            for (int i = 0; i < _lobbyPlayersPool.Count; ++i)
            {
                if (_lobbyPlayersPool[i] == null || _lobbyPlayersPool[i].AssociatedPlayer == null) continue;
                if (player == null) BMSLogger.DebugLog("IClientMockPlayer player is null");
                if (_lobbyPlayersPool[i].AssociatedPlayer.NetworkId == player.NetworkId)
                    playerCreated = true;
            }

            playerCreated = convertedPlayer.Created;
            if (playerCreated)
                return;
            
            convertedPlayer.Created = true;

            if (!LobbyPlayers.Contains(convertedPlayer))
                _lobbyPlayers.Add(convertedPlayer);
            if (_lobbyPlayersMap.ContainsKey(convertedPlayer.NetworkId))
                _lobbyPlayersMap[convertedPlayer.NetworkId] = convertedPlayer;
            else
                _lobbyPlayersMap.Add(convertedPlayer.NetworkId, convertedPlayer);

            OnFNTeamChanged(convertedPlayer);

            MainThreadManager.Run(() =>
            {
                LobbyPlayerItem item = GetNewPlayerItem();
                item.Setup(convertedPlayer, false);
                /*
                if (LobbyService.Instance.IsServer)
                    item.KickButton.SetActive(true);
                */
                item.SetParent(Grid[0]);
            });
        }

		public void OnFNPlayerDisconnected(IClientMockPlayer player)
		{
			LobbyPlayer convertedPlayer = GrabPlayer(player);

            MainThreadManager.Run(() =>
			{
                if (LobbyPlayers.Contains(convertedPlayer))
				{
					_lobbyPlayers.Remove(convertedPlayer);
					_lobbyPlayersMap.Remove(convertedPlayer.NetworkId);

					LobbyPlayerItem item = GrabLobbyPlayerItem(convertedPlayer);
					if (item != null)
						PutBackToPool(item);

                    UpdateChosens();
                }
                if (SceneManager.GetActiveScene().buildIndex == 2 && NetworkManager.Instance != null && NetworkManager.Instance.IsServer && GameManager.instance != null)
                {
                    GameManager.instance.networkObject.SendRpc(GameManager.RPC_GAME_END, Receivers.All, 4, false);
                    ReturnToLobby();
                }
            });
		}

		public void OnFNPlayerNameChanged(IClientMockPlayer player)
		{
			LobbyPlayer convertedPlayer = GrabPlayer(player);
			convertedPlayer.Name = player.Name;
			if (_myself == convertedPlayer)
				Myself.ChangeName(convertedPlayer.Name);
			else
			{
				LobbyPlayerItem item = GrabLobbyPlayerItem(convertedPlayer);
                if (item != null)
                    item.ChangeName(convertedPlayer.Name);
            }
            MainThreadManager.Instance.Execute(() => UpdateChosens());
        }

		public void OnFNTeamChanged(IClientMockPlayer player)
		{
            int newID = player.TeamID;
			if (!LobbyTeams.ContainsKey(newID))
				LobbyTeams.Add(newID, new List<IClientMockPlayer>());

			//We do this to not make Foreach loops
			IEnumerator iter = LobbyTeams.GetEnumerator();
			iter.Reset();
			while (iter.MoveNext())
			{
				if (iter.Current != null)
				{
					KeyValuePair<int, List<IClientMockPlayer>> kv = (KeyValuePair<int, List<IClientMockPlayer>>)iter.Current;
					if (kv.Value.Contains(player))
					{
						kv.Value.Remove(player);
						break;
					}
				}
				else
					break;
			}

			//We prevent the player being added twice to the same team
			if (!LobbyTeams[newID].Contains(player))
			{
				LobbyPlayer convertedPlayer = player as LobbyPlayer;
                if (convertedPlayer != null)
                {
                    convertedPlayer.TeamID = newID;

                    if (_myself == convertedPlayer)
                        Myself.ChangeTeam(newID);
                    else
                    {
                        LobbyPlayerItem item = GrabLobbyPlayerItem(convertedPlayer);
                        if (item != null)
                            item.ChangeTeam(newID);
                    }
                }

				LobbyTeams[newID].Add(player);
			}
            MainThreadManager.Instance.Execute(() => UpdateChosens());
		}

		public void OnFNAvatarIDChanged(IClientMockPlayer player)
		{
			LobbyPlayer convertedPlayer = GrabPlayer(player);
			convertedPlayer.AvatarID = player.AvatarID;
			if (_myself == convertedPlayer)
				Myself.ChangeAvatarID(convertedPlayer.AvatarID);
			else
			{
				LobbyPlayerItem item = GrabLobbyPlayerItem(convertedPlayer);
                if (item != null)
                    item.ChangeAvatarID(convertedPlayer.AvatarID);
			}
		}

		public void OnFNLobbyPlayerMessageReceived(IClientMockPlayer player, string message)
		{
			LobbyPlayer convertedPlayer = GrabPlayer(player);
			Chatbox.text += string.Format("{0}: {1}\n", convertedPlayer.Name, message);
		}

        public void OnFNPlayerSync(IClientMockPlayer player)
        {
            OnFNAvatarIDChanged(player);
            OnFNTeamChanged(player);
            OnFNPlayerNameChanged(player);
        }

		public void OnFNLobbyMasterKnowledgeTransfer(ILobbyMaster previousLobbyMaster)
		{
			LobbyPlayers.Clear();
			LobbyPlayersMap.Clear();
			LobbyTeams.Clear();
			for (int i = 0; i < previousLobbyMaster.LobbyPlayers.Count; ++i)
			{
				LobbyPlayer player = GrabPlayer(previousLobbyMaster.LobbyPlayers[i]);
				LobbyPlayers.Add(player);
				LobbyPlayersMap.Add(player.NetworkId, player);
			}

			IEnumerator iterTeams = previousLobbyMaster.LobbyTeams.GetEnumerator();
			iterTeams.Reset();
			while (iterTeams.MoveNext())
			{
				if (iterTeams.Current != null)
				{
					KeyValuePair<int, List<IClientMockPlayer>> kv = (KeyValuePair<int, List<IClientMockPlayer>>)iterTeams.Current;
					List<IClientMockPlayer> players = new List<IClientMockPlayer>();
					for (int i = 0; i < kv.Value.Count; ++i)
					{
						players.Add(GrabPlayer(kv.Value[i]));
					}
					LobbyTeams.Add(kv.Key, players);
				}
				else
					break;
			}

			IEnumerator iterPlayersMap = previousLobbyMaster.LobbyPlayersMap.GetEnumerator();
			iterPlayersMap.Reset();
			while (iterPlayersMap.MoveNext())
			{
				if (iterPlayersMap.Current != null)
				{
					KeyValuePair<uint, IClientMockPlayer> kv = (KeyValuePair<uint, IClientMockPlayer>)iterPlayersMap.Current;

					if (LobbyPlayersMap.ContainsKey(kv.Key))
						LobbyPlayersMap[kv.Key] = GrabPlayer(kv.Value);
					else
						LobbyPlayersMap.Add(kv.Key, GrabPlayer(kv.Value));
				}
				else
					break;
			}
		}

		private void SetupComplete()
        {
            if (isSetupCompleted) return;
            isSetupCompleted = true;
            //BMSLogger.DebugLog("SetupComplete");

            LobbyService.Instance.SetLobbyMaster(this);
            LobbyService.Instance.Initialize(NetworkManager.Instance.Networker);

            LobbyService.Instance.FlushCreateActions();

            //If I am the host, then I should show the kick button for all players here
            MainThreadManager.Run(() =>
            {
                LobbyPlayerItem item = GetNewPlayerItem(); //This will just auto generate the 10 items we need to start with
                item.SetParent(Grid[0]);
                PutBackToPool(item);
                if (NetworkManager.Instance.IsServer)
                {
                    item.RequestChangeTeam(0);
                }
                else
                {
                    item.RequestChangeTeam(5);
                }
            });

			_myself = GrabPlayer(LobbyService.Instance.MyMockPlayer);
			if (!LobbyPlayers.Contains(_myself))
				LobbyPlayers.Add(_myself);
			Myself.Init(this);
			Myself.Setup(_myself, true);

            List<IClientMockPlayer> currentPlayers = LobbyService.Instance.MasterLobby.LobbyPlayers;
			for (int i = 0; i < currentPlayers.Count; ++i)
			{
				IClientMockPlayer currentPlayer = currentPlayers[i];
				if (currentPlayer == _myself)
					continue;
				OnFNPlayerConnected(currentPlayers[i]);
            }

            MainThreadManager.Run(() =>
            {
                foreach (Button b in otherButtons)
                {
                    b.interactable = true;
                }
                foreach (InputField f in otherInputFields)
                {
                    f.interactable = true;
                }
                if (!NetworkManager.Instance.IsServer)
                {
                    foreach (Button b in teamChangeButtons)
                    {
                        b.interactable = true;
                    }
                }
                UpdateChosens();
            });
		}
#endregion
	}
}