using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.Service
{
    public class MostroService
    {
        private IMostroRepository _repo;

        public MostroService(IMostroRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Mostri> ListaMostriLivelloEroe(int livello)
        {
            return _repo.GetAllWithFilter(livello);
        }
    }
}
