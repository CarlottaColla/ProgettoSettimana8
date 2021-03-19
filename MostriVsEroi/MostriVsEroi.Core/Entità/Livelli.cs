using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.Core.Entità
{
    public class Livelli
    {
        public int ID { get; set; }

        //Questo campo mi serve per sapere il numero del livello
        //Attualmente è uguale all'id ma potrebbe cambiare
        public int Numero { get; set; }
        public int PuntiVita { get; set; }
        public int PuntiPassaggio { get; set; }
    }
}
