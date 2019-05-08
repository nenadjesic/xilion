using System;
using Xilion.Models.Messages.Domain;
using Xilion.Models.Messages.Services;
using Xilion.Models.Notifications.Definitions;
using StructureMap;

namespace Xilion.Models.Messages.Notifications
{
    public class MessageSendNotification : NotificationDefinition
    {
        private readonly MessageService _messageService;
        private static  Container _container;

        private Message _message;

        public MessageSendNotification(MessageService messageService , Container container)
        {
            _messageService = messageService;
            _container = container;
        }

        public long MessageID
        {
            protected get { return Data.GetValue<long>("MessageID"); }
            set { Data.SetValue("MessageID", value); }
        }

        public Message Message
        {
            get
            {
                if(_message == null)
                {
                    _message = _messageService.GetById(MessageID);
                    if(_message == null)
                        return new Message();

                }
               return _message;
            }
        }

        public static MessageSendNotification Create()
        {
            return _container.GetInstance<MessageSendNotification>();
        }
    }
}