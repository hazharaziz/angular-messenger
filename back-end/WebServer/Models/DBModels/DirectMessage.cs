using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer.Models.DBModels
{
    public partial class DirectMessage
    {
        [Key]
        [JsonPropertyName("id")]
        [Column("DirectMessageID")]
        public int DirectMessageId { get; set; }
        
        [Required]
        [JsonPropertyName("directId")]
        [Column("DirectID")]
        public int DirectId { get; set; }

        [Required]
        [JsonPropertyName("composerId")]
        [Column("ComposerID")]
        public int ComposerId { get; set; }

        [JsonPropertyName("replyToId")]
        [Column("ReplyToID")]
        public int ReplyToId { get; set; }

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
        [ForeignKey(nameof(DirectId))]
        [InverseProperty("DirectMessages")]
        public virtual DirectMessage Direct { get; set; }
    }
}
