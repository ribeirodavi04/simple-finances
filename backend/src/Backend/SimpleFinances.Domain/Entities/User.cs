using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFinances.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public Guid UserGuid { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ProfilePhotoURL { get; set; } = string.Empty;

    }
}
