using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using System.Configuration;
using MongoDB.Bson.Serialization;
using FarmDBMongoV1.Models;

namespace FarmDBMongoV1.App_Start
{
    public class MongoContext
    {
        MongoClient _client;
        public IMongoDatabase db;
        public MongoContext()        //constructor   
        {
            // Reading credentials from Web.config file   
            var MongoDatabaseName = ConfigurationManager.AppSettings["MongoDatabaseName"]; //FarmDB    
            var MongoPort = ConfigurationManager.AppSettings["MongoPort"];  //27017  
            var MongoHost = ConfigurationManager.AppSettings["MongoHost"];  //localhost  
            // Creating MongoClientSettings  
            var settings = new MongoClientSettings
            { Server = new MongoServerAddress(MongoHost, Convert.ToInt32(MongoPort)) };
            _client = new MongoClient(settings);
            db = _client.GetDatabase(MongoDatabaseName);
        }
    }


}