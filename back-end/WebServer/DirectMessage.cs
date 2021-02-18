using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer
{
    public partial class DirectMessage
    {
        [Key]
        [Column("DirectMessageID")]
        public int DirectMessageId { get; set; }
        [Column("DirectID")]
        public int DirectId { get; set; }
        [Column("ComposerID")]
        public int ComposerId { get; set; }
        [Column("ReplyToID")]
        public int ReplyToId { get; set; }
        [Required]
        [StringLength(400)]
        public string Text { get; set; }
        [Required]
        [StringLength(40)]
        public string ComposerName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        [ForeignKey(nameof(DirectId))]
        [InverseProperty("DirectMessages")]
        public virtual Direct Direct { get; set; }
    }
}
