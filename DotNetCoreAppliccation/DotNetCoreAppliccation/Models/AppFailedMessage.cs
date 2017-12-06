using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAppliccation.Models
{
    public class AppFailedMessage : TableEntity
    {
        public AppFailedMessage() { }
        public AppFailedMessage(string partitionKey, string rowKey) : base(partitionKey, rowKey) { }
        public DateTime CreatedTS { get; set; }
        public string MessageID { get; set; }
        public string TypeID { get; set; }
        public string WebJob { get; set; }
        public string Queue { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
