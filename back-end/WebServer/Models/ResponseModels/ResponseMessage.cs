using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Models.ResponseModels
{
    public class ResponseMessage
    {
        public int MessageId { get; set; }
        public int ComposerId { get; set; }
        public int? ReplyToId { get; set; }
        public string Text { get; set; }
        public string ComposerName { get; set; }
        public DateTime DateTime { get; set; }
    }
}
