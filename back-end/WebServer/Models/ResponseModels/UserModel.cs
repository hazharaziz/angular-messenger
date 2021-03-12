using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebServer.Models.ResponseModels
{
    public class UserModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }

        [JsonPropertyName("name")]
        [StringLength(40, MinimumLength = 3)]
        public string Name { get; set; }

        [JsonPropertyName("isPublic")]
        [Range(typeof(int), "0", "1")]
        public int IsPublic { get; set; }
    }
}
