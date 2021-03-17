using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.MockRepository
{
    public class MockArmiRepository : IArmiRepository
    {
        public void Create(Armi obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Armi obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Armi> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Armi> GetAllWithFilter(int ID)
        {
            //L'id che mi passa è per dire di che classe è
            List<Armi> armi = new List<Armi>()
            {
                new Armi() {ID = 1, Nome="Spadone di ferro", PuntiDanno= 10, ClassiID = 1 },
                new Armi() {ID = 2, Nome="Arco", PuntiDanno= 8, ClassiID =2 },
                new Armi() {ID = 3, Nome="Ascia", PuntiDanno= 11, ClassiID=1 },

            };
            //Ritorna solo quelle della classe
            List<Armi> armiDellaClasse = new List<Armi>();
            foreach (var item in armi)
            {
                if (item.ClassiID == ID)
                {
                    armiDellaClasse.Add(item);
                }
            }
            return armiDellaClasse;
        }

        public Armi GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Armi obj)
        {
            throw new NotImplementedException();
        }
    }
}
