using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0]")]
	public partial class GameManagerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 5;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private float _time;
		public event FieldEvent<float> timeChanged;
		public InterpolateFloat timeInterpolation = new InterpolateFloat() { LerpT = 0f, Enabled = false };
		public float time
		{
			get { return _time; }
			set
			{
				// Don't do anything if the value is the same
				if (_time == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_time = value;
				hasDirtyFields = true;
			}
		}

		public void SettimeDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_time(ulong timestep)
		{
			if (timeChanged != null) timeChanged(_time, timestep);
			if (fieldAltered != null) fieldAltered("time", _time, timestep);
		}
		[ForgeGeneratedField]
		private bool _isReady;
		public event FieldEvent<bool> isReadyChanged;
		public Interpolated<bool> isReadyInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool isReady
		{
			get { return _isReady; }
			set
			{
				// Don't do anything if the value is the same
				if (_isReady == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_isReady = value;
				hasDirtyFields = true;
			}
		}

		public void SetisReadyDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_isReady(ulong timestep)
		{
			if (isReadyChanged != null) isReadyChanged(_isReady, timestep);
			if (fieldAltered != null) fieldAltered("isReady", _isReady, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			timeInterpolation.current = timeInterpolation.target;
			isReadyInterpolation.current = isReadyInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _time);
			UnityObjectMapper.Instance.MapBytes(data, _isReady);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_time = UnityObjectMapper.Instance.Map<float>(payload);
			timeInterpolation.current = _time;
			timeInterpolation.target = _time;
			RunChange_time(timestep);
			_isReady = UnityObjectMapper.Instance.Map<bool>(payload);
			isReadyInterpolation.current = _isReady;
			isReadyInterpolation.target = _isReady;
			RunChange_isReady(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _time);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _isReady);

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
				if (timeInterpolation.Enabled)
				{
					timeInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					timeInterpolation.Timestep = timestep;
				}
				else
				{
					_time = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_time(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (isReadyInterpolation.Enabled)
				{
					isReadyInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					isReadyInterpolation.Timestep = timestep;
				}
				else
				{
					_isReady = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_isReady(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (timeInterpolation.Enabled && !timeInterpolation.current.UnityNear(timeInterpolation.target, 0.0015f))
			{
				_time = (float)timeInterpolation.Interpolate();
				//RunChange_time(timeInterpolation.Timestep);
			}
			if (isReadyInterpolation.Enabled && !isReadyInterpolation.current.UnityNear(isReadyInterpolation.target, 0.0015f))
			{
				_isReady = (bool)isReadyInterpolation.Interpolate();
				//RunChange_isReady(isReadyInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public GameManagerNetworkObject() : base() { Initialize(); }
		public GameManagerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public GameManagerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
