using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer
{
    [Index(nameof(FirstUserId), nameof(SecondUserId), Name = "UNIQUE_VALUES", IsUnique = true)]
    public partial class Direct
    {
        public Direct()
        {
            DirectMessages = new HashSet<DirectMessage>();
        }

        [Key]
        [Column("DirectID")]
        public int DirectId { get; set; }
        [Column("FirstUserID")]
        public int FirstUserId { get; set; }
        [Column("SecondUserID")]
        public int SecondUserId { get; set; }

        [InverseProperty(nameof(DirectMessage.Direct))]
        public virtual ICollection<DirectMessage> DirectMessages { get; set; }
    }
}
