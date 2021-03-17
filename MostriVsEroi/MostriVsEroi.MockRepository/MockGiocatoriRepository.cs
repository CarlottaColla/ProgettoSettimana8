using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;

namespace MostriVsEroi.MockRepository
{
    public class MockGiocatoriRepository : IGiocatoriRepository
    {
        public void Create(Giocatori obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Giocatori obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Giocatori> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Giocatori> GetAllWithFilter(int ID)
        {
            throw new NotImplementedException();
        }

        public Giocatori GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Giocatori obj)
        {
            throw new NotImplementedException();
        }
    }
}
