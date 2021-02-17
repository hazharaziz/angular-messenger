using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer.Models.DBModels
{
    public partial class Message
    {
        [Key]
        [JsonPropertyName("id")]
        [Column("MessageID")]
        public int MessageId { get; set; }

        [Required]
        [JsonPropertyName("composerId")]
        [Column("ComposerID")]
        public int ComposerId { get; set; }

        [JsonPropertyName("replyToId")]
        [Column("ReplyToID")]
        public int ReplyToId { get; set; } = 0;

        [Required]
        [JsonPropertyName("text")]
        [StringLength(400)]
        public string Text { get; set; }

        [Required]
        [JsonPropertyName("composerName")]
        [StringLength(40, MinimumLength = 3)]
        public string ComposerName { get; set; }

        [Required]
        [JsonPropertyName("dateTime")]
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ComposerId))]
        [InverseProperty(nameof(User.Messages))]
        public virtual User Composer { get; set; }
    }
}