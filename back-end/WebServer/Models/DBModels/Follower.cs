using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer.Models.DBModels
{
    [Index(nameof(UserId), nameof(FollowerId), Name = "UNIQUE_RELATION", IsUnique = true)]
    public partial class Follower
    {
        [Key]
        [Column("RelationID")]
        public int RelationId { get; set; }

        [Column("UserID")]
        public int UserId { get; set; }

        [Column("FollowerID")]
        public int FollowerId { get; set; }
        public int? Pending { get; set; }
    }
}
