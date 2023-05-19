using SorteioTimes.Interfaces;
using SorteioTimes.Model;

namespace SorteioTimes.Repositories
{
    public class RepositoryJogador : RepositoryBase<Jogador>, IRepositoryJogador
    {
        public RepositoryJogador(bool SaveChanges = true) : base(SaveChanges)
        {

        }
    }
}
