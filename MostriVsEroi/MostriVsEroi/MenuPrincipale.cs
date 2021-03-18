using MostriVsEroi.MockRepository;
using MostriVsEroi.Service;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using MostriVsEroi.Core.Entità;
using System.Timers;
using System.Diagnostics;

namespace MostriVsEroi
{
    public class MenuPrincipale
    {
        public static void Menu()
        {
            //DIConfig
            var serviceProvider = DIConfig.Configurazione();
            EroiService eroiService = serviceProvider.GetService<EroiService>();
            GiocatoriService giocatoriService = serviceProvider.GetService<GiocatoriService>();
            MostroService mostroService = serviceProvider.GetService<MostroService>();

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

            //Confronto se il giocatore esiste o no: Se non esiste lo creo
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
                int ruolo = 0;
                do
                {
                    Console.WriteLine("Scegli se essere:\n1 - Utente\n2 - Admin");
                    ruolo = Convert.ToInt32(Console.ReadLine());
                } while (ruolo != 1 && ruolo != 2);
                giocatoreAttuale.Ruolo_ID = ruolo;
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

            bool esci = false;
            do
            {
                //Resetto la console ogni volta che torno al menu
                Console.Clear();
                List<Eroi> listaEroi = new List<Eroi>();
                listaEroi = (List<Eroi>)eroiService.EroiDiUnGiocatore(giocatoreAttuale.ID);

                //Titolo
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("MOSTRI");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(" VS ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("EROI\n");
                Console.ForegroundColor = ConsoleColor.White;

                //Se ha già degli eroi può: continuare con un eroe, eliminarne uno, crearlo e uscire
                if (listaEroi.Count > 0)
                {
                    Console.WriteLine("Menu principale:");
                    Console.WriteLine("1 - Crea un nuovo Eroe");
                    Console.WriteLine("2 - Continua con un Eroe");
                    Console.WriteLine("3 - Elimina un Eroe");
                    Console.WriteLine("4 - Salva ed esci");

                    //Se è admin può creare e cancellare i mostri e vedere le statistiche dei giocatori
                    if(giocatoreAttuale.Ruolo_ID == 2)
                    {
                        Console.WriteLine("5 - Crea un nuovo mostro");
                        Console.WriteLine("6 - Elimina un mostro");
                        Console.WriteLine("7 - Guarda le statistiche");
                    }
                    int index = 0;
                    //Se non è admin può scegliere solo 4 opzioni
                    if (giocatoreAttuale.Ruolo_ID == 1)
                    {
                        do
                        {
                            index = Convert.ToInt32(Console.ReadLine());
                            if (index < 1 || index > 5)
                            {
                                Console.WriteLine("Comando non riconosciuto, riprova");
                            }
                        } while (index < 1 || index > 5);
                    }
                    //Se è admin può scegliere tra 7 opzioni
                    else
                    {
                        do
                        {
                            index = Convert.ToInt32(Console.ReadLine());
                            if (index < 1 || index > 7)
                            {
                                Console.WriteLine("Comando non riconosciuto, riprova");
                            }
                        } while (index < 1 || index > 7);
                    }

                    switch (index)
                    {
                        case 1:
                            //Crea un nuovo eroe
                            Eroi eroeCreato = eroiService.CreateEroe(giocatoreAttuale);
                            Turno(eroeCreato);
                            break;
                        case 2:
                            //Può vedere solo i suoi eroi
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
                                        //Quando seleziona l'eroe inizia il turno
                                        Turno(item);
                                        break;
                                    }
                                }
                            } while (eroeGiusto == false);
                            break;
                        case 3:
                            //Eliminare un eroe
                            foreach (var item in listaEroi)
                            {
                                Console.WriteLine($"{item.ID} - {item.Nome} livello: {item.Livello}");
                            }
                            //Deve scegliere l'eroe
                            bool eroeDaEliminare = false;
                            do
                            {
                                Console.WriteLine("Inserisci il numero dell'eroe da eliminare:");
                                int nEroe = Convert.ToInt32(Console.ReadLine());
                                foreach (var item in listaEroi)
                                {
                                    if (nEroe == item.ID)
                                    {
                                        eroeDaEliminare = true;
                                        //Quando seleziona l'eroe lo elimino
                                        eroiService.EliminaEroe(item);
                                        break;
                                    }
                                }
                            } while (eroeDaEliminare == false);
                            break;
                        case 4:
                            //Esce
                            Console.WriteLine("Arrivederci!");
                            esci = true;
                            break;
                        //Questi può sceglierli solo un admin
                        case 5:
                            //Crea un nuovo mostro
                            mostroService.CreaMostro();
                            break;
                        case 6:
                            //Elimina un mostro
                            break;
                        case 7:
                            //Mostra statistiche dei giocatori
                            break;
                        default:
                            Console.WriteLine("Case default");
                            break;
                    }
                }
                else
                {
                    //Se non ha eroi può crearne uno nuovo o uscire dal gioco
                    Console.WriteLine("Menu principale:");
                    Console.WriteLine("1 - Crea un nuovo Eroe");
                    Console.WriteLine("2 - Salva ed esci");

                    //Se è admin può creare e cancellare i mostri e vedere le statistiche dei giocatori
                    if (giocatoreAttuale.Ruolo_ID == 2)
                    {
                        Console.WriteLine("3 - Crea un nuovo mostro");
                        Console.WriteLine("4 - Elimina un mostro");
                        Console.WriteLine("5 - Guarda le statistiche");
                    }
                    int index = 0;
                    //Se non è admin può scegliere solo 2 opzioni
                    if (giocatoreAttuale.Ruolo_ID == 1)
                    {
                        do
                        {
                            index = Convert.ToInt32(Console.ReadLine());
                            if (index != 1 && index != 2)
                            {
                                Console.WriteLine("Comando non riconosciuto, riprova");
                            }
                        } while (index != 1 && index != 2);
                    }
                    //Se è admin può scegliere tra 5 opzioni
                    else
                    {
                        do
                        {
                            index = Convert.ToInt32(Console.ReadLine());
                            if (index < 1 || index > 5)
                            {
                                Console.WriteLine("Comando non riconosciuto, riprova");
                            }
                        } while (index < 1 || index > 5);
                    }

                    switch (index)
                    {
                        case 1:
                            //Crea un nuovo eroe
                            Eroi eroeCreato = eroiService.CreateEroe(giocatoreAttuale);
                            Turno(eroeCreato);
                            break;
                        case 2:
                            //Esce
                            Console.WriteLine("Arrivederci!");
                            esci = true;
                            break;
                        //Questi può sceglierli solo un admin
                        case 3:
                            //Crea un nuovo mostro
                            mostroService.CreaMostro();
                            break;
                        case 4:
                            //Elimina un mostro
                            break;
                        case 5:
                            //Mostra statistiche dei giocatori
                            break;
                        default:
                            Console.WriteLine("Case default");
                            break;
                    }
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

            //Numero dei turni
            int n = 0;

            //Iniziare il timer del tempo di gioco dell'eroe
            Stopwatch watch = new Stopwatch();
            watch.Start();
            do
            {
                //A inizio di ogni turno cancello la console
                Console.Clear();
                //Aumento il nomero dei turni;
                n++;

                //A ogni turno cambio il Mostro
                //Richiamo un mostro random di livello >= livello eroe
                List<Mostri> mostri = new List<Mostri>(mostroService.ListaMostriLivelloEroe(eroe.Livello));
                Random random = new Random();
                int numRandom = random.Next(mostri.Count);
                Mostri mostroDaSconfiggere = mostri[numRandom];
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Turno numero : " + n);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(mostroDaSconfiggere.Nome + " livello " + mostroDaSconfiggere.Livello);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(" VS ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{eroe.Nome} livello {eroe.Livello}\n");
                Console.ForegroundColor = ConsoleColor.White;

                //Resetto che nessuno è morto
                morto = false;
                do
                {
                    //Gioca sempre prima l'eroe
                    int danniSubiti = GiocaEroe(eroe, mostroDaSconfiggere, out bool danniEroe);
                    //Se la fuga ha successo
                    if (danniSubiti > 0 && danniEroe)
                    {
                        //Li sottraggo dai punti
                        eroe.Punti -= danniSubiti;

                        //finisce il turno
                        break;
                    }
                    //Attacco il mostro
                    else if (danniSubiti > 0)
                    {
                        mostroDaSconfiggere.PuntiVita -= danniSubiti;
                        Console.WriteLine($"Il mostro ha {mostroDaSconfiggere.PuntiVita} punti vita");
                        if (mostroDaSconfiggere.PuntiVita <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Hai Vinto!!!");
                            Console.ForegroundColor = ConsoleColor.White;
                            //Devo aggiungere i punti vittoria all'eroe: livello mostro * 10
                            eroe.Punti += mostroDaSconfiggere.Livello * 10;

                            //Controllo se è passato di livello
                            ControlloLivello(ref eroe);

                            //Controllo se ha vinto (è arrivato a 200 punti)
                            if(Vittoria(eroe))
                            {
                                //Se vince torna al menu principale salvando le statistiche
                                watch.Stop();
                                Console.WriteLine("Watch: " + watch.ElapsedMilliseconds);
                                TimeSpan time = new TimeSpan(watch.ElapsedMilliseconds * 10000);
                                Console.WriteLine("Time : " + time);
                                eroe.TempoTotale += watch.ElapsedMilliseconds;
                                eroiService.SalvaProgressi(eroe);
                                return;
                            }
                            //Il mostro è morto finisce il turno
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
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Hai perso...");
                            Console.ForegroundColor = ConsoleColor.White;
                            //L'eroe è morto, lo cancello
                            eroiService.EliminaEroe(eroe);
                            morto = true;
                        }
                    }
                } while (morto == false);

                //Se è morto l'eroe torna direttamente al menu
                if (eroe.PuntiVita <= 0) break;

                Console.WriteLine("Il turno è finito\nPer giocare ancora scrivi 'si', altrimenti salva e torna al menu.");
                giocaAncora = Console.ReadLine();
                if(giocaAncora != "si")
                {
                    //Fermo il timer
                    watch.Stop();
                    Console.WriteLine("Watch: " + watch.ElapsedMilliseconds);
                    TimeSpan time = new TimeSpan(watch.ElapsedMilliseconds * 10000);
                    Console.WriteLine("Time : " + time);
                    //finita la partita salvo i progressi (solo se l'eroe non è morto)
                    //eroe.TempoTotale += watch.ElapsedMilliseconds;
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
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Fuga eseguita con successo");
                    Console.ForegroundColor = ConsoleColor.White;
                    danniEroe = true;
                    return mostro.Livello * 5;
                }
            }
            //Ritorna -1 solo se la fuga non è riuscita
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Fuga non riuscita...");
            Console.ForegroundColor = ConsoleColor.White;
            return -1;
        }

