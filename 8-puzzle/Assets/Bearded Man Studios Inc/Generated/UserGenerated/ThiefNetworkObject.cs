using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0.15,0.15,0,0]")]
	public partial class ThiefNetworkObject : NetworkObject
	{
		public const int IDENTITY = 10;

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
		private float _m_Horizontal;
		public event FieldEvent<float> m_HorizontalChanged;
		public InterpolateFloat m_HorizontalInterpolation = new InterpolateFloat() { LerpT = 0.15f, Enabled = true };
		public float m_Horizontal
		{
			get { return _m_Horizontal; }
			set
			{
				// Don't do anything if the value is the same
				if (_m_Horizontal == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_m_Horizontal = value;
				hasDirtyFields = true;
			}
		}

		public void Setm_HorizontalDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_m_Horizontal(ulong timestep)
		{
			if (m_HorizontalChanged != null) m_HorizontalChanged(_m_Horizontal, timestep);
			if (fieldAltered != null) fieldAltered("m_Horizontal", _m_Horizontal, timestep);
		}
		[ForgeGeneratedField]
		private float _m_Vertical;
		public event FieldEvent<float> m_VerticalChanged;
		public InterpolateFloat m_VerticalInterpolation = new InterpolateFloat() { LerpT = 0.15f, Enabled = true };
		public float m_Vertical
		{
			get { return _m_Vertical; }
			set
			{
				// Don't do anything if the value is the same
				if (_m_Vertical == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_m_Vertical = value;
				hasDirtyFields = true;
			}
		}

		public void Setm_VerticalDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_m_Vertical(ulong timestep)
		{
			if (m_VerticalChanged != null) m_VerticalChanged(_m_Vertical, timestep);
			if (fieldAltered != null) fieldAltered("m_Vertical", _m_Vertical, timestep);
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

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			rotationInterpolation.current = rotationInterpolation.target;
			m_HorizontalInterpolation.current = m_HorizontalInterpolation.target;
			m_VerticalInterpolation.current = m_VerticalInterpolation.target;
			isRotatingLeftInterpolation.current = isRotatingLeftInterpolation.target;
			isRotatingRightInterpolation.current = isRotatingRightInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _m_Horizontal);
			UnityObjectMapper.Instance.MapBytes(data, _m_Vertical);
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
			_m_Horizontal = UnityObjectMapper.Instance.Map<float>(payload);
			m_HorizontalInterpolation.current = _m_Horizontal;
			m_HorizontalInterpolation.target = _m_Horizontal;
			RunChange_m_Horizontal(timestep);
			_m_Vertical = UnityObjectMapper.Instance.Map<float>(payload);
			m_VerticalInterpolation.current = _m_Vertical;
			m_VerticalInterpolation.target = _m_Vertical;
			RunChange_m_Vertical(timestep);
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
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _m_Horizontal);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _m_Vertical);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _isRotatingLeft);
			if ((0x20 & _dirtyFields[0]) != 0)
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
				if (m_HorizontalInterpolation.Enabled)
				{
					m_HorizontalInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					m_HorizontalInterpolation.Timestep = timestep;
				}
				else
				{
					_m_Horizontal = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_m_Horizontal(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (m_VerticalInterpolation.Enabled)
				{
					m_VerticalInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					m_VerticalInterpolation.Timestep = timestep;
				}
				else
				{
					_m_Vertical = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_m_Vertical(timestep);
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
			if (m_HorizontalInterpolation.Enabled && !m_HorizontalInterpolation.current.UnityNear(m_HorizontalInterpolation.target, 0.0015f))
			{
				_m_Horizontal = (float)m_HorizontalInterpolation.Interpolate();
				//RunChange_m_Horizontal(m_HorizontalInterpolation.Timestep);
			}
			if (m_VerticalInterpolation.Enabled && !m_VerticalInterpolation.current.UnityNear(m_VerticalInterpolation.target, 0.0015f))
			{
				_m_Vertical = (float)m_VerticalInterpolation.Interpolate();
				//RunChange_m_Vertical(m_VerticalInterpolation.Timestep);
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
				readDirtyFlags = new byte[1];

		}

		public ThiefNetworkObject() : base() { Initialize(); }
		public ThiefNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public ThiefNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
