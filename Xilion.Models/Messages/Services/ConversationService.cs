using System;
using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Messages.Data;
using Xilion.Models.Messages.Domain;
using Xilion.Models.User.Data;

namespace Xilion.Models.Messages.Services
{
    public class ConversationService
    {
        private readonly IConversationMemberRepository _conversationMemberRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserRepository _usersRepository;

        public ConversationService(IConversationRepository conversationRepository,
                                   IConversationMemberRepository conversationMemberRepository,
                                   IUserRepository usersRepository)
        {
            _conversationRepository = conversationRepository;
            _conversationMemberRepository = conversationMemberRepository;
            _usersRepository = usersRepository;
        }

        /// <summary>
        ///   Get all conversations for Users.
        /// </summary>
        /// <param name="Users"> Users object to get conversations for. </param>
        /// <returns> List of Users conversations. </returns>
        public IList<Conversation> GetConversationsByUsers(Users Users)
        {
            if (Users == null)
                throw new ArgumentNullException("Users");

            return _conversationRepository.Query().Where(x => x.Members.Any(y => y.Users == Users)).ToList();
        }


        /// <summary>
        ///   Create new conversation and assign members.
        /// </summary>
        /// <param name="Users"> List of Users to be assigned as a members. </param>
        /// <returns> Conversation instance </returns>
        public Conversation CreateConversation(IList<Users> users)
        {
            var conversation = new Conversation();

            foreach (var user in users)
            {
                var member = new ConversationMember
                                 {
                                     Conversation = conversation,
                                     Users = user
                                 };

                conversation.Members.Add(member);
            }
            _conversationRepository.Save(conversation);
            return conversation;
        }

        /// <summary>
        ///   Create new conversation and assign members.
        /// </summary>
        /// <param name="ids"> List of Users unique identifiers. </param>
        /// <returns> Newly created conversation object. </returns>
        public Conversation CreateConversation(IList<long> ids)
        {
            var Users = _usersRepository.Query().Where(x => ids.Contains(x.Id)).ToList();
            return CreateConversation(Users);
        }

        /// <summary>
        ///   Get conversation by its unique identifier.
        /// </summary>
        /// <param name="id"> Unique identifier for conversation. </param>
        /// <returns> Conversation object or null. </returns>
        public Conversation GetById(long id)
        {
            return _conversationRepository.GetById(id);
        }

        /// <summary>
        ///   Remove member from conversation by setting its parameter IsLeft to true.
        /// </summary>
        /// <param name="conversation"> Conversation object. </param>
        /// <param name="member"> Member object. </param>
        public void Leave(Conversation conversation, ConversationMember member)
        {
            if (conversation == null)
                throw new ArgumentNullException("conversation");

            if (member == null)
                throw new ArgumentNullException("member");

            if (!member.IsLeaved)
                member.IsLeaved = true;

            _conversationMemberRepository.Save(member);
        }

        /// <summary>
        ///   Remove member from conversation by setting its parameter IsLeft to true.
        /// </summary>
        /// <param name="conversationId"> Conversation unique identifier </param>
        /// <param name="memberId"> Member unique identifier. </param>
        public void Leave(long conversationId, long memberId)
        {
            var conversation = _conversationRepository.GetById(conversationId);
            if (conversation != null)
            {
                var member = conversation.Members.SingleOrDefault(x => x.Id == memberId);
                Leave(conversation, member);
            }
        }

        /// <summary>
        ///   Add member to conversation by creating presisetns or setting its parameter IsLeft to false
        /// </summary>
        /// <param name="conversation"> Conversation object. </param>
        /// <param name="Users"> Users object. </param>
        public void Join(Conversation conversation, Users Users)
        {
            if (conversation == null)
                throw new ArgumentNullException("conversation");

            if (Users == null)
                throw new ArgumentNullException("Users");

            var member = conversation.Members.SingleOrDefault(x => x.Users == Users);
            if (member == null)
            {
                member = new ConversationMember
                             {
                                 Conversation = conversation,
                                 Users = Users
                             };
                conversation.Members.Add(member);
            }
            else
                member.IsLeaved = false;

            _conversationMemberRepository.Save(member);
        }

        /// <summary>
        ///   Add member to conversation by creating presisetns or setting its parameter IsLeft to false
        /// </summary>
        /// <param name="conversationID"> Conversation unique identifier. </param>
        /// <param name="UsersID"> Users unique identifier. </param>
        public void Join(long conversationID, long UsersID)
        {
            var conversation = _conversationRepository.GetById(conversationID);

            if (conversation != null)
            {
                var Users = _usersRepository.GetById(UsersID);
                Join(conversation, Users);
            }
        }
    }
}