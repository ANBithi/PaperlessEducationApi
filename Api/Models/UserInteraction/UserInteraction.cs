using System;

namespace Api.Models
{
    public enum InteractionType
    {
        NotificationRead,

    }
    public class UserInteraction : AbstractDbEntity
    {
        public InteractionType Type { get; set; }

    }

}


