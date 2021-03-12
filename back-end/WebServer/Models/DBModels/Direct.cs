using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer.Models.DBModels
{
    [Index(nameof(FirstUserId), nameof(SecondUserId), Name = "UNIQUE_VALUES", IsUnique = true)]
    public partial class Direct
    {
        [Key]
        [Column("DirectID")]
        public int DirectId { get; set; }

        [Column("FirstUserID")]
        public int FirstUserId { get; set; }

        [Column("SecondUserID")]
        public int SecondUserId { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(DirectMessage.Direct))]
        public virtual ICollection<DirectMessage> DirectMessages { get; set; }
    }
}
