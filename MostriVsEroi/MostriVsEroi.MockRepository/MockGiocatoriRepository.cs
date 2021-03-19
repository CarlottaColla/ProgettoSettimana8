using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;

namespace MostriVsEroi.MockRepository
{
    public class MockGiocatoriRepository : IGiocatoriRepository
    {
        private static List<Giocatori> giocatoriSalvati = new List<Giocatori>() { 
            new Giocatori() {ID = 1, Nome = "Admin", Ruolo_ID = 2}
        };
        public bool Create(Giocatori obj)
        {
            giocatoriSalvati.Add(obj);
            if (giocatoriSalvati.Contains(obj))
                return true;
            return false;
        }

        public bool Delete(Giocatori obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Giocatori> GetAll()
        {
            return giocatoriSalvati;
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
