﻿using System;
using System.Runtime.Serialization;

namespace Xilion.Models.Core
{
    public class HierarchyException : CmsException
    {
        public HierarchyException()
        {
        }

        public HierarchyException(string message) : base(message)
        {
        }

        public HierarchyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HierarchyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}