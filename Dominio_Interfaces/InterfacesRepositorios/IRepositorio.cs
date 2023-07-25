using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio_Interfaces.InterfacesRepositorios
{
    public interface IRepositorio<T>
    {
        void Add(T obj);
        bool Remove(int id);
        void Update (T obj);
        IEnumerable<T> GetAll();
        T FindById(int id);

    }
}
