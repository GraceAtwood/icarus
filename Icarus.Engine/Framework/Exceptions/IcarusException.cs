using System;

namespace Icarus.Engine.Framework.Exceptions
{
    public class IcarusException : Exception
    {
        public IcarusException()
        {
        }

        public IcarusException(string message) : base(message)
        {
        }

        public IcarusException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}