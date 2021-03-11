using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebServer.Models.ResponseModels
{
    public class DirectModel
    {
        [JsonPropertyName("directId")]
        public int Id { get; set; }

        [JsonPropertyName("targetId")]
        public int TargetId { get; set; }

        [JsonPropertyName("targetName")]
        [StringLength(maximumLength: 40, MinimumLength = 3)]
        public string TargetName { get; set; }
    }
}
