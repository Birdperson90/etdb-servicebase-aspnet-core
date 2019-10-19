using System;

namespace Etdb.ServiceBase.Constants
{
    public class ServiceNames
    {
        [Obsolete("FileService shall not be used!", true)]
        public const string FileService = "FileService";

        public const string UserService = "UserService";
        public const string IndexService = "IndexService";
        public const string StorageService = "StorageService";
        public const string MessagingService = "MessagingService";
    }
}