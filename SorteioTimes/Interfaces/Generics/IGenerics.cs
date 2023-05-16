using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SorteioTimes.Interfaces.Generics
{
    public interface IGenerics<T> where T : class
    {
        void GerarTimes(List<T> itens, int itensPorTime, int numTimes);
        void SaveChanges();

    }
}
