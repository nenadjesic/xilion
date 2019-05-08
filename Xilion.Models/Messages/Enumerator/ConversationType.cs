using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilion.Framework;

namespace Xilion.Models.Messages.Enumerator
{
    public class ConversationType : Enumeration
    {
        public static readonly ConversationType Private = new ConversationType(0, "Private");
        public static readonly ConversationType Group = new ConversationType(1, "Group");
 
        public ConversationType(int value, string name, string displayName) : base(value, name, displayName)
        {
        }

        public ConversationType(int value, string name) : base(value, name)
        {
        }
    }
}
