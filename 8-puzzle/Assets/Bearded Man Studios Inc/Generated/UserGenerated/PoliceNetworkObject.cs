using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0,0,0,0.15,0.15,0.15,0,0]")]
	public partial class PoliceNetworkObject : NetworkObject
	{
		public const int IDENTITY = 7;

		private byte[] _dirtyFields = new byte[2];

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
		private int _item1Num;
		public event FieldEvent<int> item1NumChanged;
		public Interpolated<int> item1NumInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int item1Num
		{
			get { return _item1Num; }
			set
			{
				// Don't do anything if the value is the same
				if (_item1Num == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_item1Num = value;
				hasDirtyFields = true;
			}
		}

		public void Setitem1NumDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_item1Num(ulong timestep)
		{
			if (item1NumChanged != null) item1NumChanged(_item1Num, timestep);
			if (fieldAltered != null) fieldAltered("item1Num", _item1Num, timestep);
		}
		[ForgeGeneratedField]
		private int _item2Num;
		public event FieldEvent<int> item2NumChanged;
		public Interpolated<int> item2NumInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int item2Num
		{
			get { return _item2Num; }
			set
			{
				// Don't do anything if the value is the same
				if (_item2Num == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_item2Num = value;
				hasDirtyFields = true;
			}
		}

		public void Setitem2NumDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_item2Num(ulong timestep)
		{
			if (item2NumChanged != null) item2NumChanged(_item2Num, timestep);
			if (fieldAltered != null) fieldAltered("item2Num", _item2Num, timestep);
		}
		[ForgeGeneratedField]
		private int _item3Num;
		public event FieldEvent<int> item3NumChanged;
		public Interpolated<int> item3NumInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int item3Num
		{
			get { return _item3Num; }
			set
			{
				// Don't do anything if the value is the same
				if (_item3Num == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_item3Num = value;
				hasDirtyFields = true;
			}
		}

		public void Setitem3NumDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_item3Num(ulong timestep)
		{
			if (item3NumChanged != null) item3NumChanged(_item3Num, timestep);
			if (fieldAltered != null) fieldAltered("item3Num", _item3Num, timestep);
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
				_dirtyFields[0] |= 0x20;
				_cameraRotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetcameraRotationDirty()
		{
			_dirtyFields[0] |= 0x20;
			hasDirtyFields = true;
		}

		private void RunChange_cameraRotation(ulong timestep)
		{
			if (cameraRotationChanged != null) cameraRotationChanged(_cameraRotation, timestep);
			if (fieldAltered != null) fieldAltered("cameraRotation", _cameraRotation, timestep);
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
				_dirtyFields[0] |= 0x40;
				_mHorizontal = value;
				hasDirtyFields = true;
			}
		}

		public void SetmHorizontalDirty()
		{
			_dirtyFields[0] |= 0x40;
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
				_dirtyFields[0] |= 0x80;
				_mVertical = value;
				hasDirtyFields = true;
			}
		}

		public void SetmVerticalDirty()
		{
			_dirtyFields[0] |= 0x80;
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
				_dirtyFields[1] |= 0x1;
				_isRotatingLeft = value;
				hasDirtyFields = true;
			}
		}

		public void SetisRotatingLeftDirty()
		{
			_dirtyFields[1] |= 0x1;
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
				_dirtyFields[1] |= 0x2;
				_isRotatingRight = value;
				hasDirtyFields = true;
			}
		}

		public void SetisRotatingRightDirty()
		{
			_dirtyFields[1] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_isRotatingRight(ulong timestep)
		{
			if (isRotatingRightChanged != null) isRotatingRightChanged(_isRotatingRight, timestep);
			if (fieldAltered != null) fieldAltered("isRotatingRight", _isRotatingRight, timestep);
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
			item1NumInterpolation.current = item1NumInterpolation.target;
			item2NumInterpolation.current = item2NumInterpolation.target;
			item3NumInterpolation.current = item3NumInterpolation.target;
			cameraRotationInterpolation.current = cameraRotationInterpolation.target;
			mHorizontalInterpolation.current = mHorizontalInterpolation.target;
			mVerticalInterpolation.current = mVerticalInterpolation.target;
			isRotatingLeftInterpolation.current = isRotatingLeftInterpolation.target;
			isRotatingRightInterpolation.current = isRotatingRightInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _item1Num);
			UnityObjectMapper.Instance.MapBytes(data, _item2Num);
			UnityObjectMapper.Instance.MapBytes(data, _item3Num);
			UnityObjectMapper.Instance.MapBytes(data, _cameraRotation);
			UnityObjectMapper.Instance.MapBytes(data, _mHorizontal);
			UnityObjectMapper.Instance.MapBytes(data, _mVertical);
			UnityObjectMapper.Instance.MapBytes(data, _isRotatingLeft);
			UnityObjectMapper.Instance.MapBytes(data, _isRotatingRight);

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
			_item1Num = UnityObjectMapper.Instance.Map<int>(payload);
			item1NumInterpolation.current = _item1Num;
			item1NumInterpolation.target = _item1Num;
			RunChange_item1Num(timestep);
			_item2Num = UnityObjectMapper.Instance.Map<int>(payload);
			item2NumInterpolation.current = _item2Num;
			item2NumInterpolation.target = _item2Num;
			RunChange_item2Num(timestep);
			_item3Num = UnityObjectMapper.Instance.Map<int>(payload);
			item3NumInterpolation.current = _item3Num;
			item3NumInterpolation.target = _item3Num;
			RunChange_item3Num(timestep);
			_cameraRotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			cameraRotationInterpolation.current = _cameraRotation;
			cameraRotationInterpolation.target = _cameraRotation;
			RunChange_cameraRotation(timestep);
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
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _item1Num);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _item2Num);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _item3Num);
			if ((0x20 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _cameraRotation);
			if ((0x40 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _mHorizontal);
			if ((0x80 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _mVertical);
			if ((0x1 & _dirtyFields[1]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _isRotatingLeft);
			if ((0x2 & _dirtyFields[1]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _isRotatingRight);

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
				if (item1NumInterpolation.Enabled)
				{
					item1NumInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					item1NumInterpolation.Timestep = timestep;
				}
				else
				{
					_item1Num = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_item1Num(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (item2NumInterpolation.Enabled)
				{
					item2NumInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					item2NumInterpolation.Timestep = timestep;
				}
				else
				{
					_item2Num = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_item2Num(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (item3NumInterpolation.Enabled)
				{
					item3NumInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					item3NumInterpolation.Timestep = timestep;
				}
				else
				{
					_item3Num = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_item3Num(timestep);
				}
			}
			if ((0x20 & readDirtyFlags[0]) != 0)
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
			if ((0x40 & readDirtyFlags[0]) != 0)
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
			if ((0x80 & readDirtyFlags[0]) != 0)
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
			if ((0x1 & readDirtyFlags[1]) != 0)
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
			if ((0x2 & readDirtyFlags[1]) != 0)
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
			if (item1NumInterpolation.Enabled && !item1NumInterpolation.current.UnityNear(item1NumInterpolation.target, 0.0015f))
			{
				_item1Num = (int)item1NumInterpolation.Interpolate();
				//RunChange_item1Num(item1NumInterpolation.Timestep);
			}
			if (item2NumInterpolation.Enabled && !item2NumInterpolation.current.UnityNear(item2NumInterpolation.target, 0.0015f))
			{
				_item2Num = (int)item2NumInterpolation.Interpolate();
				//RunChange_item2Num(item2NumInterpolation.Timestep);
			}
			if (item3NumInterpolation.Enabled && !item3NumInterpolation.current.UnityNear(item3NumInterpolation.target, 0.0015f))
			{
				_item3Num = (int)item3NumInterpolation.Interpolate();
				//RunChange_item3Num(item3NumInterpolation.Timestep);
			}
			if (cameraRotationInterpolation.Enabled && !cameraRotationInterpolation.current.UnityNear(cameraRotationInterpolation.target, 0.0015f))
			{
				_cameraRotation = (Quaternion)cameraRotationInterpolation.Interpolate();
				//RunChange_cameraRotation(cameraRotationInterpolation.Timestep);
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
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[2];

		}

		public PoliceNetworkObject() : base() { Initialize(); }
		public PoliceNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PoliceNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
