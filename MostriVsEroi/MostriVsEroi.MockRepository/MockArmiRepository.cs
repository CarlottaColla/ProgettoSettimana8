using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.MockRepository
{
    public class MockArmiRepository : IArmiRepository
    {
        private static List<Armi> armiSalvate = new List<Armi>()
        {
            new Armi () {ID =1, ClassiID =1, Nome="Spada", PuntiDanno=10},
            new Armi () {ID =2, ClassiID =2, Nome="Bacchetta", PuntiDanno=11},
            new Armi () {ID =3, ClassiID =3, Nome="Mazza", PuntiDanno=9},
            new Armi () {ID =4, ClassiID =4, Nome="Arco", PuntiDanno=13}
        };
        public bool Create(Armi obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Armi obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Armi> GetAll()
        {
            return armiSalvate;
        }

        public IEnumerable<Armi> GetAllWithFilter(int ID)
        {
            //L'id che mi passa è per dire di che classe è
            //Ritorna solo quelle della classe
            List<Armi> armiDellaClasse = new List<Armi>();
            foreach (var item in armiSalvate)
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
            foreach(var item in armiSalvate)
            {
                if (item.ID == ID)
                    return item;
            }
            return new Armi();
        }

        public bool Update(Armi obj)
        {
            throw new NotImplementedException();
        }
    }
}
