using System;

namespace Api.Models.UserInteraction
{
    public enum InteractionType
    {
        NotificationRead,

    }
    public class UserInteraction : AbstractDbEntity
    {
        public DateTime Time { get; set; }

        public InteractionType Type { get; set; }

    }
}
