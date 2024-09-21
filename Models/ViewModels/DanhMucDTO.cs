using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiaoHangMongoDB.Models.ViewModels
{
    public class DanhMucDTO
    {
        public int MaDanhMuc { get; set; } // Đọc Id dưới dạng chuỗi
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
