using System.Runtime.Serialization;

namespace Xilion.Models.Core.Security
{
    [DataContract]
    public class AccessPermission
    {
        [DataMember]
        public string Role { get; set; }

        [DataMember]
        public int AccessRight { get; set; }

        [DataMember]
        public Access Access { get; set; }
    }
}