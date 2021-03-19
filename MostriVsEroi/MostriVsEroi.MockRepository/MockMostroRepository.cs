using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.MockRepository
{
    public class MockMostroRepository : IMostroRepository
    {
        private static List<Mostri> mostriSalvati = new List<Mostri>() { 
            new Mostri() {ID = 1, Nome = "Orco facile", Arma = 3, Livello = 1, Classe = 3, PuntiVita = 20},
            new Mostri() {ID = 2, Nome = "Orco medio", Arma = 3, Livello = 2, Classe = 3, PuntiVita = 40},
            new Mostri() {ID = 3, Nome = "Signore oscuro", Arma = 4, Livello = 3, Classe = 4, PuntiVita = 60}
        };
        public bool Create(Mostri obj)
        {
            mostriSalvati.Add(obj);
            if (mostriSalvati.Contains(obj))
                return true;
            return false;
        }

        public bool Delete(Mostri obj)
        {
            mostriSalvati.Remove(obj);
            if (mostriSalvati.Contains(obj))
                return false;
            return true;
        }

        public IEnumerable<Mostri> GetAll()
        {
            return mostriSalvati;
        }

        public IEnumerable<Mostri> GetAllWithFilter(int filtro)
        {
            throw new NotImplementedException();
        }

        public Mostri GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Mostri obj)
        {
            throw new NotImplementedException();
        }
    }
}
