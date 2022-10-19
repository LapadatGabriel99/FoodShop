using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShop.Integration.ServiceBus.Messages
{
    public class BaseMessage
    {
        public BaseMessage()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
        }

        public BaseMessage(Guid id, DateTime createdAt)
        {
            Id = id.ToString();
            CreatedAt = createdAt;
        }

        public string Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
