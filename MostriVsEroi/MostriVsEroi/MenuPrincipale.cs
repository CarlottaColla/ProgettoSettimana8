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
            foreach (var item in listaGiocatori)
            {
                if (nomeGiocatore == item.Nome)
                {
                    giocatoreTrovato = true;
                    giocatoreAttuale = item;
                    break;
                }
            }

            if (giocatoreTrovato == false)
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
                    if (index < 1 || index > 5)
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
//Può vedere solo i suoi eroi!!
                        listaEroi = (List<Eroi>)eroiService.GetAllEroi();
                        foreach (var item in listaEroi)
                        {
                            Console.WriteLine($"{item.ID} - {item.Nome} livello: {item.Livello}");
                        }
                        //Deve scegliere l'eroe
                        bool eroeGiusto = false;
                        do
                        {
                            Console.WriteLine("Inserisci il numero dell'eroe:");
                            int nEroe = Convert.ToInt32(Console.ReadLine());
                            foreach (var item in listaEroi)
                            {
                                if (nEroe == item.ID)
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

        public static void Turno(Eroi eroe) 
        {
            //DIConfig
            var serviceProvider = DIConfig.Configurazione();
            MostroService mostroService = serviceProvider.GetService<MostroService>();
            ArmiService armiService = serviceProvider.GetService<ArmiService>();
            EroiService eroiService = serviceProvider.GetService<EroiService>();

            //Il turno finisce quando l'eroe o il mostro muiono
            bool morto = false;

            //La partita finisce quando l'utente decide di tornare al menu
            string giocaAncora;

            //Richiamo un mostro random di livello >= livello eroe
            List<Mostri> mostri = new List<Mostri>(mostroService.ListaMostriLivelloEroe(eroe.Livello));
            Random random = new Random();
            int numRandom = random.Next(mostri.Count);
            Mostri mostroDaSconfiggere = mostri[numRandom];

            do
            {
                //Iniziare il timer del tempo di gioco dell'eroe
                do
                {
                    //Gioca sempre prima l'eroe
                    int danniSubiti = GiocaEroe(eroe, mostroDaSconfiggere, out bool danniEroe);
                    //Se la fuga ha successo
                    if (danniSubiti > 0 && danniEroe)
                    {
                        eroe.PuntiVita -= danniSubiti;
                        Console.WriteLine($"L'eroe ha {eroe.PuntiVita} punti vita");

                        if (eroe.PuntiVita <= 0)
                        {
                            Console.WriteLine("L'eroe è morto");
                            morto = true;
                            eroiService.EliminaEroe(eroe);
                        }
                        //finisce il turno
                        break;
                    }
                    else if (danniSubiti > 0)
                    {
                        mostroDaSconfiggere.PuntiVita -= danniSubiti;
                        Console.WriteLine($"Il mostro ha {mostroDaSconfiggere.PuntiVita} punti vita");
                        if (mostroDaSconfiggere.PuntiVita <= 0)
                        {
                            Console.WriteLine("Il mostro è morto");
                            //Devo aggiungere i punti vittoria all'eroe: livello mostro * 10
                            eroe.Punti += mostroDaSconfiggere.Livello * 10;
                            morto = true;
                        }
                    }
                    //Se la fuga fallisce
                    //è il turno del mostro se uno dei due non è morto
                    if (morto != true)
                    {
                        Console.WriteLine("è il tuno del mostro");
                        int danniMostro = GiocaMostro(mostroDaSconfiggere);
                        eroe.PuntiVita -= danniMostro;
                        Console.WriteLine($"L'eroe ha {eroe.PuntiVita} punti vita");

                        if (eroe.PuntiVita <= 0)
                        {
                            Console.WriteLine("L'eroe è morto");
                            eroiService.EliminaEroe(eroe);
                            morto = true;
                            //Devo cancellarlo
                        }
                    }
                } while (morto == false);
                //Se è morto l'eroe torna direttamente al menu
                if (eroe.PuntiVita <= 0) break;

                Console.WriteLine("Il turno è finito\nPer giocare ancora scrivi 'si', altrimenti salva e torna al menu.");
                giocaAncora = Console.ReadLine();
                if(giocaAncora != "si")
                {
//finita la partita salvo i progressi (solo se l'eroe non è morto)
                    //eroiService.SalvaProgressi(eroe);
                }
            } while (giocaAncora == "si");
            
        }

        public static int GiocaMostro(Mostri mostro)
        {
            //DIConfig
            var serviceProvider = DIConfig.Configurazione();
            ArmiService armiService = serviceProvider.GetService<ArmiService>();

            //il mostro attacca sempre
            Armi armaMostro = new Armi();
            armaMostro = armiService.ArmaPersonaggio(mostro.Arma);
            return armaMostro.PuntiDanno;
        }

        //Int perchè così ritorna i danni subiti dal mostro/eroe e li sottraggo in Turno
        //danniEroe serve per vedere a chi togliere i danni
        //ritorna -1 se la fuga fallisce
        public static int GiocaEroe(Eroi eroe, Mostri mostro, out bool danniEroe) {
            //DIConfig
            var serviceProvider = DIConfig.Configurazione();
            ArmiService armiService = serviceProvider.GetService<ArmiService>();

            //Se fallisce la fuga non lo guardo
            danniEroe = false;

            int scelta = 0;
            do
            {
                Console.WriteLine("è il turno dell'eroe cosa vuoi fare?\n" +
                    "1 - Attacca!\n" +
                    "2 - Scappa");
                scelta = Convert.ToInt32(Console.ReadLine());
                if(scelta == 1 || scelta == 2)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Comando non valido, riprova");
                }
            } while(true);

            //Attacco
            if(scelta == 1)
            {
                //Devo prendere i punti danno dell'arma
                Armi armaGiocatore = new Armi();
                armaGiocatore = armiService.ArmaPersonaggio(eroe.Arma);
                //int danniInflitti = armaGiocatore.PuntiDanno;
                //Li passo al turno così modifico direttamente il mostro
                danniEroe = false;
                Console.WriteLine("Hai attaccato il mostro con successo");
                return armaGiocatore.PuntiDanno;
            }
            else
            {
                if (Fuga())
                {
                    //I punti sottratti all'eroe sono livello mostro * 5
                    Console.WriteLine("Fuga eseguita con successo");
                    danniEroe = true;
                    return mostro.Livello * 5;
                }
            }
            //Ritorna -1 solo se la fuga non è riuscita
            Console.WriteLine("Fuga non riuscita..");
            return -1;
        }

        public static bool Fuga()
        {
            //Creo un numero random: se è pari allora la fuga ha successo
            Random random = new Random();
            int numRandom = random.Next(100);
            return numRandom % 2 == 0;
        }

    }
}
