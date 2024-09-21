using GiaoHangMongoDB.Models;
using GiaoHangMongoDB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GiaoHangMongoDB.Controllers
{
    public class ThucAnController : Controller
    {
        public List<DanhMucThucAn> GetDanhMucs()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("QL_GiaoHangNhanh");
            var danhMucCollection = database.GetCollection<DanhMucThucAn>("DanhMucThucAn");
            return danhMucCollection.Find(danhMuc => true).ToList();
        }



        public List<ThucAn> GetThucAns()
        {
            try
            {
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("QL_GiaoHangNhanh");
                var thucAnCollection = database.GetCollection<ThucAn>("ThucAn");

                return thucAnCollection.Find(thucAn => true).ToList();
            }
            catch (Exception ex)
            {
                // In ra lỗi để kiểm tra
                Console.WriteLine($"Lỗi: {ex.Message}");
                throw; // Để có thể xử lý tiếp nếu cần
            }
        }




        // Hiển thị danh sách thức ăn
        public IActionResult Index()
        {
            var thucAnList = GetThucAns();
            return View(thucAnList);
        }

        // Tạo thức ăn mới
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new ThucAnViewModel
            {
                ThucAn = new ThucAn(), // Khởi tạo đối tượng ThucAn
                DanhMucThucAns = GetDanhMucs() // Đảm bảo danh mục được lấy đúng cách và không null
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(ThucAnViewModel viewModel)
        {
            // Tạo một đối tượng ThucAn mới từ viewModel
            var newThucAn = viewModel.ThucAn;

            // Kết nối đến MongoDB
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("QL_GiaoHangNhanh");
            var thucAnCollection = database.GetCollection<ThucAn>("ThucAn");

            // Thêm món ăn mới vào collection
            thucAnCollection.InsertOne(newThucAn); // MongoDB tự động gán giá trị cho Id

            return RedirectToAction("Index");
        }










        // Sửa thức ăn
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var thucAnList = GetThucAns(); // Lấy danh sách món ăn
            var thucAn = thucAnList.FirstOrDefault(t => t.MaThucAn == id);

            if (thucAn == null)
            {
                return NotFound(); // Trả về lỗi nếu không tìm thấy món ăn
            }

            var viewModel = new ThucAnViewModel
            {
                ThucAn = thucAn, // Gán giá trị cho đối tượng ThucAn
                DanhMucThucAns = GetDanhMucs() // Đảm bảo danh mục không null
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ThucAnViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Kết nối đến MongoDB
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("QL_GiaoHangNhanh");
                var thucAnCollection = database.GetCollection<ThucAn>("ThucAn");

                // Tìm món ăn cần cập nhật
                var filter = Builders<ThucAn>.Filter.Eq(t => t.MaThucAn, viewModel.ThucAn.MaThucAn);
                var update = Builders<ThucAn>.Update
                    .Set(t => t.TenThucAn, viewModel.ThucAn.TenThucAn)
                    .Set(t => t.DanhMuc, viewModel.ThucAn.DanhMuc)
                    .Set(t => t.Gia, viewModel.ThucAn.Gia)
                    .Set(t => t.MoTa, viewModel.ThucAn.MoTa);

                // Cập nhật món ăn trong MongoDB
                var result = thucAnCollection.UpdateOne(filter, update);

                if (result.ModifiedCount > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Nếu không có tài liệu nào được cập nhật
                    ModelState.AddModelError("", "Cập nhật không thành công.");
                }
            }

            // Nếu có lỗi, nạp lại danh mục để hiển thị combobox
            viewModel.DanhMucThucAns = GetDanhMucs();
            return View(viewModel);
        }




        // Xóa thức ăn
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var thucAnList = GetThucAns(); // Lấy danh sách món ăn
            var thucAn = thucAnList.FirstOrDefault(t => t.MaThucAn == id);

            if (thucAn == null)
            {
                return NotFound(); // Trả về lỗi nếu món ăn không tồn tại
            }

            return View(thucAn);
        }


        [HttpPost]
        public IActionResult Delete(ThucAn thucAn)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("QL_GiaoHangNhanh");
            var thucAnCollection = database.GetCollection<ThucAn>("ThucAn");

            // Tạo filter để tìm món ăn theo MaThucAn
            var filter = Builders<ThucAn>.Filter.Eq(t => t.MaThucAn, thucAn.MaThucAn);

            // Thực hiện xóa món ăn
            var result = thucAnCollection.DeleteOne(filter);

            if (result.DeletedCount > 0)
            {
                return RedirectToAction("Index"); // Chuyển hướng về danh sách món ăn
            }

            // Nếu không tìm thấy món ăn để xóa
            ModelState.AddModelError("", "Món ăn không tồn tại.");
            return View(thucAn); // Trả về view để hiển thị thông báo lỗi
        }


    }
}
