using System;
using System.Collections.Generic;
using System.Linq;
using Xilion.Models.Core;
using Xilion.Models.Messages.Data;
using Xilion.Models.Messages.Domain;
using Xilion.Models.Messages.Notifications;
using Xilion.Models.Notifications;
using Xilion.Models.Notifications.Data;
using Xilion.Models.User.Data;
using Users = Xilion.Models.Users;


namespace Xilion.Models.Messages.Services
{
    public class MessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _usersRepository;
        private readonly IMessageStateRepository _messageStateRepository;
        private readonly NotificationService _notificationRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository usersRepository, IMessageStateRepository messageStateRepository, NotificationService notificationRepository )
        {
            _messageRepository = messageRepository;
            _usersRepository = usersRepository;
            _messageStateRepository = messageStateRepository;
            _notificationRepository = notificationRepository;
        }

        public void NotifyMe(Message message, Users sender)
        {
            MessageSendNotification notification =  MessageSendNotification.Create();
            notification.MessageID = message.Id;

            _notificationRepository.Notify(sender, notification, _usersRepository.GetAll().ToList());
        }

        /// <summary>
        /// Get message by its unique identifier.
        /// </summary>
        /// <param name="id">Messages unique identifier.</param>
        public Message GetById(long id)
        {
            return _messageRepository.GetById(id);
        }

        /// <summary>
        /// Send message to all conversation members.
        /// </summary>
        /// <param name="message">Message object.</param>
        public void SendMessage(Message message)
        {
            _messageRepository.Save(message);
        }

        /// <summary>
        /// Get conversation messages for member.
        /// </summary>
        /// <param name="conversation">Conversation object.</param>
        /// <param name="member">Conversation member to get messages for.</param>
        /// <param name="includePrevious">Value indicates if all previous messages will be shown.</param>
        /// <param name="referentMesasgeId">If defined only messages newer than this one will be returned. </param>
        /// <returns>List of member messages.</returns>
        public IList<Message> GetMemberMessages(Conversation conversation, ConversationMember member, long referentMesasgeId,bool includePrevious = false)
        {
            if (conversation.Members.Contains(member))
            {
                var messages = _messageRepository.Query().Where(x => x.Conversation == conversation);

                if (!includePrevious)
                    messages = messages.Where(x => x.CreatedOn > member.CreatedOn);

                messages = messages.OrderByDescending(x => x.CreatedOn);
                if (referentMesasgeId != 0)
                {
                    var referentMessage = conversation.Messages.SingleOrDefault(x => x.Id == referentMesasgeId);
                    if (referentMessage != null)
                        messages = messages.Where(x => x.CreatedOn > referentMessage.CreatedOn);
                }

                return messages.ToList();
            }

            throw new CmsException("Users is not member of this conversation.");
        }

        /// <summary>
        /// Mark a message as read adding new MessageState or update existing setting its parameter IsRead to true.
        /// </summary>
        /// <param name="message">Message object.</param>
        /// <param name="member">Member object.</param>
        public void SetAsRead(Message message, ConversationMember member)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            if (member == null)
                throw new ArgumentNullException("member");

            var ms =
                _messageStateRepository.Query().SingleOrDefault(x => x.Member == member && x.Message == message);
            if (ms == null)
            {
                ms = new MessageState
                         {
                             IsRead = true,
                             Member = member,
                             Message = message,
                             ReadDate = DateTime.Now
                         };
            }
            else
            {
                ms.IsRead = true;
                ms.ReadDate = DateTime.Now;
            }
            _messageStateRepository.Save(ms);
        }

        /// <summary>
        /// Mark message as unreaded setting its parameter IsRead to false.
        /// </summary>
        /// <param name="message">Message object.</param>
        /// <param name="member">Conversation member object.</param>
        public void SetAsUnread(Message message, ConversationMember member)
        {
            var ms =
                _messageStateRepository.Query().SingleOrDefault(x => x.Member == member && x.Message == message);
            if (ms != null)
            {
                ms.IsRead = false;
                _messageStateRepository.Save(ms);
            }
        }


    }
}