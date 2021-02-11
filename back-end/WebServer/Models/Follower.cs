using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer.Models
{
    [Index(nameof(UserId), nameof(FollowerId), Name = "UNIQUE_RELATION", IsUnique = true)]
    public partial class Follower
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("FollowerID")]
        public int FollowerId { get; set; }
        public int? Pending { get; set; }

        [ForeignKey(nameof(FollowerId))]
        [InverseProperty("FollowerFollowerNavigations")]
        public virtual User FollowerNavigation { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty("FollowerUsers")]
        public virtual User User { get; set; }
    }
}
