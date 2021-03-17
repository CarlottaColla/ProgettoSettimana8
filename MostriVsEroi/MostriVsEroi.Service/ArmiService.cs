using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.Service
{
    public class ArmiService
    {
        private IArmiRepository _repo;

        public ArmiService(IArmiRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Armi> ListaArmiConFiltro(int id)
        {
            return _repo.GetAllWithFilter(id);
        }
    }
}
