using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TremendBoard.Infrastructure.Services.DTOs
{
    public class ProjectDetailDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusMessage { get; set; }
        public string ProjectStatus { get; set; }
        public DateTime Deadline { get; set; }
    }
}
