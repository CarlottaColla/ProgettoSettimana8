using MostriVsEroi.ADO_Repository;
using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
using MostriVsEroi.MockRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.Service
{
    public class MostroService
    {
        private IMostroRepository _repo;

        public MostroService(IMostroRepository repo)
        {
            _repo = repo;
        }

        public void EliminaMostro()
        {
            //Mostro quali sono i mostri disponibili
            List<Mostri> tuttiIMostri = new List<Mostri>(TuttiIMostri());
            foreach(var item in tuttiIMostri)
            {
                Console.WriteLine($"{item.ID} - {item.Nome} di lievllo {item.Livello}");
            }
            
            int nMostro = 0;
            Console.WriteLine("Scegli il numero del mostro da eliminare: ");
            do
            {

                bool corretto = Int32.TryParse(Console.ReadLine(), out nMostro);
                if (corretto == true)
                {
                    foreach (var item in tuttiIMostri)
                    {
                        if (nMostro == item.ID)
                        {
                            _repo.Delete(item);
                            return;
                        }
                    }

                }
                Console.WriteLine("Numero non corretto, riprova:");
            } while (true);
        }

        public List<Mostri> TuttiIMostri()
        {
            return (List<Mostri>)_repo.GetAll();
        }

        public IEnumerable<Mostri> ListaMostriLivelloEroe(int livello)
        {
            List<Mostri> mostri = new List<Mostri>(TuttiIMostri());
            List<Mostri> mostriUtilizzabili = new List<Mostri>();
            foreach (var item in mostri)
            {
                if (item.Livello <= livello)
                {
                    mostriUtilizzabili.Add(item);
                }
            }
            return mostriUtilizzabili;
        }

        public void CreaMostro()
        {
            Mostri mostro = new Mostri();
            //Nome, Classe, Arma, Livello, PuntiVita
            Console.WriteLine("Inserisci il nome del mostro: ");
            string nome = Console.ReadLine();
            //Il nome deve essere univoco
            bool univoco = true;
            do
            {
                univoco = true;
                List<Mostri> listaMostri = new List<Mostri>(TuttiIMostri());
                foreach (var item in listaMostri)
                {
                    if (nome == item.Nome)
                    {
                        Console.WriteLine("Il nome del mostro deve essere univoco, riprova: ");
                        nome = Console.ReadLine();
                        univoco = false;
                    }
                }
            } while (univoco != true);
            mostro.Nome = nome;
            Console.WriteLine("Le classi disponibili sono: ");
            //Mostra lista di classi con filtro su mostro
            //var classiService = new ClassiService(new MockClassiRepository());
            var classiService = new ClassiService(new ADOClassiRepos());
            var classiEroi = classiService.ListaClassiConFiltro(0);
            foreach (var item in classiEroi)
            {
                Console.WriteLine($"{item.ID} - {item.Nome}");
            }
            bool trovato = false; 
            Console.WriteLine("Inserisci il numero della classe che vuoi: ");
            do
            {
                bool corretto = Int32.TryParse(Console.ReadLine(), out int idClasse);
                if (corretto == true)
                {
                    foreach (var item in classiEroi)
                    {
                        if (item.ID == idClasse)
                        {
                            trovato = true;
                            mostro.Classe = item.ID;
                            break;
                        }
                    }
                }
                if(trovato != true || corretto == false)
                    Console.WriteLine("Numero non corretto, riprova:");
            } while (trovato == false);

            //Mostra la lista delle armi con filtro sulla categoria del mostro
            //var armiService = new ArmiService(new MockArmiRepository());
            var armiService = new ArmiService(new ADOArmiRepos());
            var armiEroe = armiService.ListaArmiConFiltro(mostro.Classe);
            Console.WriteLine("Le armi disponibili sono: ");
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
                            mostro.Arma = item.ID;
                            break;
                        }
                    }
                }
                if (trovataArma != true || corretto == false)
                    Console.WriteLine("Numero non corretto, riprova:");
            } while (trovataArma == false);
            //L'admin può scegliere il livello del mostro
            Console.WriteLine("Inserisci il livello del mostro da 1 a 5:");
            int livello = 0;
            do
            {
                bool corretto = Int32.TryParse(Console.ReadLine(), out livello);
                if (corretto == true && livello > 0 && livello < 6)
                    break;
                Console.WriteLine("Livello non corretto, riprova:");
            } while (true);
            mostro.Livello = livello;

            //I punti vita sono relativi al livello
            //var livelliService = new LivelliService(new MockLivelliRepository());
            var livelliService = new LivelliService(new ADOLivelliRepos());
            var livelloDB = livelliService.RitornaLivello(livello);
            mostro.PuntiVita = livelloDB.PuntiVita;
            _repo.Create(mostro);
        }
    }
}
