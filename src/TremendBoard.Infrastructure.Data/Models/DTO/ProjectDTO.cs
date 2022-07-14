using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TremendBoard.Infrastructure.Data.Models.DTO
{
    public class ProjectDTO
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusMessage { get; set; }
        public string ProjectStatus { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Deadline { get; set; }

        public IList<ProjectUserDTO> ProjectUsers { get; set; }
        public IEnumerable<UserDTO> Users { get; set; }
        public IEnumerable<ApplicationRoleDTO> Roles { get; set; }
    }
}
