using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.Core.Interfacce
{
    public interface IRepository<T>
    {
        //CRUD

        //Insert
        void Create(T obj);
        //Read
        T GetByID(int ID);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithFilter(int filtro);
        //Update
        bool Update(T obj);
        //Delete
        bool Delete(T obj);
    }
}
