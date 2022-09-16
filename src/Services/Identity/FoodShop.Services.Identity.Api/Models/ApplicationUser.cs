using Microsoft.AspNetCore.Identity;

namespace FoodShop.Services.Identity.Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }
    }
}
