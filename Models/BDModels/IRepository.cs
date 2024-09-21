using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GiaoHangMongoDB.Models.DBModel
{
    public interface IRepository<T,K>
    {
        List<T> GetAll();
        T GetbyID(K key);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(K key);
    }
}
