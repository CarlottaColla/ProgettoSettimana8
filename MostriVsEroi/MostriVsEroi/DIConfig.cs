using Microsoft.Extensions.DependencyInjection;
using MostriVsEroi.ADO_Repository;
using MostriVsEroi.Core.Interfacce;
using MostriVsEroi.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace MostriVsEroi
{
    class DIConfig
    {
        public static ServiceProvider Configurazione()
        {
            return new ServiceCollection()
                //Aggiungo i miei servizi
                .AddTransient<EroiService>()
                .AddTransient<ClassiService>()
                .AddTransient<GiocatoriService>()
                //Aggiungo un "servizio" che mappa l'astrazione con l'implementazione concreta
                .AddTransient<IEroiRepository, ADOEroiRepos>()
                .AddTransient<IClassiRepository, ADOClassiRepos>()
                .AddTransient<IGiocatoriRepository, ADOGiocatoriRepos>()
                //Provider generico di servizi
                .BuildServiceProvider();
        }
    }
}
