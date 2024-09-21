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
    public class DanhMucController : Controller
    {
        private const string JsonFilePath = "wwwroot/data/DanhMucThucAn.json";

        public IActionResult Index()
        {
            var danhMucs = GetDanhMucs();
            return View(danhMucs);
        }

        // Tạo danh mục mới
        [HttpGet]
        public IActionResult Create()
        {
            return View(new DanhMucThucAn());
        }

        [HttpPost]
        public IActionResult Create(DanhMucThucAn danhMuc)
        {
            if (ModelState.IsValid) // Kiểm tra xem dữ liệu có hợp lệ không
            {
                // Tạo kết nối đến MongoDB
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("QL_GiaoHangNhanh");
                var danhMucCollection = database.GetCollection<DanhMucThucAn>("DanhMucThucAn");

                // Tự động tăng MaDanhMuc
                var danhMucs = GetDanhMucs();
                danhMuc.MaDanhMuc = danhMucs.Any() ? danhMucs.Max(dm => dm.MaDanhMuc) + 1 : 1;

                // Thêm danh mục mới vào MongoDB
                danhMucCollection.InsertOne(danhMuc);

                return RedirectToAction("Index");
            }

            // Nếu dữ liệu không hợp lệ, trả lại view với đối tượng hiện tại
            return View(danhMuc);
        }


        // Sửa danh mục
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var danhMucs = GetDanhMucs();
            var danhMuc = danhMucs.FirstOrDefault(dm => dm.MaDanhMuc == id);
            if (danhMuc == null) return NotFound();
            return View(danhMuc);
        }

        [HttpPost]
        public IActionResult Edit(DanhMucThucAn danhMuc)
        {
            var danhMucs = GetDanhMucs();
            var existingDanhMuc = danhMucs.FirstOrDefault(dm => dm.MaDanhMuc == danhMuc.MaDanhMuc);
            if (existingDanhMuc == null) return NotFound();

            // Cập nhật thông tin
            existingDanhMuc.Name = danhMuc.Name;
            existingDanhMuc.Description = danhMuc.Description;

            // Cập nhật vào MongoDB
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("QL_GiaoHangNhanh");
            var danhMucCollection = database.GetCollection<DanhMucThucAn>("DanhMucThucAn");

            // Tìm kiếm và cập nhật
            var filter = Builders<DanhMucThucAn>.Filter.Eq(dm => dm.MaDanhMuc, danhMuc.MaDanhMuc);
            danhMucCollection.ReplaceOne(filter, existingDanhMuc);

            return RedirectToAction("Index");
        }


        // Xóa danh mục
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var danhMucs = GetDanhMucs();
            var danhMuc = danhMucs.FirstOrDefault(dm => dm.MaDanhMuc == id);
            if (danhMuc == null) return NotFound();
            return View(danhMuc);
        }

        [HttpPost]
        public IActionResult Delete(DanhMucDTO viewModel)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("QL_GiaoHangNhanh");
            var danhMucCollection = database.GetCollection<DanhMucThucAn>("DanhMucThucAn");

            // Tìm danh mục để xóa
            var filter = Builders<DanhMucThucAn>.Filter.Eq(dm => dm.MaDanhMuc, viewModel.MaDanhMuc);

            // Xóa danh mục trong MongoDB
            var result = danhMucCollection.DeleteOne(filter);

            // Kiểm tra xem có xóa thành công không
            if (result.DeletedCount == 0)
            {
                return NotFound(); // Trả về lỗi nếu không tìm thấy danh mục để xóa
            }

            return RedirectToAction("Index"); // Quay lại danh sách sau khi xóa
        }


        // Helper methods
        private List<DanhMucThucAn> GetDanhMucs()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("QL_GiaoHangNhanh");
            var danhMucCollection = database.GetCollection<DanhMucThucAn>("DanhMucThucAn");
            return danhMucCollection.Find(danhMuc => true).ToList();
        }




        private void SaveDanhMucs(List<DanhMucThucAn> danhMucs)
        {
            System.IO.File.WriteAllText(JsonFilePath, JsonConvert.SerializeObject(danhMucs, Formatting.Indented));
        }
    }
}
