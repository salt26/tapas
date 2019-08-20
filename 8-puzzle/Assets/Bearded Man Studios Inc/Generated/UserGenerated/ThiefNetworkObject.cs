using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0.15,0.15,0,0,0.15]")]
	public partial class ThiefNetworkObject : NetworkObject
	{
		public const int IDENTITY = 11;

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
		private Quaternion _rotation;
		public event FieldEvent<Quaternion> rotationChanged;
		public InterpolateQuaternion rotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion rotation
		{
			get { return _rotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_rotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_rotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetrotationDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_rotation(ulong timestep)
		{
			if (rotationChanged != null) rotationChanged(_rotation, timestep);
			if (fieldAltered != null) fieldAltered("rotation", _rotation, timestep);
		}
		[ForgeGeneratedField]
		private float _mHorizontal;
		public event FieldEvent<float> mHorizontalChanged;
		public InterpolateFloat mHorizontalInterpolation = new InterpolateFloat() { LerpT = 0.15f, Enabled = true };
		public float mHorizontal
		{
			get { return _mHorizontal; }
			set
			{
				// Don't do anything if the value is the same
				if (_mHorizontal == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_mHorizontal = value;
				hasDirtyFields = true;
			}
		}

		public void SetmHorizontalDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_mHorizontal(ulong timestep)
		{
			if (mHorizontalChanged != null) mHorizontalChanged(_mHorizontal, timestep);
			if (fieldAltered != null) fieldAltered("mHorizontal", _mHorizontal, timestep);
		}
		[ForgeGeneratedField]
		private float _mVertical;
		public event FieldEvent<float> mVerticalChanged;
		public InterpolateFloat mVerticalInterpolation = new InterpolateFloat() { LerpT = 0.15f, Enabled = true };
		public float mVertical
		{
			get { return _mVertical; }
			set
			{
				// Don't do anything if the value is the same
				if (_mVertical == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_mVertical = value;
				hasDirtyFields = true;
			}
		}

		public void SetmVerticalDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_mVertical(ulong timestep)
		{
			if (mVerticalChanged != null) mVerticalChanged(_mVertical, timestep);
			if (fieldAltered != null) fieldAltered("mVertical", _mVertical, timestep);
		}
		[ForgeGeneratedField]
		private bool _isRotatingLeft;
		public event FieldEvent<bool> isRotatingLeftChanged;
		public Interpolated<bool> isRotatingLeftInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool isRotatingLeft
		{
			get { return _isRotatingLeft; }
			set
			{
				// Don't do anything if the value is the same
				if (_isRotatingLeft == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_isRotatingLeft = value;
				hasDirtyFields = true;
			}
		}

		public void SetisRotatingLeftDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_isRotatingLeft(ulong timestep)
		{
			if (isRotatingLeftChanged != null) isRotatingLeftChanged(_isRotatingLeft, timestep);
			if (fieldAltered != null) fieldAltered("isRotatingLeft", _isRotatingLeft, timestep);
		}
		[ForgeGeneratedField]
		private bool _isRotatingRight;
		public event FieldEvent<bool> isRotatingRightChanged;
		public Interpolated<bool> isRotatingRightInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool isRotatingRight
		{
			get { return _isRotatingRight; }
			set
			{
				// Don't do anything if the value is the same
				if (_isRotatingRight == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x20;
				_isRotatingRight = value;
				hasDirtyFields = true;
			}
		}

		public void SetisRotatingRightDirty()
		{
			_dirtyFields[0] |= 0x20;
			hasDirtyFields = true;
		}

		private void RunChange_isRotatingRight(ulong timestep)
		{
			if (isRotatingRightChanged != null) isRotatingRightChanged(_isRotatingRight, timestep);
			if (fieldAltered != null) fieldAltered("isRotatingRight", _isRotatingRight, timestep);
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
				_dirtyFields[0] |= 0x40;
				_cameraRotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetcameraRotationDirty()
		{
			_dirtyFields[0] |= 0x40;
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
			rotationInterpolation.current = rotationInterpolation.target;
			mHorizontalInterpolation.current = mHorizontalInterpolation.target;
			mVerticalInterpolation.current = mVerticalInterpolation.target;
			isRotatingLeftInterpolation.current = isRotatingLeftInterpolation.target;
			isRotatingRightInterpolation.current = isRotatingRightInterpolation.target;
			cameraRotationInterpolation.current = cameraRotationInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _mHorizontal);
			UnityObjectMapper.Instance.MapBytes(data, _mVertical);
			UnityObjectMapper.Instance.MapBytes(data, _isRotatingLeft);
			UnityObjectMapper.Instance.MapBytes(data, _isRotatingRight);
			UnityObjectMapper.Instance.MapBytes(data, _cameraRotation);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_rotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			rotationInterpolation.current = _rotation;
			rotationInterpolation.target = _rotation;
			RunChange_rotation(timestep);
			_mHorizontal = UnityObjectMapper.Instance.Map<float>(payload);
			mHorizontalInterpolation.current = _mHorizontal;
			mHorizontalInterpolation.target = _mHorizontal;
			RunChange_mHorizontal(timestep);
			_mVertical = UnityObjectMapper.Instance.Map<float>(payload);
			mVerticalInterpolation.current = _mVertical;
			mVerticalInterpolation.target = _mVertical;
			RunChange_mVertical(timestep);
			_isRotatingLeft = UnityObjectMapper.Instance.Map<bool>(payload);
			isRotatingLeftInterpolation.current = _isRotatingLeft;
			isRotatingLeftInterpolation.target = _isRotatingLeft;
			RunChange_isRotatingLeft(timestep);
			_isRotatingRight = UnityObjectMapper.Instance.Map<bool>(payload);
			isRotatingRightInterpolation.current = _isRotatingRight;
			isRotatingRightInterpolation.target = _isRotatingRight;
			RunChange_isRotatingRight(timestep);
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
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rotation);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _mHorizontal);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _mVertical);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _isRotatingLeft);
			if ((0x20 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _isRotatingRight);
			if ((0x40 & _dirtyFields[0]) != 0)
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
				if (rotationInterpolation.Enabled)
				{
					rotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					rotationInterpolation.Timestep = timestep;
				}
				else
				{
					_rotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_rotation(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (mHorizontalInterpolation.Enabled)
				{
					mHorizontalInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					mHorizontalInterpolation.Timestep = timestep;
				}
				else
				{
					_mHorizontal = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_mHorizontal(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (mVerticalInterpolation.Enabled)
				{
					mVerticalInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					mVerticalInterpolation.Timestep = timestep;
				}
				else
				{
					_mVertical = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_mVertical(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (isRotatingLeftInterpolation.Enabled)
				{
					isRotatingLeftInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					isRotatingLeftInterpolation.Timestep = timestep;
				}
				else
				{
					_isRotatingLeft = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_isRotatingLeft(timestep);
				}
			}
			if ((0x20 & readDirtyFlags[0]) != 0)
			{
				if (isRotatingRightInterpolation.Enabled)
				{
					isRotatingRightInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					isRotatingRightInterpolation.Timestep = timestep;
				}
				else
				{
					_isRotatingRight = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_isRotatingRight(timestep);
				}
			}
			if ((0x40 & readDirtyFlags[0]) != 0)
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
			if (rotationInterpolation.Enabled && !rotationInterpolation.current.UnityNear(rotationInterpolation.target, 0.0015f))
			{
				_rotation = (Quaternion)rotationInterpolation.Interpolate();
				//RunChange_rotation(rotationInterpolation.Timestep);
			}
			if (mHorizontalInterpolation.Enabled && !mHorizontalInterpolation.current.UnityNear(mHorizontalInterpolation.target, 0.0015f))
			{
				_mHorizontal = (float)mHorizontalInterpolation.Interpolate();
				//RunChange_mHorizontal(mHorizontalInterpolation.Timestep);
			}
			if (mVerticalInterpolation.Enabled && !mVerticalInterpolation.current.UnityNear(mVerticalInterpolation.target, 0.0015f))
			{
				_mVertical = (float)mVerticalInterpolation.Interpolate();
				//RunChange_mVertical(mVerticalInterpolation.Timestep);
			}
			if (isRotatingLeftInterpolation.Enabled && !isRotatingLeftInterpolation.current.UnityNear(isRotatingLeftInterpolation.target, 0.0015f))
			{
				_isRotatingLeft = (bool)isRotatingLeftInterpolation.Interpolate();
				//RunChange_isRotatingLeft(isRotatingLeftInterpolation.Timestep);
			}
			if (isRotatingRightInterpolation.Enabled && !isRotatingRightInterpolation.current.UnityNear(isRotatingRightInterpolation.target, 0.0015f))
			{
				_isRotatingRight = (bool)isRotatingRightInterpolation.Interpolate();
				//RunChange_isRotatingRight(isRotatingRightInterpolation.Timestep);
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

		public ThiefNetworkObject() : base() { Initialize(); }
		public ThiefNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public ThiefNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
