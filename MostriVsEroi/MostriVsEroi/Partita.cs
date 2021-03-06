using MostriVsEroi.Core.Entità;
using MostriVsEroi.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace MostriVsEroi
{
    public class Partita
    {
        //In questa classe ci sono tutte le funzioni relative alla partita

        /// <summary>
        /// Gestisce tutta la partita
        /// </summary>
        /// <param name="eroe">Prende l'eroe scelto dal giocatore</param>
        public static void Turno(Eroi eroe)
        {
            //DIConfig
            var serviceProvider = DIConfig.Configurazione();
            MostroService mostroService = serviceProvider.GetService<MostroService>();
            ArmiService armiService = serviceProvider.GetService<ArmiService>();
            EroiService eroiService = serviceProvider.GetService<EroiService>();
            LivelliService livelliService = serviceProvider.GetService<LivelliService>();

            //Il turno finisce quando l'eroe o il mostro muiono
            bool morto = false;

            //La partita finisce quando l'utente decide di tornare al menu
            bool giocaAncora;

            //Numero dei turni
            int n = 0;

            //Iniziare il timer del tempo di gioco dell'eroe
            Stopwatch watch = new Stopwatch();
            watch.Start();
            do
            {
                //A inizio di ogni turno cancello la console
                Console.Clear();
                //Aumento il numero dei turni;
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

                //A inizio turno resetto che nessuno è morto
                morto = false;

                //Con il Mock i danni al mostro vengono salvati, devo resettarlo
                //Con il database non serve
                //mostroDaSconfiggere.PuntiVita = livelliService.RitornaLivello(mostroDaSconfiggere.Livello).PuntiVita;
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
                        //Se il mostro non è morto scrivo quanti punti vita gli rimangono
                        Console.ForegroundColor = ConsoleColor.Blue;
                        if(mostroDaSconfiggere.PuntiVita > 0)
                            Console.WriteLine($"Il mostro ha {mostroDaSconfiggere.PuntiVita} punti vita");
                        Console.ResetColor();
                        if (mostroDaSconfiggere.PuntiVita <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Il mostro è morto\nHai Vinto!!!");
                            Console.ForegroundColor = ConsoleColor.White;
                            //Devo aggiungere i punti vittoria all'eroe: livello mostro * 10
                            eroe.Punti += mostroDaSconfiggere.Livello * 10;

                            //Controllo se è passato di livello
                            ControlloLivello(ref eroe);

                            //Controllo se ha vinto (è arrivato a 200 punti)
                            if (Vittoria(eroe))
                            {
                                //Se vince torna al menu principale salvando le statistiche
                                watch.Stop();
                                //Console.WriteLine("Watch: " + watch.ElapsedMilliseconds);
                                //TimeSpan time = new TimeSpan(watch.ElapsedMilliseconds * 10000);
                                //Console.WriteLine("Time : " + time);
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
                        Console.WriteLine("è il turno del mostro");
                        int danniMostro = GiocaMostro(mostroDaSconfiggere);
                        eroe.PuntiVita -= danniMostro;
                        Console.WriteLine("Il mostro attacca con successo");
                        //Se l'eroe non è morto mostro quanti punti vita gli restano
                        Console.ForegroundColor = ConsoleColor.Blue;
                        if(eroe.PuntiVita > 0)
                            Console.WriteLine($"L'eroe ha {eroe.PuntiVita} punti vita");
                        Console.ResetColor();
                        if (eroe.PuntiVita <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("L'eroe è morto\nHai perso...");
                            Console.ForegroundColor = ConsoleColor.White;
                            //L'eroe è morto, lo cancello
                            eroiService.EliminaEroe(eroe);
                            morto = true;
                        }
                    }
                } while (morto == false);

                //Se è morto l'eroe torna direttamente al menu, non può decidere di non salvare
                if (eroe.PuntiVita <= 0) break;
                //Se ha vinto può decidere se continuare o se tornare salvando o no
                Console.WriteLine("Il turno è finito, cosa vuoi fare?\n" +
                        "1 - Giocare ancora\n" +
                        "2 - Salva e torna al menu\n" +
                        "3 - Torna al menu senza salvare");
                int scelta = 0;
                bool corretta = false;
                do
                {
                    corretta = Int32.TryParse(Console.ReadLine(), out scelta);
                    if (corretta == false || scelta < 1 || scelta > 3)
                        Console.WriteLine("Comando non valido, riprova:");
                } while (corretta == false || scelta < 1 || scelta > 3);

                //Se decide di non salvare torna al menu principale
                if (scelta == 3)
                    break;
                else if (scelta == 2)
                    giocaAncora = false;
                else
                    giocaAncora = true;
                //Salva e torna al menu
                if (giocaAncora != true)
                {
                    //Fermo il timer
                    watch.Stop();
                    //Console.WriteLine("Watch: " + watch.ElapsedMilliseconds);
                    //TimeSpan time = new TimeSpan(watch.ElapsedMilliseconds * 10000);
                    //Console.WriteLine("Time : " + time);
                    //finita la partita salvo i progressi (solo se l'eroe non è morto)
                    eroe.TempoTotale += watch.ElapsedMilliseconds;
                    eroiService.SalvaProgressi(eroe);
                }
            } while (giocaAncora == true);
            //Al rientro nel menu si cancella la console
            Console.WriteLine("Premi un tasto per tornare al menu principale");
            Console.ReadKey();
        }

        /// <summary>
        /// Il mostro può solo attaccare
        /// </summary>
        /// <param name="mostro">è il mostro che sta giocando</param>
        /// <returns>ritorna i danni da infliggere all'eroe</returns>
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

        /// <summary>
        /// In un turno l'eroe può: combattere (infligge danni al mostro), fuggire (se ci riesce gli vengono
        /// sottratti dei punti, se non ci riesce il turno passa al mostro)
        /// </summary>
        /// <param name="eroe">L'eroe che sta giocando</param>
        /// <param name="mostro">Il mostro che sta giocando</param>
        /// <param name="danniEroe">è un bool che viene ritornato, true = fuga fallita, devo togliere dei 
        /// punti all'eroe, false l'eroe ha attaccato il mostro quindi devo togliergli dei punti vita,
        /// se la fuga fallisce non lo guardo</param>
        /// <returns>ritorna i punti da togliere all'eroe o al mostro o -1 se la fuga fallisce</returns>
        public static int GiocaEroe(Eroi eroe, Mostri mostro, out bool danniEroe)
        {
            //DIConfig
            var serviceProvider = DIConfig.Configurazione();
            ArmiService armiService = serviceProvider.GetService<ArmiService>();

            //Se fallisce la fuga non lo guardo
            danniEroe = false;

            int scelta = 0;
            bool corretto = false;
            Console.WriteLine("è il turno dell'eroe cosa vuoi fare?\n" +
                    "1 - Attacca!\n" +
                    "2 - Scappa");
            do
            {
                corretto = Int32.TryParse(Console.ReadLine(), out scelta);
                if (corretto == false || scelta != 1 && scelta != 2)
                {
                    Console.WriteLine("Comando non riconosciuto, riprova:");
                }
            } while (corretto == false || scelta != 1 && scelta != 2);

            //Attacco
            if (scelta == 1)
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

        /// <summary>
        /// Funzione che calcola se un numero random è pari o no per stabilire se l'eroe riesce a fuggire
        /// </summary>
        /// <returns>true se riesce a scappare, false altrimenti</returns>
        public static bool Fuga()
        {
            //Creo un numero random: se è pari allora la fuga ha successo
            Random random = new Random();
            int numRandom = random.Next(100);
            return numRandom % 2 == 0;
        }

        /// <summary>
        /// Questa funzione controlla se l'eroe è passato di livello dopo aver ucciso un mostro
        /// </summary>
        /// <param name="eroe">è passato per riferimento perchè se passa di livello aumenta il livello e i punti vita</param>
        public static void ControlloLivello(ref Eroi eroe)
        {
            //Se l'eroe è di livello 5 non può più passare di livello
            if (eroe.Livello == 5)
            {
                return;
            }

            //DIConfig
            var serviceProvider = DIConfig.Configurazione();
            LivelliService livelliService = serviceProvider.GetService<LivelliService>();

            //Prendo la lista dei livelli
            List<Livelli> listaLivelli = new List<Livelli>(livelliService.ListaLivelli());

            //Confronto i punti eroe con i punti massimi del suo livello, se li supera passa di livello
            foreach (var item in listaLivelli)
            {
                if (item.ID == eroe.Livello + 1) //Livello successivo
                {
                    if (item.PuntiPassaggio <= eroe.Punti)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Congratulazioni sei passato di livello!!");
                        eroe.Livello++;
                        Console.WriteLine("Sei salito al livello: " + eroe.Livello);
                        eroe.PuntiVita = item.PuntiVita;
                        Console.WriteLine("Ora i tuoi punti vita sono: " + eroe.PuntiVita);
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Questa funzione controlla se l'eroe ha vinto
        /// </summary>
        /// <param name="eroe">è l'eroe che sta giocando</param>
        /// <returns>true se ha vinto, false altrimenti</returns>
        public static bool Vittoria(Eroi eroe)
        {
            //Si vince se l'eroe raggiunge i 200 punti
            if (eroe.Punti >= 200)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("CONGRATULAZIONI, HAI VINTO IL GIOCO!!");
                Console.ResetColor();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Questa funzione stampa le statistiche degli eroi di un giocatore.
        /// Può essere chiamata solo se un giocatore ha degli eroi
        /// </summary>
        /// <param name="giocatore"> è il giocatore attuale </param>
        public static void StatisticheUtente(Giocatori giocatore)
        {
            //DIConfig
            var serviceProvider = DIConfig.Configurazione();
            EroiService eroiService = serviceProvider.GetService<EroiService>();

            //Visualizzo le statistiche degli eroi del giocatore
            //Non controllo se non ha eroi perchè può chiamare questa funzione solo se ha degli eroi
            List<Eroi> listaEroiDelGiocatore = new List<Eroi>(eroiService.EroiDiUnGiocatore(giocatore.ID));
            Console.WriteLine("Ecco le statistiche dei tuoi giocatori:");
            foreach (var item in listaEroiDelGiocatore)
            {
                TimeSpan time = new TimeSpan(item.TempoTotale * 10000);
                Console.WriteLine($"\t{item.Nome}\tLivello: {item.Livello}\tPunti accumulati: {item.Punti}\tTempo di gioco: {time}");
            }
            Console.WriteLine("Premi un tasto per tornare al menu principale");
            Console.ReadKey();
        }

        /// <summary>
        /// Questa funzione stampa le statitiche degli eroi di ogni giocatore
        /// </summary>
        public static void Statistiche()
        {
            //DIConfig
            var serviceProvider = DIConfig.Configurazione();
            GiocatoriService giocatoriService = serviceProvider.GetService<GiocatoriService>();
            EroiService eroiService = serviceProvider.GetService<EroiService>();

            //Creo la lista di tutti i giocatori
            List<Giocatori> listaGiocatori = new List<Giocatori>(giocatoriService.ListaGiocatori());

            //Per ogni giocatore stampo le statistiche tutti i suoi eroi
            foreach (var giocatore in listaGiocatori)
            {
                //Visualizzo le statistiche dei giocatori solo se hanno degli eroi
                List<Eroi> listaEroiDelGiocatore = new List<Eroi>(eroiService.EroiDiUnGiocatore(giocatore.ID));
                if (listaEroiDelGiocatore.Count != 0)
                {
                    Console.WriteLine($"Statistiche del giocatore: {giocatore.Nome}");
                    foreach (var eroe in listaEroiDelGiocatore)
                    {
                        TimeSpan time = new TimeSpan(eroe.TempoTotale * 10000);
                        Console.WriteLine($"\t{eroe.Nome}\tLivello: {eroe.Livello}\tPunti accumulati: {eroe.Punti}\tTempo di gioco: {time}");
                    }
                }
            }
            Console.WriteLine("Premi un tasto per tornare al menu principale");
            Console.ReadKey();
        }
    }
}
