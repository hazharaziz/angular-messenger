using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebServer.Models.RequestModels
{
    public class ChangePasswordRequest
    {
        [Required]
        [JsonPropertyName("oldPassword")]
        [StringLength(16, MinimumLength = 4)]
        public string OldPassword { get; set; }

        [Required]
        [JsonPropertyName("newPassword")]
        [StringLength(16, MinimumLength = 4)]
        public string NewPassword { get; set; }
    }
}
