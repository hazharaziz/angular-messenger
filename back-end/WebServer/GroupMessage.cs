using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer
{
    public partial class GroupMessage
    {
        [Key]
        [Column("GroupMessageID")]
        public int GroupMessageId { get; set; }
        [Column("GroupID")]
        public int GroupId { get; set; }
        [Column("ReplyToID")]
        public int ReplyToId { get; set; }
        [Column("ComposerID")]
        public int ComposerId { get; set; }
        [Required]
        [StringLength(400)]
        public string Text { get; set; }
        [Required]
        [StringLength(40)]
        public string ComposerName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        [ForeignKey(nameof(GroupId))]
        [InverseProperty("GroupMessages")]
        public virtual Group Group { get; set; }
    }
}
