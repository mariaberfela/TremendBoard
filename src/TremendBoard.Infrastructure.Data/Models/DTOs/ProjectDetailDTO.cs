using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Data.Models.ViewModels
{
    public class ProjectDetailDTO
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProjectStatus { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd", ApplyFormatInEditMode = true)]
        public DateTime Deadline { get; set; }
        public string StatusMessage { get; set; }

        public IList<ProjectUserDetailDTO> ProjectUsers { get; set; }
        public IEnumerable<UserDetailDTO> Users { get; set; }
        public IEnumerable<ApplicationRoleDetailDTO> Roles { get; set; }
    }
}
