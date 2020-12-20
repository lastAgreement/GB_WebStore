using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
    public class User: IdentityUser
    {
        public const string Administrator = "Administrator";
        public const string DefaultAdminPassword = "AdPass";

        public string Description { get; set; }
    }

    public class Role : IdentityRole
    {
        public const string Administrator = "Administrators";
        public const string User = "Users";

        public string Description { get; set; }

    }
}
