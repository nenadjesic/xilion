using System;

namespace Xilion.Models.Core.Exceptions
{
    /// <summary>
    /// Alias exception
    /// </summary>
    public class AliasException : Exception
    {

        public AliasException()
        {
        }

        public AliasException(string message)
            : base(message)
        {
        }

        public AliasException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

        public AliasException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

    }
}