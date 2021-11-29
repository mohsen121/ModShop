using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Family { get; set; }

        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public DateTime Created { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
