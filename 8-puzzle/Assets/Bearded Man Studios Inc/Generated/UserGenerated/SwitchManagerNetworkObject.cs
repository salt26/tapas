using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0,0]")]
	public partial class SwitchManagerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 12;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private int _switchesState;
		public event FieldEvent<int> switchesStateChanged;
		public Interpolated<int> switchesStateInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int switchesState
		{
			get { return _switchesState; }
			set
			{
				// Don't do anything if the value is the same
				if (_switchesState == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_switchesState = value;
				hasDirtyFields = true;
			}
		}

		public void SetswitchesStateDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_switchesState(ulong timestep)
		{
			if (switchesStateChanged != null) switchesStateChanged(_switchesState, timestep);
			if (fieldAltered != null) fieldAltered("switchesState", _switchesState, timestep);
		}
		[ForgeGeneratedField]
		private byte _state;
		public event FieldEvent<byte> stateChanged;
		public Interpolated<byte> stateInterpolation = new Interpolated<byte>() { LerpT = 0f, Enabled = false };
		public byte state
		{
			get { return _state; }
			set
			{
				// Don't do anything if the value is the same
				if (_state == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_state = value;
				hasDirtyFields = true;
			}
		}

		public void SetstateDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_state(ulong timestep)
		{
			if (stateChanged != null) stateChanged(_state, timestep);
			if (fieldAltered != null) fieldAltered("state", _state, timestep);
		}
		[ForgeGeneratedField]
		private int _recentNum;
		public event FieldEvent<int> recentNumChanged;
		public Interpolated<int> recentNumInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int recentNum
		{
			get { return _recentNum; }
			set
			{
				// Don't do anything if the value is the same
				if (_recentNum == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_recentNum = value;
				hasDirtyFields = true;
			}
		}

		public void SetrecentNumDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_recentNum(ulong timestep)
		{
			if (recentNumChanged != null) recentNumChanged(_recentNum, timestep);
			if (fieldAltered != null) fieldAltered("recentNum", _recentNum, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			switchesStateInterpolation.current = switchesStateInterpolation.target;
			stateInterpolation.current = stateInterpolation.target;
			recentNumInterpolation.current = recentNumInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _switchesState);
			UnityObjectMapper.Instance.MapBytes(data, _state);
			UnityObjectMapper.Instance.MapBytes(data, _recentNum);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_switchesState = UnityObjectMapper.Instance.Map<int>(payload);
			switchesStateInterpolation.current = _switchesState;
			switchesStateInterpolation.target = _switchesState;
			RunChange_switchesState(timestep);
			_state = UnityObjectMapper.Instance.Map<byte>(payload);
			stateInterpolation.current = _state;
			stateInterpolation.target = _state;
			RunChange_state(timestep);
			_recentNum = UnityObjectMapper.Instance.Map<int>(payload);
			recentNumInterpolation.current = _recentNum;
			recentNumInterpolation.target = _recentNum;
			RunChange_recentNum(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _switchesState);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _state);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _recentNum);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (switchesStateInterpolation.Enabled)
				{
					switchesStateInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					switchesStateInterpolation.Timestep = timestep;
				}
				else
				{
					_switchesState = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_switchesState(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (stateInterpolation.Enabled)
				{
					stateInterpolation.target = UnityObjectMapper.Instance.Map<byte>(data);
					stateInterpolation.Timestep = timestep;
				}
				else
				{
					_state = UnityObjectMapper.Instance.Map<byte>(data);
					RunChange_state(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (recentNumInterpolation.Enabled)
				{
					recentNumInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					recentNumInterpolation.Timestep = timestep;
				}
				else
				{
					_recentNum = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_recentNum(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (switchesStateInterpolation.Enabled && !switchesStateInterpolation.current.UnityNear(switchesStateInterpolation.target, 0.0015f))
			{
				_switchesState = (int)switchesStateInterpolation.Interpolate();
				//RunChange_switchesState(switchesStateInterpolation.Timestep);
			}
			if (stateInterpolation.Enabled && !stateInterpolation.current.UnityNear(stateInterpolation.target, 0.0015f))
			{
				_state = (byte)stateInterpolation.Interpolate();
				//RunChange_state(stateInterpolation.Timestep);
			}
			if (recentNumInterpolation.Enabled && !recentNumInterpolation.current.UnityNear(recentNumInterpolation.target, 0.0015f))
			{
				_recentNum = (int)recentNumInterpolation.Interpolate();
				//RunChange_recentNum(recentNumInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public SwitchManagerNetworkObject() : base() { Initialize(); }
		public SwitchManagerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public SwitchManagerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
