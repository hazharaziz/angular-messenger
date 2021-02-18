using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer
{
    public partial class Group
    {
        public Group()
        {
            GroupMembers = new HashSet<GroupMember>();
            GroupMessages = new HashSet<GroupMessage>();
        }

        [Key]
        [Column("GroupID")]
        public int GroupId { get; set; }
        [Required]
        [StringLength(40)]
        public string GroupName { get; set; }
        [Column("CreatorID")]
        public int CreatorId { get; set; }
        [Required]
        [StringLength(40)]
        public string CreatorName { get; set; }

        [InverseProperty(nameof(GroupMember.Group))]
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        [InverseProperty(nameof(GroupMessage.Group))]
        public virtual ICollection<GroupMessage> GroupMessages { get; set; }
    }
}
