using SorteioTimes.Domain;
using SorteioTimes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SorteioTimes.Repositories
{
    public class RepositoryJogador : RepositoryBase<Jogador>, IRepositoryJogador
    {
        public RepositoryJogador(bool SaveChanges = true) : base(SaveChanges)
        {

        }
    }
}
