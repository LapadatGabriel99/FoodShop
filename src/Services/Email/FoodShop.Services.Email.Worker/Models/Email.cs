using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodShop.Services.Email.Worker.Models
{
    internal class EmailModel
    {
        public EmailModel()
        {
            Recipients = new List<string>();
        }

        public List<string> Recipients { get; set; }

        public string Body { get; set; }

        public string Subject { get; set; }
    }
}
