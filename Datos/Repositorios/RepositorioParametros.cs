using Datos.ContextoEF;
using Dominio_Interfaces.EntidadesAuxiliares;
using Dominio_Interfaces.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{
    public class RepositorioParametros : IRepositorioParametros
    {
        public LibreriaContexto Contexto { get; set; }

        public RepositorioParametros(LibreriaContexto ctx)
        {
            Contexto = ctx;
        }
        public void Add(Parametros obj)
        {
            throw new NotImplementedException();
        }

        public Parametros FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parametros> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Parametros obj)
        {
            throw new NotImplementedException();
        }

        public string ValorParametro(string nombre)
        {
           var resultado = Contexto.Parametros.Where(param =>param.NombreParametro.ToLower()==nombre.ToLower()).Select(param => param.Valor).SingleOrDefault();
            return resultado;
        }
    }
}
