using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;


namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenEndDate { get; set; }

        // Список книг, которые взял пользователь
        public ICollection<UserBook> UserBooks { get; set; }
    }
}
