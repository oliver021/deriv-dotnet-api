using System;
using System.Runtime.Serialization;

namespace OliWorkshop.Deriv
{
    [Serializable]
    internal class SocketStreamException : Exception
    {
        public SocketStreamException()
        {
        }

        public SocketStreamException(string message) : base(message)
        {
        }

        public SocketStreamException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SocketStreamException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}