using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using System;
using MostriVsEroi.MockRepository;
using System.Collections.Generic;
using MostriVsEroi.ADO_Repository;

namespace MostriVsEroi.Service
{
    public class EroiService 
    {
        private IEroiRepository _repo;

        public EroiService (IEroiRepository repo)
        {
            _repo = repo;
        }

        public Eroi CreateEroe(Giocatori giocatore)
        {
            Eroi eroe = new Eroi();
            //ID, Nome, Classe, Arma, Livello, TempoTotale = 0, PuntiVita
            //L'id lo inserisce il db
            Console.WriteLine("Inserisci il nome del tuo eroe: ");
            string nome = Console.ReadLine();
            eroe.Nome = nome;
            Console.WriteLine("Le classi disponibili sono: ");
            //Mostra lista di classi con filtro su eroe
            var classiService = new ClassiService(new ADOClassiRepos());
            var classiEroi = classiService.ListaClassiConFiltro(1);
            foreach(var item in classiEroi)
            {
                Console.WriteLine($"{item.ID} - {item.Nome}");
            }
            bool trovato = false;
            do
            {
                Console.WriteLine("Inserisci il numero della classe che vuoi: ");
                int idClasse = Convert.ToInt32(Console.ReadLine());
                foreach (var item in classiEroi)
                {
                    if (item.ID == idClasse)
                    {
                        trovato = true;
                        eroe.Classe = item.ID;
                        break;
                    }
                }
            } while (trovato == false);
            //Mostra lista di armi con filtro su eroe
            var armiService = new ArmiService(new ADOArmiRepos());
            var armiEroe = armiService.ListaArmiConFiltro(eroe.Classe);
            foreach (var item in armiEroe)
            {
                Console.WriteLine($"{item.ID} - {item.Nome}");
            }
            bool trovataArma = false;
            do
            {
                Console.WriteLine("Inserisci il numero dell'arma che vuoi: ");
                int idArma = Convert.ToInt32(Console.ReadLine());
                foreach (var item in armiEroe)
                {
                    if (item.ID == idArma)
                    {
                        trovataArma = true;
                        eroe.Arma = item.ID;
                        break;
                    }
                }
            } while (trovataArma == false);
            eroe.Giocatore = giocatore.ID;
            eroe.Livello = 1;
            eroe.TempoTotale = 0;
            eroe.PuntiVita = 20;
            _repo.Create(eroe);

            //Devo restituirlo creato
            List<Eroi> eroi = new List<Eroi> (GetAllEroi());
            foreach(var item in eroi)
            {
                //Basta il nome perchè è univoco
                if(item.Nome == nome)
                {
                    return item;
                }

            }
            Console.WriteLine("Errore nella Creazione");
            return new Eroi();

        }

        public IEnumerable<Eroi> GetAllEroi()
        {
            return _repo.GetAll();
        }
    }
}
