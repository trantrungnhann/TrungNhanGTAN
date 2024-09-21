using GiaoHangMongoDB.Models.BDModels;
using GiaoHangMongoDB.Models.DataModels;
using GiaoHangMongoDB.ViewModels;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiaoHangMongoDB.Models.DBModel
{
    public class RepositoryCustomer : IRepositoryCustomer
    {

        ConnectionModel db;

        public RepositoryCustomer(ConnectionModel db)
        {
            this.db = db;
        }

        public bool Delete(int key)
        {
            try
            {

            }
            catch (Exception)
            {
                return false;
            }
            db.customer.DeleteOne(x => x.MaCustomer == key);
            return true;
        }
        public List<Customer> GetAll()
        {
            return db.customer.Find(FilterDefinition<Customer>.Empty).ToList();
        }

        public Customer GetbyID(int key)
        {
            return db.customer.Find(x => x.MaCustomer == key).FirstOrDefault();
        }

        public bool Insert(Customer entity)
        {
            entity.NameCustomer = entity.NameCustomer ?? "";
            entity.sdt = entity.sdt ?? "";
            entity.Email = entity.Email ?? "";
            entity.DiaChi = entity.DiaChi ?? "";
            db.customer.InsertOne(entity);
            return true;
        }

        public bool Update(Customer entity)
        {
            var p = Builders<Customer>.Update.Set("MaKhachHang", entity.MaCustomer)
                .Set("TenKhachHang", entity.NameCustomer)
                .Set("SoDienThoai", entity.sdt)
                .Set("Email", entity.Email)
                .Set("DiaChi", entity.DiaChi)
                .Set("MaDiaDiem", entity.MaDiaDiem);
            db.customer.UpdateOne(x => x.MaCustomer == entity.MaCustomer, p);
            return true;
        }
        public List<CustomerViewModels> GetCustomerFull()
        {
            var lookup = new BsonDocument
    {
        { "$lookup", new BsonDocument
            {
                { "from", "diaDiems" },
                { "localField", "MaDiaDiem" },
                { "foreignField", "MaDiaDiem" },
                { "as", "diaDiemDetails" }
            }
        }
    };
            var unwind = new BsonDocument
    {
        { "$unwind", "$diaDiemDetails" }
    };

            var customers = db.customer.Aggregate<BsonDocument>(new[] { lookup, unwind }).ToList();

            var data = new List<CustomerViewModels>();
            foreach (var e in customers)
            {
                var customer = new CustomerViewModels
                {
                    MaCustomer = e.Contains("MaCustomer") ? e["MaCustomer"].ToInt32() : default,
                    NameCustomer = e.Contains("NameCustomer") ? e["NameCustomer"].ToString() : null,
                    sdt = e.Contains("sdt") ? e["sdt"].ToString() : null,
                    Email = e.Contains("Email") ? e["Email"].ToString() : null,
                    DiaChi = e.Contains("DiaChi") ? e["DiaChi"].ToString() : null,
                    TenDiaDiem = e.Contains("diaDiemDetails") && e["diaDiemDetails"].AsBsonArray.Count > 0
                                  ? e["diaDiemDetails"][0]["TenDiaDiem"].ToString()
                                  : null
                };

                data.Add(customer);
            }


            return data;
        }



        Customer IRepository<Customer, int>.GetbyID(int key)
        {
            throw new NotImplementedException();
        }
    }
}
