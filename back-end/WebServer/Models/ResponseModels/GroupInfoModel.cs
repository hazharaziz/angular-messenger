using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebServer.Models.ResponseModels
{
    public class GroupInfoModel
    {
        [JsonPropertyName("groupId")]
        public int GroupId { get; set; }

        [JsonPropertyName("groupName")]
        public string GroupName { get; set; }

        [JsonPropertyName("creatorId")]
        public int CreatorId { get; set; }

        [JsonPropertyName("creatorName")]
        public string CreatorName { get; set; }

        [JsonPropertyName("members")]
        public List<UserModel> Members { get; set; }
    }
}
