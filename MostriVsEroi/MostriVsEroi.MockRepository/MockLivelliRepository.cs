using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.MockRepository
{
    public class MockLivelliRepository : ILivelliRepository
    {
        private static List<Livelli> livelliSalvati = new List<Livelli>() { 
            new Livelli () {ID = 1, Numero = 1, PuntiPassaggio = 0, PuntiVita=20},
            new Livelli () {ID = 2, Numero = 2, PuntiPassaggio = 30, PuntiVita=40},
            new Livelli () {ID = 3, Numero = 3, PuntiPassaggio = 60, PuntiVita=60},
            new Livelli () {ID = 4, Numero = 4, PuntiPassaggio = 90, PuntiVita=80},
            new Livelli () {ID = 5, Numero = 5, PuntiPassaggio = 120, PuntiVita=100}
        };
        public bool Create(Livelli obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Livelli obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Livelli> GetAll()
        {
            return livelliSalvati;
        }

        public IEnumerable<Livelli> GetAllWithFilter(int filtro)
        {
            throw new NotImplementedException();
        }

        public Livelli GetByID(int ID)
        {
            foreach(var item in livelliSalvati)
            {
                if (item.ID == ID)
                    return item;
            }
            return new Livelli();
        }

        public bool Update(Livelli obj)
        {
            throw new NotImplementedException();
        }
    }
}
