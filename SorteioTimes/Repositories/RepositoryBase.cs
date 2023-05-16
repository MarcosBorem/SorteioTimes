using SorteioTimes.Context;
using SorteioTimes.Domain;
using SorteioTimes.Interfaces.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SorteioTimes.Repositories
{
    public class RepositoryBase<T> : IGenerics<T>, IDisposable where T : class
    {
        protected SorteioTimesContext _context;
        public bool _SaveChanges = true;
        public RepositoryBase(bool saveChanges = true)
        {
            _SaveChanges = saveChanges;
            _context = new SorteioTimesContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void GerarTimes(List<T> itens, int itensPorTime, int numTimes)
        {
            Random random = new Random();
            List<T> itensEmbaralhados = itens.OrderBy(x => random.Next()).ToList();

            List<List<T>> times = new List<List<T>>();

            for (int i = 0; i < numTimes; i++)
            {
                times.Add(new List<T>());
            }

            int itemIndex = 0;
            int timesComItensCompletos = itens.Count / itensPorTime;
            int itensRestantes = itens.Count % itensPorTime;

            for (int i = 0; i < numTimes; i++)
            {
                int itensNoTime = itensPorTime;

                if (i < itensRestantes)
                {
                    itensNoTime++;
                }

                for (int j = 0; j < itensNoTime; j++)
                {
                    T item = itensEmbaralhados[itemIndex];
                    times[i].Add(item);
                    itemIndex++;
                }
            }

            Console.WriteLine("Times:");
            Console.WriteLine();

            for (int i = 0; i < numTimes; i++)
            {
                Console.WriteLine($"Time {i + 1}:");

                double somaNotas = 0;
                foreach (dynamic item in times[i])
                {
                    Console.WriteLine($"{item.Nome}  {item.Nota}");
                    somaNotas += item.Nota;

                }
                double media = (double)somaNotas / itensPorTime;
                Console.WriteLine($"Média: {media:F2}");
                Console.WriteLine();
            }
        }
    }
}
