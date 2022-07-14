using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Data.Models.ViewModels
{
    public class UserDetailsViewModel
    {
        public string Id { get; set; }

        public SelectList ApplicationRoles { get; set; }

        public string UserRoleId { get; set; }

        public string CurrentUserRole { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
