using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiaoHangMongoDB.ViewModels
{
    public class CustomerViewModels
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public int? MaCustomer { get; set; }
        public String? NameCustomer { get; set; }
        public String? sdt { get; set; }
        public String? Email { get; set; }
        public String? DiaChi { get; set; }
        public String? TenDiaDiem { get; set; }
    }
}
