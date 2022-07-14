﻿using System.ComponentModel.DataAnnotations;

namespace TremendBoard.Infrastructure.Data.Models.DTO
{
    public class ApplicationRoleDTO
    {
        public string Id { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string UserRoleName { get; set; }
        public string StatusMessage { get; set; }
    }
}
