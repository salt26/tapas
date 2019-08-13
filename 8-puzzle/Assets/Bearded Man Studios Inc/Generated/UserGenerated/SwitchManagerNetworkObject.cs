using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0,0]")]
	public partial class SwitchManagerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 9;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private int _switchsState;
		public event FieldEvent<int> switchsStateChanged;
		public Interpolated<int> switchsStateInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int switchsState
		{
			get { return _switchsState; }
			set
			{
				// Don't do anything if the value is the same
				if (_switchsState == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_switchsState = value;
				hasDirtyFields = true;
			}
		}

		public void SetswitchsStateDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_switchsState(ulong timestep)
		{
			if (switchsStateChanged != null) switchsStateChanged(_switchsState, timestep);
			if (fieldAltered != null) fieldAltered("switchsState", _switchsState, timestep);
		}
		[ForgeGeneratedField]
		private int _onGroupsNum;
		public event FieldEvent<int> onGroupsNumChanged;
		public Interpolated<int> onGroupsNumInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int onGroupsNum
		{
			get { return _onGroupsNum; }
			set
			{
				// Don't do anything if the value is the same
				if (_onGroupsNum == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_onGroupsNum = value;
				hasDirtyFields = true;
			}
		}

		public void SetonGroupsNumDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_onGroupsNum(ulong timestep)
		{
			if (onGroupsNumChanged != null) onGroupsNumChanged(_onGroupsNum, timestep);
			if (fieldAltered != null) fieldAltered("onGroupsNum", _onGroupsNum, timestep);
		}
		[ForgeGeneratedField]
		private int _lastTouch;
		public event FieldEvent<int> lastTouchChanged;
		public Interpolated<int> lastTouchInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int lastTouch
		{
			get { return _lastTouch; }
			set
			{
				// Don't do anything if the value is the same
				if (_lastTouch == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_lastTouch = value;
				hasDirtyFields = true;
			}
		}

		public void SetlastTouchDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_lastTouch(ulong timestep)
		{
			if (lastTouchChanged != null) lastTouchChanged(_lastTouch, timestep);
			if (fieldAltered != null) fieldAltered("lastTouch", _lastTouch, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			switchsStateInterpolation.current = switchsStateInterpolation.target;
			onGroupsNumInterpolation.current = onGroupsNumInterpolation.target;
			lastTouchInterpolation.current = lastTouchInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _switchsState);
			UnityObjectMapper.Instance.MapBytes(data, _onGroupsNum);
			UnityObjectMapper.Instance.MapBytes(data, _lastTouch);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_switchsState = UnityObjectMapper.Instance.Map<int>(payload);
			switchsStateInterpolation.current = _switchsState;
			switchsStateInterpolation.target = _switchsState;
			RunChange_switchsState(timestep);
			_onGroupsNum = UnityObjectMapper.Instance.Map<int>(payload);
			onGroupsNumInterpolation.current = _onGroupsNum;
			onGroupsNumInterpolation.target = _onGroupsNum;
			RunChange_onGroupsNum(timestep);
			_lastTouch = UnityObjectMapper.Instance.Map<int>(payload);
			lastTouchInterpolation.current = _lastTouch;
			lastTouchInterpolation.target = _lastTouch;
			RunChange_lastTouch(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _switchsState);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _onGroupsNum);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _lastTouch);

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
				if (switchsStateInterpolation.Enabled)
				{
					switchsStateInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					switchsStateInterpolation.Timestep = timestep;
				}
				else
				{
					_switchsState = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_switchsState(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (onGroupsNumInterpolation.Enabled)
				{
					onGroupsNumInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					onGroupsNumInterpolation.Timestep = timestep;
				}
				else
				{
					_onGroupsNum = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_onGroupsNum(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (lastTouchInterpolation.Enabled)
				{
					lastTouchInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					lastTouchInterpolation.Timestep = timestep;
				}
				else
				{
					_lastTouch = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_lastTouch(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (switchsStateInterpolation.Enabled && !switchsStateInterpolation.current.UnityNear(switchsStateInterpolation.target, 0.0015f))
			{
				_switchsState = (int)switchsStateInterpolation.Interpolate();
				//RunChange_switchsState(switchsStateInterpolation.Timestep);
			}
			if (onGroupsNumInterpolation.Enabled && !onGroupsNumInterpolation.current.UnityNear(onGroupsNumInterpolation.target, 0.0015f))
			{
				_onGroupsNum = (int)onGroupsNumInterpolation.Interpolate();
				//RunChange_onGroupsNum(onGroupsNumInterpolation.Timestep);
			}
			if (lastTouchInterpolation.Enabled && !lastTouchInterpolation.current.UnityNear(lastTouchInterpolation.target, 0.0015f))
			{
				_lastTouch = (int)lastTouchInterpolation.Interpolate();
				//RunChange_lastTouch(lastTouchInterpolation.Timestep);
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
