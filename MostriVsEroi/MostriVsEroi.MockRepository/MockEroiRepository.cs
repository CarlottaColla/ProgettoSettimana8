using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.MockRepository
{
    public class MockEroiRepository : IEroiRepository
    {
        public void Create(Eroi obj)
        {
            //Insert
            new Eroi()
            {
                ID = obj.ID,
                Nome = obj.Nome,
                Classe = obj.Classe,
                Arma = obj.Arma,
                Livello = obj.Livello,
                TempoTotale = 0,
                PuntiVita = obj.PuntiVita
            };
            Console.WriteLine("Eroe creato!");
        }

        public bool Delete(Eroi obj)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Eroi> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Eroi> GetAllWithFilter(int ID)
        {
            throw new NotImplementedException();
        }

        public Eroi GetByID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Update(Eroi obj)
        {
            throw new NotImplementedException();
        }
    }
}
