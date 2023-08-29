// Copyright (c) 2023 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da Plataforma Cessão Digital.

using Microsoft.Extensions.Configuration;

namespace CessaoDigital.Integrador
{
    internal class Configuracoes
    {
        private static readonly IConfiguration config;

        static Configuracoes()
        {
            config =
                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }

        internal class Paths
        {
            internal static string Dados => config["Paths:Dados"];

            internal static string Remessas => Path.Combine(Dados, "Remessas");

            internal static string RemessasEnviadas => Path.Combine(Dados, "Remessas", "Enviadas");

            internal static string Logs => Path.Combine(Dados, "Logs");
        }
    }
}