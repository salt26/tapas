﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace BeardedManStudios.Forge.Networking.Unity.Lobby
{
	public class LobbyPlayerItem : MonoBehaviour
	{
		public Color[] TeamColors;
		//public Color[] AvatarColors;
		public GameObject KickButton;
		//public Image AvatarBG;
		//public Text AvatarID;
		public InputField PlayerName;  //use only this one
		public Text PlayerTeamID;  //maybe use?

        public Button[] Buttons;

		[HideInInspector]
		public Transform ThisTransform;

		[HideInInspector]
		public GameObject ThisGameObject;

		public LobbyPlayer AssociatedPlayer { get; private set; }
		private LobbyManager _manager;

		public void Init(LobbyManager manager)
		{
			ThisGameObject = gameObject;
			ThisTransform = transform;
			_manager = manager;
		}

		public void Setup(LobbyPlayer associatedPlayer, bool interactableValue)
		{
			ToggleInteractables(interactableValue);
			AssociatedPlayer = associatedPlayer;
            //ChangeAvatarID(associatedPlayer.AvatarID);
            ChangeName(associatedPlayer.Name);
            ChangeTeam(associatedPlayer.TeamID);
		}

		public void SetParent(Transform parent)
		{
			ThisTransform.SetParent(parent);
			ThisTransform.localPosition = Vector3.zero;
			ThisTransform.localScale = Vector3.one;
		}

		public void KickPlayer()
		{
			_manager.KickPlayer(this);
		}
		
		public void RequestChangeTeam(int nextID)
		{
			_manager.ChangeTeam(this, nextID);
		}

        
		public void RequestChangeAvatarID()  //will not use
		{
            /*
			int nextID = AssociatedPlayer.AvatarID + 1;
			if (nextID >= AvatarColors.Length)
				nextID = 0;

			_manager.ChangeAvatarID(this, nextID);
            */
		}
        

		public void RequestChangeName()
		{
			_manager.ChangeName(this, PlayerName.text);
		}

        
		public void ChangeAvatarID(int id)   //will not use
		{
            /*
			Color avatarColor = Color.white;

			//Note: This is just an example, you are free to make your own team colors and
			// change this to however you see fit
			if (TeamColors.Length > id && id >= 0)
				avatarColor = AvatarColors[id];

			AvatarID.text = id.ToString();
			AvatarBG.color = avatarColor;
            */
		}
        

		public void ChangeName(string name)
		{
			PlayerName.text = name;
		}

		public void ChangeTeam(int id)
		{
            //PlayerTeamID.text = string.Format("Team {0}", id);
            if (PlayerTeamID != null)
            {
                switch (id)
                {
                    case 0:
                        PlayerTeamID.text = "호스트";
                        break;
                    case 1:
                        PlayerTeamID.text = "경찰";
                        break;
                    case 2:
                        PlayerTeamID.text = "도둑";
                        break;
                    case 3:
                        PlayerTeamID.text = "경찰 조력자";
                        break;
                    case 4:
                        PlayerTeamID.text = "도둑 조력자";
                        break;
                    default:
                        PlayerTeamID.text = "";
                        break;
                }
            }
            // TODO: 푯말 움직이는 코드를 여기에 넣으세요.
		}

		public void ToggleInteractables(bool value)
		{
            for (int i = 0; i < Buttons.Length; ++i)
                Buttons[i].interactable = value;

            //AvatarBG.raycastTarget = value;
            if (PlayerTeamID != null)
			    PlayerTeamID.raycastTarget = value;
			PlayerName.interactable = value;
		}

		public void ToggleObject(bool value)
		{
			ThisGameObject.SetActive(value);
		}
	}
}