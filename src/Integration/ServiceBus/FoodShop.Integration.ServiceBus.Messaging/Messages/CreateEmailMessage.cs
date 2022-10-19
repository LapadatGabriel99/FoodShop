using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShop.Integration.ServiceBus.Messages
{
    public class CreateEmailMessage : BaseMessage
    {
        public string UserEmail { get; set; }

        public string Reason { get; set; }

        public string Content { get; set; }
    }
}
