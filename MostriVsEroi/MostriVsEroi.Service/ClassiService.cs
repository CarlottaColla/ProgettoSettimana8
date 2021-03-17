using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using MostriVsEroi.MockRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.Service
{
    public class ClassiService
    {
        private IClassiRepository _repo;

        public ClassiService (IClassiRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Classi> ListaClassiConFiltro(int id)
        {
           return _repo.GetAllWithFilter(id);
        }
    }
}
