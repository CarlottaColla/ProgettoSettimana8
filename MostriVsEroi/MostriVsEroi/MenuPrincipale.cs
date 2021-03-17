using MostriVsEroi.MockRepository;
using MostriVsEroi.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MostriVsEroi.Core.Entità;

namespace MostriVsEroi
{
   public class MenuPrincipale
    {
        public static void Menu()
        {
            var serviceProvider = DIConfig.Configurazione();
            EroiService eroiService = serviceProvider.GetService<EroiService>();
            GiocatoriService giocatoriService = serviceProvider.GetService<GiocatoriService>();

            Console.Write("BENVENUTO A ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("MOSTRI");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" VS ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("EROI\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Inserisci il tuo nome: ");
            string nomeGiocatore = Console.ReadLine();
            //Dovrò confrontare se esiste o no: Se non esiste lo creo
            //Se non ha eroi può crearne uno nuovo o uscire dal gioco
            //Se ha già degli eroi può: continuare con un eroe, eliminarne uno, crearlo e uscire
            List<Giocatori> listaGiocatori = new List<Giocatori>();
            listaGiocatori = (List<Giocatori>)giocatoriService.ListaGiocatori();
            bool giocatoreTrovato = false;
            Giocatori giocatoreAttuale = new Giocatori();
            foreach(var item in listaGiocatori)
            {
                if(nomeGiocatore == item.Nome)
                {
                    giocatoreTrovato = true;
                    giocatoreAttuale = item;
                    break;
                }
            }

            if(giocatoreTrovato == false)
            {
                giocatoreAttuale.Nome = nomeGiocatore;
                giocatoreAttuale.Ruolo_ID = 1;
                giocatoriService.CreaGiocatore(giocatoreAttuale);
                List<Giocatori> listaGiocatori2 = new List<Giocatori>();
                listaGiocatori2 = (List<Giocatori>)giocatoriService.ListaGiocatori();
                foreach (var item in listaGiocatori2)
                {
                    if (nomeGiocatore == item.Nome)
                    {
                        giocatoreAttuale = item;
                        break;
                    }
                }

            }

            Console.WriteLine($"{giocatoreAttuale.ID}");

            bool esci = false;
            do
            {
                Console.WriteLine("Menu principale:");
                Console.WriteLine("1 - Crea un nuovo Eroe");
                Console.WriteLine("2 - Continua con un Eroe");
                Console.WriteLine("3 - Elimina un Eroe");
                Console.WriteLine("4 - Salva ed esci");
                int index = 0;
                do
                {
                    index = Convert.ToInt32(Console.ReadLine());
                    if( index < 1 || index > 5)
                    {
                        Console.WriteLine("Comando non riconosciuto, riprova");
                    }

                } while (index < 1 || index > 5);

                switch (index)
                {
                    case 1:
                        //Funziona
                        Console.WriteLine("Crea un nuovo Eroe");
                        Eroi eroeCreato = eroiService.CreateEroe(giocatoreAttuale);
                        Turno(eroeCreato);
                        break;
                    case 2:
                        //Funziona
                        //Da sistemare: si può vedere solo se è un giocatore già inserito
                        Console.WriteLine("Mostra gli eroi");
                        List<Eroi> listaEroi = new List<Eroi>();
                        listaEroi = (List<Eroi>)eroiService.GetAllEroi();
                        foreach(var item in listaEroi)
                        {
                            Console.WriteLine($"{item.ID} - {item.Nome} livello: {item.Livello}");
                        }
                        //Deve scegliere l'eroe
                        bool eroeGiusto = false;
                        do
                        {
                            Console.WriteLine("Inserisci il numero dell'eroe:");
                            int nEroe = Convert.ToInt32(Console.ReadLine());
                            foreach(var item in listaEroi)
                            {
                                if(nEroe == item.ID)
                                {
                                    eroeGiusto = true;
                                    Turno(item);
                                    break;
                                }
                            }
                        } while (eroeGiusto == false);
                        break;
                    case 3:
                        Console.WriteLine("Case 3");
                        break;
                    case 4:
                        Console.WriteLine("Case 5");
                        esci = true;
                        break;
                    default:
                        Console.WriteLine("Case default");
                        break;
                }
            } while (esci != true);
        }

        public static void Turno(Eroi eroe) {
            //Entra correttamente
            Console.WriteLine($"Entrato in turno con il giocatore: {eroe.Nome}");
                        
        }
    }
}
