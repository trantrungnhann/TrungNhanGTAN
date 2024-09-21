
using GiaoHangMongoDB.Models.DataModels;
using GiaoHangMongoDB.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiaoHangMongoDB.Models.DBModel
{
    public interface IRepositoryCustomer:IRepository<Customer,int>
    {
        List<CustomerViewModels> GetCustomerFull();
    }
}
