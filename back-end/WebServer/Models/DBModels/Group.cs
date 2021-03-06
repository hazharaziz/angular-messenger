﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer.Models.DBModels
{
    public partial class Group
    {
        public Group()
        {
            GroupMembers = new HashSet<GroupMember>();
            GroupMessages = new HashSet<GroupMessage>();
        }

        [Key]
        [JsonPropertyName("id")]
        [Column("GroupID")]
        public int GroupId { get; set; }

        [Required]
        [JsonPropertyName("groupName")]
        [StringLength(40, MinimumLength = 3)]
        public string GroupName { get; set; }

        [JsonPropertyName("creatorId")]
        [Column("CreatorID")]
        public int CreatorId { get; set; }

        [JsonPropertyName("creatorName")]
        [StringLength(40, MinimumLength = 3)]
        public string CreatorName { get; set; }

        [JsonPropertyName("membersCount")]
        public int MembersCount { get; set; }

        [JsonPropertyName("addMemberAccess")]
        [Range(typeof(int), "0", "1")]
        public int AddMemberAccess { get; set; } = 0;

        [JsonIgnore]
        [InverseProperty(nameof(GroupMember.Group))]
        public virtual ICollection<GroupMember> GroupMembers { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(GroupMessage.Group))]
        public virtual ICollection<GroupMessage> GroupMessages { get; set; }
    }
}