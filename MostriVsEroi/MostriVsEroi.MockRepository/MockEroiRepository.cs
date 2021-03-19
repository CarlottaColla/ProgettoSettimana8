using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.MockRepository
{
    public class MockEroiRepository : IEroiRepository
    {
        private static List<Eroi> eroiSalvati = new List<Eroi>();
        public bool Create(Eroi obj)
        {
            eroiSalvati.Add(obj);
            if (eroiSalvati.Contains(obj))
                return true;
            return false;
        }

        public bool Delete(Eroi obj)
        {
            eroiSalvati.Remove(obj);
            if(eroiSalvati.Contains(obj))
                return false;
            return true;
        }

        public IEnumerable<Eroi> GetAll()
        {
            return eroiSalvati;
        }

        public IEnumerable<Eroi> GetAllWithFilter(int ID)
        {
            List<Eroi> eroiDiUnGiocatore = new List<Eroi>();
            foreach(var item in eroiSalvati)
            {
                if (item.Giocatore == ID)
                    eroiDiUnGiocatore.Add(item);
            }
            return eroiDiUnGiocatore;
        }

        public Eroi GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Eroi obj)
        {
            foreach(var item in eroiSalvati)
            {
                if(item.ID == obj.ID)
                {
                    item.Livello = obj.Livello;
                    item.Punti = obj.Punti;
                    item.PuntiVita = obj.PuntiVita;
                    item.TempoTotale = obj.TempoTotale;
                    return true;
                }
            }
            return false;
        }
    }
}
