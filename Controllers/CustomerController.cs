using GiaoHangMongoDB.Models.DataModels;
using GiaoHangMongoDB.Models.DBModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiaoHangMongoDB.Controllers
{
    public class CustomerController : Controller
    {
        IRepositoryCustomer repositoryCustomer;
        public CustomerController(IRepositoryCustomer repositoryCustomer)
        {
            this.repositoryCustomer = repositoryCustomer;
        }
        public IActionResult Index()
        {
            var data = repositoryCustomer.GetAll();
            if (data == null)
            {
                return View(new List<Customer>());
            }
            return View(data);
        }
    }
}
