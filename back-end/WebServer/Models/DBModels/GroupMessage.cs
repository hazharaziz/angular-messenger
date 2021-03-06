﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer.Models.DBModels
{
    public partial class GroupMessage
    {
        [Key]
        [JsonPropertyName("id")]
        [Column("GroupMessageID")]
        public int GroupMessageId { get; set; }

        [JsonPropertyName("groupId")]
        [Column("GroupID")]
        public int GroupId { get; set; }

        [JsonPropertyName("replyToId")]
        [Column("ReplyToID")]
        public int ReplyToId { get; set; }

        [JsonPropertyName("composerId")]
        [Column("ComposerID")]
        public int ComposerId { get; set; }

        [Required]
        [JsonPropertyName("text")]
        [StringLength(400)]
        public string Text { get; set; }

        [JsonPropertyName("composerName")]
        [StringLength(40, MinimumLength = 3)]
        public string ComposerName { get; set; }

        [JsonPropertyName("dateTime")]
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(GroupId))]
        [InverseProperty("GroupMessages")]
        public virtual Group Group { get; set; }
    }
}