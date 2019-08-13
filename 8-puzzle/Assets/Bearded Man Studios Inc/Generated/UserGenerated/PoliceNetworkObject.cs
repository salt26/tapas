using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0,0,0]")]
	public partial class PoliceNetworkObject : NetworkObject
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
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _item1Num);
			UnityObjectMapper.Instance.MapBytes(data, _item2Num);
			UnityObjectMapper.Instance.MapBytes(data, _item3Num);

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
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public PoliceNetworkObject() : base() { Initialize(); }
		public PoliceNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PoliceNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
