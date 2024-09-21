using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiaoHangMongoDB.Models.DataModels
{
    public class Customer
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("MaKhachHang")]
        public int? MaCustomer { get; set; }

        [BsonElement("TenKhachHang")]
        public string? NameCustomer { get; set; }

        [BsonElement("SoDienThoai")]
        public string? sdt { get; set; }

        [BsonElement("Email")]
        public string? Email { get; set; }

        [BsonElement("DiaChi")]
        public string? DiaChi { get; set; }

        [BsonElement("MaDiaDiem")]
        public int? MaDiaDiem { get; set; }
    }
}
