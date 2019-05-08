using System.Linq;
using Xilion.Models.Messages.Domain;

namespace Xilion.Models.Messages.Extensions
{
    public static class ConversationExtensions
    {
        /// <summary>
        ///   Get conversation member
        /// </summary>
        /// <param name="conversation"> Conversatin this member belongs to. </param>
        /// <param name="Users"> Cms Users representation of the member. </param>
        /// <returns> </returns>
        public static ConversationMember GetMember(this Conversation conversation, Users Users)
        {
            return conversation.Members.SingleOrDefault(x => x.Users == Users);
        }

        /// <summary>
        ///   Get conversation member
        /// </summary>
        /// <param name="conversation"> Conversatin this member belongs to. </param>
        /// <param name="Usersname"> Users unique name. </param>
        /// <returns> </returns>
        public static ConversationMember GetMember(this Conversation conversation, string Usersname)
        {
            return conversation.Members.SingleOrDefault(x => x.Users.UserName == Usersname);
        }
    }
}