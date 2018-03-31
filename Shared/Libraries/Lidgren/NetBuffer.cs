using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lidgren.Network
{
	public partial class NetBuffer
	{
		/// <summary>
		/// Number of bytes to overallocate for each message to avoid resizing
		/// </summary>
		protected const int c_overAllocateAmount = 4;

		private static readonly Dictionary<Type, MethodInfo> s_readMethods;
		private static readonly Dictionary<Type, MethodInfo> s_writeMethods;

		internal byte[] m_data;
		internal int m_bitLength;
		internal int m_readPosition;

		/// <summary>
		/// Gets or sets the internal data buffer
		/// </summary>
		public byte[] Data
		{
			get { return m_data; }
			set { m_data = value; }
		}

		/// <summary>
		/// Gets or sets the length of the used portion of the buffer in bytes
		/// </summary>
		public int LengthBytes
		{
			get { return ((m_bitLength + 7) >> 3); }
			set
			{
				m_bitLength = value * 8;
				InternalEnsureBufferSize(m_bitLength);
			}
		}

		/// <summary>
		/// Gets or sets the length of the used portion of the buffer in bits
		/// </summary>
		public int LengthBits
		{
			get { return m_bitLength; }
			set
			{
				m_bitLength = value;
				InternalEnsureBufferSize(m_bitLength);
			}
		}

		/// <summary>
		/// Gets or sets the read position in the buffer, in bits (not bytes)
		/// </summary>
		public long Position
		{
			get { return (long)m_readPosition; }
			set { m_readPosition = (int)value; }
		}

		/// <summary>
		/// Gets the position in the buffer in bytes; note that the bits of the first returned byte may already have been read - check the Position property to make sure.
		/// </summary>
		public int PositionInBytes
		{
			get { return (int)(m_readPosition / 8); }
		}
		
		static NetBuffer()
		{
			s_readMethods = new Dictionary<Type, MethodInfo>();
			MethodInfo[] methods = typeof(NetIncomingMessage).GetMethods(BindingFlags.Instance | BindingFlags.Public);
			foreach (MethodInfo mi in methods)
			{
				if (mi.GetParameters().Length == 0 && mi.Name.StartsWith("Read", StringComparison.InvariantCulture) && mi.Name.Substring(4) == mi.ReturnType.Name)
				{
					s_readMethods[mi.ReturnType] = mi;
				}
			}

			s_writeMethods = new Dictionary<Type, MethodInfo>();
			methods = typeof(NetOutgoingMessage).GetMethods(BindingFlags.Instance | BindingFlags.Public);
			foreach (MethodInfo mi in methods)
			{
				if (mi.Name.Equals("Write", StringComparison.InvariantCulture))
				{
					ParameterInfo[] pis = mi.GetParameters();
					if (pis.Length == 1)
						s_writeMethods[pis[0].ParameterType] = mi;
				}
			}
		}

        // Serializes an object into the out going message.
        public void PackObjectInto(NetOutgoingMessage outMsg, object obj)
        {
            byte[] objBytes = SerializeObject(obj);

            outMsg.Write(objBytes.Length);
            outMsg.Write(objBytes);
        }

        // Decodes the next object stored in the in message.
        public object UnpackObjectFrom(NetIncomingMessage inMsg)
        {
            int objectLength = inMsg.ReadInt32();
            byte[] objectBytes = inMsg.ReadBytes(objectLength);

            return DeserializeObject(objectBytes);
        }

        // Convert an object that has been marked as [Serializable] into a byte array.
        byte[] SerializeObject(object obj)
        {
            BinaryFormatter binFormatter = new BinaryFormatter();

            using (MemoryStream memStream = new MemoryStream())
            {
                binFormatter.Serialize(memStream, obj);
                return memStream.ToArray();
            }
        }

        // Convert a byte array back into the original object it was.
        object DeserializeObject(byte[] bytes)
        {
            BinaryFormatter binFormatter = new BinaryFormatter();

            using (MemoryStream memStream = new MemoryStream())
            {
                memStream.Write(bytes, 0, bytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                return binFormatter.Deserialize(memStream);
            }
        }
    }
}
