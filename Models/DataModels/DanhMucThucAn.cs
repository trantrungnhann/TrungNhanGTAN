using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiaoHangMongoDB.Models.ViewModels
{
    public class DanhMucThucAn
    {
        [BsonId]
        public ObjectId Id { get; set; } // Nếu bạn sử dụng ObjectId

        [BsonElement("MaDanhMuc")]
        public int MaDanhMuc { get; set; }

        [BsonElement("name")] // Đảm bảo tên khớp với trường trong MongoDB
        public string Name { get; set; }

        [BsonElement("description")] // Đảm bảo tên khớp với trường trong MongoDB
        public string Description { get; set; }
    }
}
