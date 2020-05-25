﻿namespace KwetService.DatastoreSettings
{
    public class KwetstoreDatabaseSettings : IKwetstoreDatabaseSettings
    {
        public string KwetCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IKwetstoreDatabaseSettings
    {
        string KwetCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}