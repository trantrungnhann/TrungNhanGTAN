using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiaoHangMongoDB.Models.ViewModels
{
    public class ThucAn
    {
        [BsonId]
        public ObjectId Id { get; set; } // Nếu sử dụng ObjectId

        [BsonElement("MaThucAn")]
        public int MaThucAn { get; set; }

        [BsonElement("TenThucAn")]
        public string TenThucAn { get; set; }

        [BsonElement("DanhMuc")]
        public int DanhMuc { get; set; }

        [BsonElement("Gia")]
        public int Gia { get; set; }

        [BsonElement("MoTa")]
        public string MoTa { get; set; }

    }
}
