using SorteioTimes.Context;
using SorteioTimes.Interfaces.Generics;

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
            //List<T> jogadoresDisponiveis = itens.Where(j => !j.Excluido).ToList();
            List<T> itensEmbaralhados = itens.OrderBy(x => random.Next()).ToList();

            List<List<T>> times = new List<List<T>>();

            for (int i = 0; i < numTimes; i++)
            {
                times.Add(new List<T>());
            }

            int itemIndex = 0;

            for (int i = 0; i < numTimes; i++)
            {
                for (int j = 0; j < itensPorTime; j++)
                {
                    if (itemIndex < itens.Count)
                    {
                        T item = itensEmbaralhados[itemIndex];
                        if (itens.Contains(item) && !times.Any(time => time.Contains(item)))
                        {
                            times[i].Add(item);
                        }
                        else
                        {
                            j--; // Desconsidera a iteração atual e repete para preencher o lugar vazio
                        }
                        itemIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Console.WriteLine("Times:");

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