        public static bool Fuga()
        {
            //Creo un numero random: se è pari allora la fuga ha successo
            Random random = new Random();
            int numRandom = random.Next(100);
            return numRandom % 2 == 0;
        }

        public static void ControlloLivello(ref Eroi eroe)
        {
            //Se l'eroe è di livello 5 non può più passare di livello
            if(eroe.Livello == 5)
            {
                return;
            }

            //DIConfig
            var serviceProvider = DIConfig.Configurazione();
            LivelliService livelliService = serviceProvider.GetService<LivelliService>();

            //Prendo la lista dei livelli
            List<Livelli> listaLivelli = new List<Livelli>(livelliService.ListaLivelli());

            //Confronto i punti eroe con i punti massimi del suo livello, se li supera passa di livello
            foreach(var item in listaLivelli)
            {
                if(item.ID == eroe.Livello + 1) //Livello successivo
                {
                    if(item.PuntiPassaggio <= eroe.Punti)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Congratulazioni sei passato di livello!!");
                        eroe.Livello++;
                        Console.WriteLine("Sei passato al livello: " + eroe.Livello);
                        eroe.PuntiVita = item.PuntiVita;
                        Console.WriteLine("Ora i tuoi punti vita sono: " + eroe.PuntiVita);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                }
            }
        }

        public static bool Vittoria(Eroi eroe)
        {
            //Si vince se l'eroe raggiunge i 200 punti
            if(eroe.Punti >= 200)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("CONGRATULAZIONI, HAI VINTO IL GIOCO!!");
                Console.ResetColor();
                return true;
            }

            return false;
        }

    }
}
