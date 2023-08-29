// Copyright (c) 2023 - BITFIN Software Ltda. Todos os Direitos Reservados.
// Código exclusivo para consumo dos serviços (API's) da Plataforma Cessão Digital.

using CessaoDigital.Proxy;
using CessaoDigital.Proxy.Configuracoes;

namespace CessaoDigital.Integrador
{
    internal abstract class Tarefa
    {
        private readonly static AppSettingsJson config = new();
        protected readonly static ProxyDoServico proxy = new(config.Conexoes.First());
        private readonly IList<Logger.Registro> logs = new List<Logger.Registro>();

        protected Tarefa(string nome) =>
            this.Nome = nome;

        internal string Nome { get; }

        internal abstract Task Executar(CancellationToken cancellationToken = default);

        protected void Log(string mensagem, string severidade = Logger.Informativo) =>
            this.logs.Add(new(DateTime.Now, this.Nome, severidade, mensagem));

        protected void Log(Exception erro) =>
            this.logs.Add(new(DateTime.Now, this.Nome, Logger.Falha, erro.ToString().SemQuebraDeLinha()));

        internal static Tarefa? Criar(string descricao) =>
            descricao?.ToLower() switch
            {
                "uploaddexml" => new UploadDeNFe(),
                _ => null
            };

        public override string ToString() => this.Nome;

        internal IEnumerable<Logger.Registro> Logs => this.logs;
    }
}