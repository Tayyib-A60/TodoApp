using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Server.Data.Entities
{
    [PrimaryKey("ID")]
    public class TodoEntity
    {
        public Guid ID { get; set; }
        // [Required]
        // [ForeignKey("UserID")]
        // public string UserID { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public Guid UserID { get; set; }

    }
}