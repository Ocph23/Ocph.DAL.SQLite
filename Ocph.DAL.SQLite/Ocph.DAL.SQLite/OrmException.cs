using System;
using System.Runtime.Serialization;

namespace vJine.Core.ORM
{
    [Serializable]
    internal class OrmException : Exception
    {
        private string v1;
        private object v2;

        public OrmException()
        {
        }

        public OrmException(string message) : base(message)
        {
        }

        public OrmException(string v1, object v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public OrmException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OrmException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}