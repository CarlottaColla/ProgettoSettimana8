using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.Service
{
    public class GiocatoriService
    {
        private IGiocatoriRepository _repo;

        public GiocatoriService(IGiocatoriRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Giocatori> ListaGiocatori()
        {
            return _repo.GetAll();
        }

        public void CreaGiocatore (Giocatori obj)
        {
            _repo.Create(obj);
        }
    }
}
