using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebServer.Models.ResponseModels
{
    public class GroupModel
    {
        [JsonPropertyName("groupId")]
        public int GroupId { get; set; }

        [JsonPropertyName("groupName")]
        [StringLength(40, MinimumLength = 3)]
        public string GroupName { get; set; }
    }
}
