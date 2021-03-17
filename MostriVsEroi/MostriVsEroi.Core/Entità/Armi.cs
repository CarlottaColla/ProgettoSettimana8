using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi.Core.Entità
{
    public class Armi
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int PuntiDanno { get; set; }
        //Da controllare, è una classe
        public int ClassiID { get; set; }
    }
}
