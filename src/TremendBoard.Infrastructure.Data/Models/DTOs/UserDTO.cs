using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Data.Models.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }

        public SelectList ApplicationRoles { get; set; }

        [Display(Name = "Role")]
        public string UserRoleId { get; set; }

        public string CurrentUserRole { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string LastName { get; set; }

        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
