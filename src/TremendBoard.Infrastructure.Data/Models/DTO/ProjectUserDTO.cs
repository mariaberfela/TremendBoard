using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Data.Models.DTO
{
    public class ProjectUserDTO
    {
        [Required]
        public string ProjectId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRoleName { get; set; }
    }
}
