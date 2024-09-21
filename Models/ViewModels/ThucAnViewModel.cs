using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiaoHangMongoDB.Models.ViewModels
{
    public class ThucAnViewModel
    {
        public ThucAn ThucAn { get; set; } = new ThucAn(); 
        public IEnumerable<DanhMucThucAn> DanhMucThucAns { get; set; } = new List<DanhMucThucAn>(); 
    }
}
