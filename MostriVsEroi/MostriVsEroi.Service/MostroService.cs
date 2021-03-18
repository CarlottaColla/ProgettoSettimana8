using MostriVsEroi.ADO_Repository;
using MostriVsEroi.Core.Entità;
using MostriVsEroi.Core.Interfacce;
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
            do
            {
                Console.WriteLine("Scegli il numero del mostro da eliminare: ");
                nMostro = Convert.ToInt32(Console.ReadLine());
                foreach (var item in tuttiIMostri)
                {
                    if(nMostro == item.ID)
                    {
                        if (_repo.Delete(item))
                            Console.WriteLine("Mostro eliminato");
                        return;
                    }
                }
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
                if (item.Livello == livello)
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
            mostro.Nome = nome;
            Console.WriteLine("Le classi disponibili sono: ");
            //Mostra lista di classi con filtro su mostro
            var classiService = new ClassiService(new ADOClassiRepos());
            var classiEroi = classiService.ListaClassiConFiltro(0);
            foreach (var item in classiEroi)
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
                        mostro.Classe = item.ID;
                        break;
                    }
                }
            } while (trovato == false);

            //Mostra lista di armi con filtro sula categoria del mostro
            var armiService = new ArmiService(new ADOArmiRepos());
            var armiEroe = armiService.ListaArmiConFiltro(mostro.Classe);
            Console.WriteLine("Le armi disponibili sono: ");
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
                        mostro.Arma = item.ID;
                        break;
                    }
                }
            } while (trovataArma == false);
            int livello = 0;
            do
            {
                Console.WriteLine("Inserisci il livello del mostro da 1 a 5:");
                livello = Convert.ToInt32(Console.ReadLine());
                if (livello > 0 && livello < 6)
                    break;
            } while (true);
            mostro.Livello = livello;

            //I punti vita sono relativi al livello
            var livelliService = new LivelliService(new ADOLivelliRepos());
            var livelloDB = livelliService.RitornaLivello(livello);
            mostro.PuntiVita = livelloDB.PuntiVita;
            _repo.Create(mostro);
        }
    }
}
