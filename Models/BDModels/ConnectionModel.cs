using GiaoHangMongoDB.Models.DataModels;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiaoHangMongoDB.Models.BDModels
{
    public class ConnectionModel
    {
        IConfiguration Configuration;
        public ConnectionModel(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IMongoDatabase Connection
        {
            get
            {
                var cl = new MongoClient(Configuration.GetConnectionString("MongoConnection"));
                var data = cl.GetDatabase(Configuration.GetConnectionString("database"));
                return data;
            }
        }
        public IMongoCollection<Customer> customer => Connection.GetCollection<Customer>("KhachHang");

    }
}
