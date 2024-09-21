using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiaoHangMongoDB.Models.ViewModels
{
    public class ThucAnDTO
    {
        public int MaThucAn { get; set; }
        public string TenThucAn { get; set; }
        public int DanhMuc { get; set; }
        public int Gia { get; set; }
        public string MoTa { get; set; }
    }
}
