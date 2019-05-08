using System;
using System.Collections.Generic;
using NHibernate.Search.Attributes;
using Xilion.Models.Classifications;
using Xilion.Models.Core.Data;
using Xilion.Models.Core;
using Xilion.Framework;
using Xilion.Models.Core.Domain;

namespace Xilion.Models.Events
{
    public class EventSubscriptionStatus : Enumeration
    {
        public static readonly EventSubscriptionStatus Pending = new EventSubscriptionStatus(0, "Pending");
        public static readonly EventSubscriptionStatus Approved = new EventSubscriptionStatus(1, "Approved");
        public static readonly EventSubscriptionStatus Declined = new EventSubscriptionStatus(2, "Declined");
        public static readonly EventSubscriptionStatus Suspended = new EventSubscriptionStatus(3, "Suspended");

        public EventSubscriptionStatus(int value, string displayName)
            : base(value, displayName)
        {
        }
    }
}