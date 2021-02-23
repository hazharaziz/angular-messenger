using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebServer.Models.DBModels
{
    [Index(nameof(Username), Name = "UNIQUE_USERNAME", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("username")]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [JsonPropertyName("password")]
        [StringLength(16, MinimumLength = 4)]
        public string Password { get; set; }

        [Required]
        [JsonPropertyName("name")]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }

        [JsonPropertyName("isPublic")]
        [Range(typeof(int), "0", "1")]
        public int IsPublic { get; set; } = 1;

        [JsonIgnore]
        [InverseProperty(nameof(Message.Composer))]
        public virtual ICollection<Message> Messages { get; set; }
    }
}