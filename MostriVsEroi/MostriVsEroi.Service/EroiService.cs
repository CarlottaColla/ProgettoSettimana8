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

            //Il nome deve essere univoco, lo confronto con gli eroi già inseriti
            Console.WriteLine("Inserisci il nome del tuo eroe: ");
            string nome = Console.ReadLine();
            bool univoco = true;
            do
            {
                univoco = true;
                List<Eroi> listaEroi = new List<Eroi>(GetAllEroi());
                foreach (var item in listaEroi)
                {
                    if (nome == item.Nome)
                    {
                        Console.WriteLine("Il nome dell'eroe deve essere univoco, riprova: ");
                        nome = Console.ReadLine();
                        univoco = false;
                    }
                }
            } while (univoco != true);
            eroe.Nome = nome;
            Console.WriteLine("Le classi disponibili sono: ");
            //Mostra lista di classi con filtro su eroe
            //var classiService = new ClassiService(new MockClassiRepository());
            var classiService = new ClassiService(new ADOClassiRepos());
            var classiEroi = classiService.ListaClassiConFiltro(1);
            foreach(var item in classiEroi)
            {
                Console.WriteLine($"{item.ID} - {item.Nome}");
            }
            bool trovato = false;
            Console.WriteLine("Inserisci il numero della classe: ");
            do
            {
                bool corretto = Int32.TryParse(Console.ReadLine(), out int classe);
                if (corretto == true)
                {
                    foreach (var item in classiEroi)
                    {
                        if (item.ID == classe)
                        {
                            trovato = true;
                            eroe.Classe = item.ID;
                            break;
                        }
                    }
                }
                if(corretto == false || trovato == false)
                    Console.WriteLine("Classe non trovata, riprova");
            } while (trovato == false);

            //Mostra lista di armi con filtro sulla classe dell'eroe
            //var armiService = new ArmiService(new MockArmiRepository());
            var armiService = new ArmiService(new ADOArmiRepos());
            var armiEroe = armiService.ListaArmiConFiltro(eroe.Classe);
            foreach (var item in armiEroe)
            {
                Console.WriteLine($"{item.ID} - {item.Nome}");
            }
            bool trovataArma = false;
            Console.WriteLine("Inserisci il numero dell'arma che vuoi: ");
            do
            {
                bool corretto = Int32.TryParse(Console.ReadLine(), out int idArma);
                if (corretto == true)
                {
                    foreach (var item in armiEroe)
                    {
                        if (item.ID == idArma)
                        {
                            trovataArma = true;
                            eroe.Arma = item.ID;
                            break;
                        }
                    }
                }
                if(corretto == false || trovataArma == false)
                    Console.WriteLine("Arma non trovata, riprova:");
            } while (trovataArma == false);
            eroe.Giocatore = giocatore.ID;

            //Tutti i nuovi eroi sono di livello 1
            eroe.Livello = 1;

            //Impostato a 0 perchè non ha ancora mai giocato
            eroe.TempoTotale = 0;

            //I punti vita sono relativi al livello
            //var livelliService = new LivelliService(new MockLivelliRepository());
            var livelliService = new LivelliService(new ADOLivelliRepos());
            var livelloDB = livelliService.RitornaLivello(1);
            eroe.PuntiVita = livelloDB.PuntiVita;

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
            return new Eroi();

        }

        public IEnumerable<Eroi> GetAllEroi()
        {
            return _repo.GetAll();
        }

        public IEnumerable<Eroi> EroiDiUnGiocatore(int id)
        {
            return _repo.GetAllWithFilter(id);
        }

        public void EliminaEroe(Eroi obj)
        {
            _repo.Delete(obj);
        }

        public void SalvaProgressi(Eroi obj)
        {
            _repo.Update(obj);
        }
    }
}
