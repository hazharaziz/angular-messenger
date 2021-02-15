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
        public Message()
        {
            InverseReplyTo = new HashSet<Message>();
        }

        [Key]
        [Column("MessageID")]
        public int MessageId { get; set; }

        [Required]
        [Column("ComposerID")]
        public int ComposerId { get; set; }

        [Column("ReplyToID")]
        public int ReplyToId { get; set; } = 0;

        [Required]
        [StringLength(400)]
        public string Text { get; set; }

        [Required]
        [StringLength(70)]
        public string ComposerName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ComposerId))]
        [InverseProperty(nameof(User.Messages))]
        public virtual User Composer { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(ReplyToId))]
        [InverseProperty(nameof(Message.InverseReplyTo))]
        public virtual Message ReplyTo { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Message.ReplyTo))]
        public virtual ICollection<Message> InverseReplyTo { get; set; }
    }
}
