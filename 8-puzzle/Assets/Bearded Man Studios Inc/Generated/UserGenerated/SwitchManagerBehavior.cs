using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedRPC("{\"types\":[[\"int\"][][][\"string\"][\"string\"]]")]
	[GeneratedRPCVariableNames("{\"types\":[[\"recentNum\"][][][\"policeSuppMsg\"][\"thiefSuppMsg\"]]")]
	public abstract partial class SwitchManagerBehavior : NetworkBehavior
	{
		public const byte RPC_UPDATE_SWITCHES = 0 + 5;
		public const byte RPC_CANT_PUSH_AGAIN = 1 + 5;
		public const byte RPC_OPEN_EXIT = 2 + 5;
		public const byte RPC_UPDATE_POLICE_SUPP_MSG = 3 + 5;
		public const byte RPC_UPDATE_THIEF_SUPP_MSG = 4 + 5;
		
		public SwitchManagerNetworkObject networkObject = null;

		public override void Initialize(NetworkObject obj)
		{
			// We have already initialized this object
			if (networkObject != null && networkObject.AttachedBehavior != null)
				return;
			
			networkObject = (SwitchManagerNetworkObject)obj;
			networkObject.AttachedBehavior = this;

			base.SetupHelperRpcs(networkObject);
			networkObject.RegisterRpc("UpdateSwitches", UpdateSwitches, typeof(int));
			networkObject.RegisterRpc("CantPushAgain", CantPushAgain);
			networkObject.RegisterRpc("OpenExit", OpenExit);
			networkObject.RegisterRpc("UpdatePoliceSuppMsg", UpdatePoliceSuppMsg, typeof(string));
			networkObject.RegisterRpc("UpdateThiefSuppMsg", UpdateThiefSuppMsg, typeof(string));

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
			Initialize(new SwitchManagerNetworkObject(networker, createCode: TempAttachCode, metadata: metadata));
		}

		private void DestroyGameObject(NetWorker sender)
		{
			MainThreadManager.Run(() => { try { Destroy(gameObject); } catch { } });
			networkObject.onDestroy -= DestroyGameObject;
		}

		public override NetworkObject CreateNetworkObject(NetWorker networker, int createCode, byte[] metadata = null)
		{
			return new SwitchManagerNetworkObject(networker, this, createCode, metadata);
		}

		protected override void InitializedTransform()
		{
			networkObject.SnapInterpolations();
		}

		/// <summary>
		/// Arguments:
		/// int recentNum
		/// </summary>
		public abstract void UpdateSwitches(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void CantPushAgain(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void OpenExit(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void UpdatePoliceSuppMsg(RpcArgs args);
		/// <summary>
		/// Arguments:
		/// </summary>
		public abstract void UpdateThiefSuppMsg(RpcArgs args);

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}