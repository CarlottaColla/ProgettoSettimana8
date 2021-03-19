using MostriVsEroi.Core.Entità;
using System;
using System.Collections.Generic;
using System.Text;
using MostriVsEroi.Core.Interfacce;

namespace MostriVsEroi.MockRepository
{
    public class MockClassiRepository : IClassiRepository
    {
        private static List<Classi> classiSalvate = new List<Classi>() { 
            new Classi() {ID = 1, Nome = "Guerriero", IsEroe = true},
            new Classi() {ID = 2, Nome = "Mago", IsEroe = true},
            new Classi() {ID = 3, Nome = "Orco", IsEroe = false},
            new Classi() {ID = 4, Nome = "SignoreDelMale", IsEroe = false},
        };
        public bool Create(Classi obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Classi obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Classi> GetAll()
        {
            return classiSalvate;
        }

        public IEnumerable<Classi> GetAllWithFilter(int filtro)
        {
            List<Classi> classiEroe = new List<Classi>();
            //Il filtro che mi passa è 1 se vuole le classi degli eroi e 0 se vuole quelle dei mostri
            //è int perchè nel database salva 0 o 1, devo fare la conversione
            bool eroe;
            if (filtro == 1)
                eroe = true;
            else
                eroe = false;
            foreach(var item in classiSalvate)
            {
                if (item.IsEroe == eroe)
                    classiEroe.Add(item);
            }
            return classiEroe;
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
