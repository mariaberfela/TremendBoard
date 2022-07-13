using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Data.Models.Entities
{
    public class ProjectDetail
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusMessage { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ProjectDeadline { get; set; }
        public string ProjectStatus { get; set; }

        public IList<ProjectUserDetail> ProjectUsers { get; set; }
        public IEnumerable<UserDetail> Users { get; set; }
        public IEnumerable<ApplicationRoleDetail> Roles { get; set; }
    }
}
