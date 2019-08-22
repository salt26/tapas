using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedRPC("{\"types\":[[\"int\", \"bool\"][\"int\"][\"int\"][\"string\", \"int\", \"int\"]]")]
	[GeneratedRPCVariableNames("{\"types\":[[\"win_TeamID\", \"timeOver\"][\"teamID\"][\"playerTeamID\"][\"message\", \"teamNum\", \"senderID\"]]")]
	public abstract partial class GameManagerBehavior : NetworkBehavior
	{
		public const byte RPC_GAME_END = 0 + 5;
		public const byte RPC_GAME_START = 1 + 5;
		public const byte RPC_READY = 2 + 5;
		public const byte RPC_RECEIVE_MESSAGE = 3 + 5;
		
		public GameManagerNetworkObject networkObject = null;

		public override void Initialize(NetworkObject obj)
		{
			// We have already initialized this object
			if (networkObject != null && networkObject.AttachedBehavior != null)
				return;
			
			networkObject = (GameManagerNetworkObject)obj;
			networkObject.AttachedBehavior = this;

			base.SetupHelperRpcs(networkObject);
			networkObject.RegisterRpc("GameEnd", GameEnd, typeof(int), typeof(bool));
			networkObject.RegisterRpc("GameStart", GameStart, typeof(int));
			networkObject.RegisterRpc("Ready", Ready, typeof(int));
			networkObject.RegisterRpc("ReceiveMessage", ReceiveMessage, typeof(string), typeof(int), typeof(int));

			networkObject.onDestroy += DestroyGameObject;

			if (!obj.IsOwner)
			{
				if (!skipAttachIds.ContainsKey(obj.NetworkId)){
					uint newId = obj.NetworkId + 1;
					ProcessOthers(gameObject.transform, ref newId);
				}
				else
					skipAttachIds.Remove(obj.NetworkId);
			}

			if (obj.Metadata != null)
			{
				byte transformFlags = obj.Metadata[0];

				if (transformFlags != 0)
				{
					BMSByte metadataTransform = new BMSByte();
					metadataTransform.Clone(obj.Metadata);
					metadataTransform.MoveStartIndex(1);

					if ((transformFlags & 0x01) != 0 && (transformFlags & 0x02) != 0)
					{
						MainThreadManager.Run(() =>
						{
							transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform);
							transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform);
						});
					}
					else if ((transformFlags & 0x01) != 0)
					{
						MainThreadManager.Run(() => { transform.position = ObjectMapper.Instance.Map<Vector3>(metadataTransform); });
					}
					else if ((transformFlags & 0x02) != 0)
					{
						MainThreadManager.Run(() => { transform.rotation = ObjectMapper.Instance.Map<Quaternion>(metadataTransform); });
					}
				}
			}

			MainThreadManager.Run(() =>
			{
				NetworkStart();
				networkObject.Networker.FlushCreateActions(networkObject);
			});
		}

		protected override void CompleteRegistration()
		{
			base.CompleteRegistration();
			networkObject.ReleaseCreateBuffer();
		}

		public override void Initialize(NetWorker networker, byte[] metadata = null)
		{
			Initialize(new GameManagerNetworkObject(networker, createCode: TempAttachCode, metadata: metadata));
		}

		private void DestroyGameObject(NetWorker sender)
		{
			MainThreadManager.Run(() => { try { Destroy(gameObject); } catch { } });
			networkObject.onDestroy -= DestroyGameObject;
		}

		public override NetworkObject CreateNetworkObject(NetWorker networker, int createCode, byte[] metadata = null)
		{
			return new GameManagerNetworkObject(networker, this, createCode, metadata);
		}

		protected override void InitializedTransform()
		{
			networkObject.SnapInterpolations();
		}

		/// <summary>
		/// Arguments:
		/// int win_TeamID
		/// bool timeOver
		/// </summary>
		public abstract void GameEnd(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// int teamID
		/// </summary>
		public abstract void GameStart(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// int playerTeamID
		/// </summary>
		public abstract void Ready(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// string message
		/// int teamNum
		/// int senderID
		/// </summary>
		public abstract void ReceiveMessage(RpcArgs args);

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}