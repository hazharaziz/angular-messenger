using System;
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
            DirectMessages = new HashSet<Group>();
        }

        [Key]
        [Column("DirectID")]
        public int DirectId { get; set; }

        [Column("FirstUserID")]
        public int FirstUserId { get; set; }

        [Column("SecondUserID")]
        public int SecondUserId { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Group.Direct))]
        public virtual ICollection<Group> DirectMessages { get; set; }
    }
}
