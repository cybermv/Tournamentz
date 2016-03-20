namespace Tournamentz.BL.Core.Logging
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using NLog;
    using NLog.Config;
    using NLog.Targets;
    using System;

    [Target("MongoDb")]
    public class MongoDbLogTarget : Target
    {
        private IMongoClient _client;
        private IMongoDatabase _database;

        [RequiredParameter]
        public string ConnectionString { get; set; }

        [RequiredParameter]
        public string DatabaseName { get; set; }

        public string CollectionName { get; set; }

        protected override void InitializeTarget()
        {
            base.InitializeTarget();

            this._client = new MongoClient(this.ConnectionString);
            this._database = _client.GetDatabase(this.DatabaseName);
        }

        protected override void CloseTarget()
        {
            base.CloseTarget();

            this._database = null;
            this._client = null;
            // TODO: close connection
        }

        protected override void Write(LogEventInfo logEvent)
        {
            // TODO: implement
            //IMongoCollection<BsonDocument> logCollection = this._database
            //    .GetCollection<BsonDocument>(this.CollectionName ?? "log");

            //logCollection.InsertOne(logEvent.ToBsonDocument());
        }
    }
}