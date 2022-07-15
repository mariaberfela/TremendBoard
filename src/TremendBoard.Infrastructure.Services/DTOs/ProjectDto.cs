using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Services.DTOs
{
    public class ProjectDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusMessage { get; set; }

        //[DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Deadline { get; set; }
        public string ProjectStatus { get; set; }

        public IList<ProjectUserDto> ProjectUsers { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}
