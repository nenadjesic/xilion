﻿using System;
using System.Runtime.Serialization;

namespace Xilion.Models.Core
{
    public class CmsException : Exception
    {
        public CmsException()
        {
        }

        public CmsException(string message) : base(message)
        {
        }

        public CmsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CmsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}