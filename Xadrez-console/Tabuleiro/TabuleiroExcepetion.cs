using System;
using System.Runtime.Serialization;

namespace tabuleiro
{
    [Serializable]
    internal class TabuleiroExcepetion : Exception
    {
        public TabuleiroExcepetion()
        {
        }

        public TabuleiroExcepetion(string message) : base(message)
        {
        }

        public TabuleiroExcepetion(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TabuleiroExcepetion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}