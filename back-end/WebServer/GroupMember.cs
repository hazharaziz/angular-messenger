using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer
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

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("GroupMembers")]
        public virtual Group Group { get; set; }
    }
}
