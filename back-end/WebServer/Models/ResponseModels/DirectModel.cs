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
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("directName")]
        [StringLength(maximumLength: 40, MinimumLength = 3)]
        public string DirectName { get; set; }
    }
}
