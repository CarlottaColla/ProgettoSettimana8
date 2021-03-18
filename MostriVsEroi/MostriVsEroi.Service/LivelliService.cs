using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.Service
{
    public class LivelliService
    {
        private ILivelliRepository _repo;

        public LivelliService(ILivelliRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Livelli> ListaLivelli()
        {
            return _repo.GetAll();
        }

        public Livelli RitornaLivello(int id)
        {
            return _repo.GetByID(id);
        }

    }
}
