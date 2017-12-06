using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreAppliccation.Models
{
    public class ManageTableStorage
    {
        public static IConfiguration _iconfiguration;
        public ManageTableStorage(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        private string _accountName = _iconfiguration.GetSection("Appsettings:AccountName").Value;
        private string _keyName = _iconfiguration.GetSection("Appsettings:AccountName").Value;

        private static StorageCredentials storageCredentials { get; set; }
        private static CloudStorageAccount cloudStorageAccount { get; set; }
        private static CloudTableClient cloudTableClient { get; set; }

        /// <summary>
        /// Getting the table reference using tableName
        /// </summary>
        public CloudTable GetTableRefence(string tableName)
        {
            storageCredentials = new StorageCredentials(_accountName, _keyName);
            cloudStorageAccount = new CloudStorageAccount(storageCredentials, useHttps: true);
            cloudTableClient = cloudStorageAccount.CreateCloudTableClient();
            return cloudTableClient.GetTableReference(tableName);
        }
    }
}
