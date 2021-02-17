using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer.Models.DBModels
{
    [Index(nameof(GroupId), nameof(UserId), Name = "UNIQUE_MEMBERS", IsUnique = true)]
    public partial class GroupMember
    {
        [Key]
        [Column("MemberID")]
        public int MemberId { get; set; }

        [Column("GroupID")]
        public int GroupId { get; set; }

        [Column("UserID")]
        public int UserId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(GroupId))]
        [InverseProperty("GroupMembers")]
        public virtual Group Group { get; set; }
    }
}
