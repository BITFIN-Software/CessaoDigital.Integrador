# CessaoDigital.Integrador
###### Envio de arquivos fiscais para Antecipação à Fornecedores.
Ferramenta para automatizar o envio de documentos fiscais para a Plataforma Cessão Digital, disponibilizando os mesmos para os respectivos fornecedores realizarem operações de Antecipação de Recebíveis. Essa ferramenta abstrai toda a comunicação com as API's da Plataforma, apenas ficando sob responsabilidade da aplicação (ou de um usuário) depositar os arquivos em um determinado diretório, onde ela empacotará os mesmos e, na sequência, enviará para a Plataforma processar e disponibilizar aos fornecedores.

> IMPORTANTE: Utilizando este mecanismo de importação de documentos fiscais, faz com que os mesmos já estejam autorizados à serem antecipados pelos respectivos fornecedores. Certifique-se de somente disponibilizar neste local os documentos fiscais que já estejam devidamente aptos para serem antecipados.

Antes de utilizar a ferramenta, é necessário é informar as configurações através do arquivo `appsettings.json`, que consiste nos parâmetros informados no momento da contratação do serviço e o diretório raiz onde os arquivos serão gerados ou armazenados. Esse arquivo deve ser devidamente configurado antes de fazer uso desta ferramenta.

```json
{
  "CessaoDigital.Proxy": {
    "ConexaoPadrao": "Local",
    "Conexoes": [
      {
        "Ambiente": "Local",
        "Versao": "v1",
        "CodigoDoContratante": "c7efea7b-5040-4956-9d11-755dbeaddf5",
        "ChaveDeIntegracao": "CHAVE-FORNECIDA-PELA-PLATAFORMA",
        "Timeout": "00:00:20"
      }
    ]
  },
  "Paths": {
    "Dados": "C:\\Temp\\Teste"
  }
}
```
A partir deste diretório raiz, serão criados subdiretórios para organização dos arquivos, onde os arquivos gerados por outras aplicações para envio, deverão ser colocados no diretório `Remessas` e, quando ele foi enviado ao serviço, a ferramenta moverá o arquivo para o subdiretório `Enviadas`. Por fim, todas as execuções são registradas e armazenadas em arquivos de logs diários no subdiretório `Logs`.
```
* C:\Temp\Teste
|---- Remessas
|-------- Enviadas
|---- Logs
```

> O diretório *Logs* armazenará os logs de execução, gerando um arquivo por dia, no formato **AAMMDD.txt**.

Para executar a ferramenta, você poderá utilizar um dos parâmetros que se pode ver a seguir:

```
CessaoDigital.Integrador
Envio de arquivos fiscais para Antecipaçao à Fornecedores.
Copyright (C) 2023 - Cessao Digital(R)

Uso:
    Upload de Xml de Notas Fiscais:
        CDI.exe UploadDeNFe
```

### Execução Periódica
Alternativamente poderá agendar este executável para que ele seja executado periodicamente. Caso precise disso, pode recorrer à recursos nativos do próprio sistema operacional (como as Tarefas do Windows), para que se configure o período e a quantidade de repetições necessárias.

> #### API's
>
> Para um maior controle sobre o envio dos arquivos e uma gestão total sobre os documentos fiscais, fornecedores e operações, opte por implementar as API's que a Plataforma oferece. Abaixo estão os links que podem ser úteis:
> - Documentação de Integração: https://cessaodigital.com.br/integracao
> - Proxy .NET: https://github.com/BITFIN-Software/CessaoDigital.Proxy

---

> #### CONTATOS
>
> - Site: <https://cessaodigital.com.br>
> - E-mail Técnico: <dev@bitfin.com.br>
> - E-mail Comercial: <comercial@bitfin.com.br>
> - Telefone (+WhatsApp): +55 (19) 9.9901-1065
