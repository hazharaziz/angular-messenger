using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebServer.Models.RequestModels
{
    public class LoginRequest
    {
        [Required]
        [JsonPropertyName("username")]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [JsonPropertyName("password")]
        [StringLength(16, MinimumLength = 4)]
        public string Password { get; set; }
    }
}
