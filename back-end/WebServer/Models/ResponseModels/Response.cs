using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebServer.Models.ResponseModels
{
    #nullable enable
    public class Response<T>
    {
        [JsonIgnore]
        public int Status { get; set; }
        
        public string? Token { get; set; }
        
        public T? Data { get; set; }
    }
}
