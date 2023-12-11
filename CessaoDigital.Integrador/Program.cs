// Copyright (c) 2023 - BITFIN Tecnologia Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da Plataforma Cessão Digital.

using System.Text;

namespace CessaoDigital.Integrador
{
    internal class Program
    {
        const string Executavel = "CDI.exe";

        static async Task Main(string[] args)
        {
            if (Setup.Configurar())
            {
                if ((args?.Length ?? 0) > 0)
                {
                    if (args[0].ToLower() is "?" or "ajuda")
                    {
                        ExibirAjuda();
                    }
                    else
                    {
                        var tarefa = Tarefa.Criar(args[0]);

                        if (tarefa != null)
                        {
                            using (var log = new Logger())
                            {
                                if (tarefa != null)
                                {
                                    var sucesso = true;
                                    log.Log(tarefa.Nome, $"Início da Tarefa");

                                    try
                                    {
                                        await tarefa.Executar();
                                    }
                                    catch (Exception ex)
                                    {
                                        log.Log(tarefa.Nome, ex);
                                        sucesso = false;
                                    }
                                    finally
                                    {
                                        foreach (var l in tarefa.Logs)
                                            log.Log(l);

                                        log.Log(tarefa.Nome, sucesso ? "Executada com Sucesso" : "Falha na Execução", sucesso ? Logger.Informativo : Logger.Alerta);
                                        log.Log(tarefa.Nome, $"Fim da Tarefa");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"A tarefa \"{args[0]}\" não foi localizada.");
                            Console.ReadLine();
                        }
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"O diretório raiz \"{Configuracoes.Paths.Dados}\" não foi localizado.");
                Console.ReadLine();
            }
        }

        private static void ExibirAjuda() =>
            Console.WriteLine(
                new StringBuilder()
                    .AppendLine("CessaoDigital.Integrador")
                    .AppendLine("Envio de arquivos fiscais para Antecipação à Fornecedores.")
                    .AppendLine($"Copyright (C) {DateTime.Now.Year} - Cessão Digital(R)")
                    .AppendLine()
                    .AppendLine("IMPORTANTE: UTILIZANDO ESTE MECANISMO DE IMPORTAÇÃO DE DOCUMENTOS FISCAIS, FAZ")
                    .AppendLine("COM QUE OS MESMOS JÁ ESTEJAM AUTORIZADOS À SEREM ANTECIPADOS PELOS RESPECTIVOS")
                    .AppendLine("FORNECEDORES. CERTIFIQUE-SE DE SOMENTE DISPONIBILIZAR NESTE LOCAL OS DOCUMENTOS")
                    .AppendLine("FISCAIS QUE JÁ ESTEJAM DEVIDAMENTE APTOS PARA SEREM ANTECIPADOS.")
                    .AppendLine()
                    .AppendLine("Uso:")
                    .AppendLine("    Upload de Xml de Notas Fiscais:")
                    .AppendLine($"        {Executavel} UploadDeNFe")
                    .ToString());
    }
}