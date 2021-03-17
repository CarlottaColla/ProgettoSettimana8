using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.Core.Entità
{
    public abstract class Personaggi
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int PuntiVita { get; set; }
        public int Livello { get; set; }
        public int Classe { get; set; }
        public int Arma { get; set; }

    }
}
