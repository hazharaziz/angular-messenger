using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebServer.Models.RequestModels
{
    public class DirectMessageRequest
    {
        [Required]
        [JsonPropertyName("directId")]
        public int DirectId { get; set; }

        [Required]
        [JsonPropertyName("targetId")]
        public int TargetId { get; set; }

        [JsonPropertyName("replyToId")]
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
        public DateTime DateTime { get; set; }
    }
}
