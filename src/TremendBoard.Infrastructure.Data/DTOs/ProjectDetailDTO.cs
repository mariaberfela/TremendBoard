using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Data.DTOs
{
    public class ProjectDetailDTO
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectStatus { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime Deadline { get; set; }
        public string StatusMessage { get; set; }

        public List<ProjectUserDetailViewDTO> ProjectUsers { get; set; }
        public IEnumerable<UserDetailDTO> Users { get; set; }
        public IEnumerable<RoleDetailViewDTO> Roles { get; set; }

    }
}
