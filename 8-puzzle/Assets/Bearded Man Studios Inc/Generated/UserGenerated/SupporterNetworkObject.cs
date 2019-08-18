using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0.15]")]
	public partial class SupporterNetworkObject : NetworkObject
	{
		public const int IDENTITY = 8;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		[ForgeGeneratedField]
		private Quaternion _droneRotation;
		public event FieldEvent<Quaternion> droneRotationChanged;
		public InterpolateQuaternion droneRotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion droneRotation
		{
			get { return _droneRotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_droneRotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_droneRotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetdroneRotationDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_droneRotation(ulong timestep)
		{
			if (droneRotationChanged != null) droneRotationChanged(_droneRotation, timestep);
			if (fieldAltered != null) fieldAltered("droneRotation", _droneRotation, timestep);
		}
		[ForgeGeneratedField]
		private Quaternion _cameraRotation;
		public event FieldEvent<Quaternion> cameraRotationChanged;
		public InterpolateQuaternion cameraRotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion cameraRotation
		{
			get { return _cameraRotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_cameraRotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_cameraRotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetcameraRotationDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_cameraRotation(ulong timestep)
		{
			if (cameraRotationChanged != null) cameraRotationChanged(_cameraRotation, timestep);
			if (fieldAltered != null) fieldAltered("cameraRotation", _cameraRotation, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			droneRotationInterpolation.current = droneRotationInterpolation.target;
			cameraRotationInterpolation.current = cameraRotationInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _droneRotation);
			UnityObjectMapper.Instance.MapBytes(data, _cameraRotation);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_droneRotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			droneRotationInterpolation.current = _droneRotation;
			droneRotationInterpolation.target = _droneRotation;
			RunChange_droneRotation(timestep);
			_cameraRotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			cameraRotationInterpolation.current = _cameraRotation;
			cameraRotationInterpolation.target = _cameraRotation;
			RunChange_cameraRotation(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _droneRotation);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _cameraRotation);

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
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (droneRotationInterpolation.Enabled)
				{
					droneRotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					droneRotationInterpolation.Timestep = timestep;
				}
				else
				{
					_droneRotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_droneRotation(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (cameraRotationInterpolation.Enabled)
				{
					cameraRotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					cameraRotationInterpolation.Timestep = timestep;
				}
				else
				{
					_cameraRotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_cameraRotation(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (droneRotationInterpolation.Enabled && !droneRotationInterpolation.current.UnityNear(droneRotationInterpolation.target, 0.0015f))
			{
				_droneRotation = (Quaternion)droneRotationInterpolation.Interpolate();
				//RunChange_droneRotation(droneRotationInterpolation.Timestep);
			}
			if (cameraRotationInterpolation.Enabled && !cameraRotationInterpolation.current.UnityNear(cameraRotationInterpolation.target, 0.0015f))
			{
				_cameraRotation = (Quaternion)cameraRotationInterpolation.Interpolate();
				//RunChange_cameraRotation(cameraRotationInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public SupporterNetworkObject() : base() { Initialize(); }
		public SupporterNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public SupporterNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
