using mandiri_project.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace mandiri_project.Entities
{
    public partial class ApplicationUser
    {
        [Key]
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string Phone { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string BusinessAreaCode { get; set; } = null!;
        public DateTime TokenExpireDate { get; set; }
        public string? Token { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

    }

}
