using System;
using System.Runtime.Serialization;

namespace tabuleiro
{
    [Serializable]
    internal class TabuleiroException : Exception
    {
        public TabuleiroException()
        {
        }

        public TabuleiroException(string message) : base(message)
        {
        }

        public TabuleiroException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TabuleiroException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}