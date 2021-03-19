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
        /// <summary>
        /// Questo è il menu che viene mostrato all'utente
        /// </summary>
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

            //Confronto se il giocatore esiste o no
            List<Giocatori> listaGiocatori = new List<Giocatori>();
            listaGiocatori = (List<Giocatori>)giocatoriService.ListaGiocatori();
            bool giocatoreTrovato = false;
            Giocatori giocatoreAttuale = new Giocatori();
            foreach (var item in listaGiocatori)
            {
                //Se esiste lo prendo
                if (nomeGiocatore == item.Nome)
                {
                    giocatoreTrovato = true;
                    giocatoreAttuale = item;
                    break;
                }
            }
            //Se non esiste lo creo
            if (giocatoreTrovato == false)
            {
                giocatoreAttuale.Nome = nomeGiocatore;
                //Tutti i giocatori inseriti da console sono utenti, non admin
                giocatoreAttuale.Ruolo_ID = 1;
                //Se l'utente inserisce un nome già presente nel db il giocatore non viene inserito,
                //il nome deve essere univoco
                bool univoco = false;
                do
                {
                    univoco = giocatoriService.CreaGiocatore(giocatoreAttuale);
                    //Se il nome è univoco esco
                    if (univoco == true)
                    {
                        break;
                    }
                    //Se non è univoco lo deve inserire di nuovo
                    else
                    {
                        Console.WriteLine("Inserisci un nuovo nome: ");
                        giocatoreAttuale.Nome = Console.ReadLine();
                    }
                } while (true);
                List <Giocatori> listaGiocatori2 = new List<Giocatori>();
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

                //Se ha già degli eroi può: continuare con un eroe, eliminarne uno, crearlo, vedere le statistiche e uscire
                if (listaEroi.Count > 0)
                {
                    Console.WriteLine("Menu principale:");
                    Console.WriteLine("1 - Crea un nuovo Eroe");
                    Console.WriteLine("2 - Continua con un Eroe");
                    Console.WriteLine("3 - Elimina un Eroe");
                    Console.WriteLine("4 - Guarda le statistiche dei tuoi eroi");
                    Console.WriteLine("5 - Salva ed esci");

                    //Se è admin può creare e cancellare i mostri e vedere le statistiche dei giocatori
                    if(giocatoreAttuale.Ruolo_ID == 2)
                    {
                        Console.WriteLine("6 - Crea un nuovo mostro");
                        Console.WriteLine("7 - Elimina un mostro");
                        Console.WriteLine("8 - Guarda le statistiche globali");
                    }
                    int index = 0;
                    bool corretto = false;
                    //Se non è admin può scegliere solo 5 opzioni
                    if (giocatoreAttuale.Ruolo_ID == 1)
                    {
                        do
                        {
                            corretto = Int32.TryParse(Console.ReadLine(), out index);
                            if (corretto == false || index < 1 || index > 6)
                            {
                                Console.WriteLine("Comando non riconosciuto, riprova");
                            }
                        } while (corretto == false || index < 1 || index > 6);
                    }
                    //Se è admin può scegliere tra 8 opzioni
                    else
                    {
                        do
                        {
                            corretto = Int32.TryParse(Console.ReadLine(), out index);
                            if (corretto == false || index < 1 || index > 8)
                            {
                                Console.WriteLine("Comando non riconosciuto, riprova");
                            }
                        } while (corretto == false || index < 1 || index > 8);
                    }

                    switch (index)
                    {
                        case 1:
                            //Crea un nuovo eroe
                            Eroi eroeCreato = eroiService.CreateEroe(giocatoreAttuale);
                            Console.WriteLine("Premi un tasto per iniziare il turno");
                            Console.ReadKey();
                            Partita.Turno(eroeCreato);
                            break;
                        case 2:
                            //Può vedere solo i suoi eroi
                            foreach (var item in listaEroi)
                            {
                                Console.WriteLine($"{item.ID} - {item.Nome} livello: {item.Livello}");
                            }
                            //Deve scegliere l'eroe
                            bool eroeGiusto = false;
                            Console.WriteLine("Inserisci il numero dell'eroe:");
                            do
                            {
                                bool isCorretto = Int32.TryParse(Console.ReadLine(), out int nEroe);
                                if (isCorretto == true)
                                {
                                    foreach (var item in listaEroi)
                                    {
                                        if (nEroe == item.ID)
                                        {
                                            eroeGiusto = true;
                                            //Quando seleziona l'eroe inizia il turno
                                            Console.WriteLine("Premi un tasto per iniziare il turno");
                                            Console.ReadKey();
                                            Partita.Turno(item);
                                            break;
                                        }
                                    }
                                }
                                if (isCorretto == false || eroeGiusto == false)
                                    Console.WriteLine("Eroe non corretto, riprova:");
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
                            Console.WriteLine("Inserisci il numero dell'eroe da eliminare:");
                            do
                            {
                                bool eroeCorretto = Int32.TryParse(Console.ReadLine(), out int nEroe);
                                if (eroeCorretto == true)
                                {
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
                                }
                                if (eroeCorretto == false || eroeDaEliminare == false)
                                    Console.WriteLine("Eroe non valido, riprova:");
                            } while (eroeDaEliminare == false);
                            Console.WriteLine("Premi un tasto per continuare");
                            Console.ReadKey();
                            break;
                        case 4:
                            //Mostra le statistiche del giocatore attuale
                            Partita.StatisticheUtente(giocatoreAttuale);
                            break;
                        case 5:
                            //Esce
                            //Non faccio nessuna operazione perchè tutte quelle precedenti sono già state salvate
                            Console.WriteLine("Arrivederci!");
                            esci = true;
                            break;
                        //Questi può sceglierli solo un admin
                        case 6:
                            //Crea un nuovo mostro
                            mostroService.CreaMostro();
                            Console.WriteLine("Premi un tasto per continuare");
                            Console.ReadKey();
                            break;
                        case 7:
                            //Elimina un mostro
                            mostroService.EliminaMostro();
                            Console.WriteLine("Premi un tasto per continuare");
                            Console.ReadKey();
                            break;
                        case 8:
                            //Mostra statistiche dei giocatori
                            Partita.Statistiche();
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
                    bool corretto = false;
                    //Se non è admin può scegliere solo 2 opzioni
                    if (giocatoreAttuale.Ruolo_ID == 1)
                    {
                        do
                        {
                            corretto = Int32.TryParse(Console.ReadLine(), out index);
                            if (corretto == false || index != 1 && index != 2)
                            {
                                Console.WriteLine("Comando non riconosciuto, riprova");
                            }
                        } while (corretto == false || index != 1 && index != 2);
                    }
                    //Se è admin può scegliere tra 5 opzioni
                    else
                    {
                        do
                        {
                            corretto = Int32.TryParse(Console.ReadLine(), out index);
                            if (corretto == false || index < 1 || index > 5)
                            {
                                Console.WriteLine("Comando non riconosciuto, riprova");
                            }
                        } while (corretto == false || index < 1 || index > 5);
                    }

                    switch (index)
                    {
                        case 1:
                            //Crea un nuovo eroe
                            Eroi eroeCreato = eroiService.CreateEroe(giocatoreAttuale);
                            Console.WriteLine("Premi un tasto per iniziare il turno");
                            Console.ReadKey();
                            Partita.Turno(eroeCreato);
                            break;
                        case 2:
                            //Esce
                            //Non faccio nessuna operazione perchè tutte quelle precedenti sono già state salvate
                            Console.WriteLine("Arrivederci!");
                            esci = true;
                            break;
                        //Questi può sceglierli solo un admin
                        case 3:
                            //Crea un nuovo mostro
                            mostroService.CreaMostro();
                            Console.WriteLine("Premi un tasto per continuare");
                            Console.ReadKey();
                            break;
                        case 4:
                            //Elimina un mostro
                            mostroService.EliminaMostro();
                            Console.WriteLine("Premi un tasto per continuare");
                            Console.ReadKey();
                            break;
                        case 5:
                            //Mostra statistiche dei giocatori
                            Partita.Statistiche();
                            break;
                        default:
                            Console.WriteLine("Case default");
                            break;
                    }
                }
            } while (esci != true);
        }
    }
}
