// Copyright (c) 2023 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da Plataforma Cessão Digital.

namespace CessaoDigital.Integrador
{
    internal class UploadDeNFe : Tarefa
    {
        internal UploadDeNFe()
            : base("Upload de NFe") { }

        internal override async Task Executar(CancellationToken cancellationToken = default)
        {
            var notasFiscais = new List<(string, byte[])>();

            foreach (var arquivo in Directory.GetFiles(Configuracoes.Paths.Remessas, "*.xml"))
                notasFiscais.Add((Path.GetFileName(arquivo), await File.ReadAllBytesAsync(arquivo, cancellationToken)));

            if (notasFiscais.Count > 0)
            {
                this.Log($"Qtde. de Arquivos: {notasFiscais.Count}");
                this.Log($"Upload Id: {await proxy.Ancora.EnvioDeNFe(Compactador.Compactar(notasFiscais), cancellationToken)}");

                foreach (var nf in notasFiscais)
                    File.Move(Path.Combine(Configuracoes.Paths.Remessas, nf.Item1), Utilitarios.GerarNomeDeArquivo(Configuracoes.Paths.RemessasEnviadas, nf.Item1));
            }
        }
    }
}