using MostriVsEroi.Core.Entità;
using System;
using System.Collections.Generic;
using System.Text;
using MostriVsEroi.Core.Interfacce;

namespace MostriVsEroi.MockRepository
{
    public class MockClassiRepository : IClassiRepository
    {
        public void Create(Classi obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Classi obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Classi> GetAll()
        {
            throw new NotImplementedException();
        }

        //Dovrebbe servire perchè un admin può aggiungere anche un mostro
        public IEnumerable<Classi> GetAllWithFilter(int ID)
        {
            //L'id che mi passa è per dire che è un eroe
            //Ritorna tutti gli eroi
            List<Classi> classi = new List<Classi>()
            {
                new Classi () {ID = 1, Nome = "Mago", IsEroe = true},
                new Classi () {ID = 2, Nome = "Guerriero", IsEroe = true}
            };
            return classi;

        }

        public Classi GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Classi obj)
        {
            throw new NotImplementedException();
        }
    }
}
