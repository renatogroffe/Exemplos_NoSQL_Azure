using System.Dynamic;
using Microsoft.Azure.Documents.Client;

namespace CargaCatologo
{
    public static class Carga
    {
        public static void InserirDadosProdutos(DocumentClient client)
        {
            Produto prod001 = new Produto();
            prod001.id = "PROD001";
            prod001.Nome = "Detergente";
            prod001.Tipo = "Limpeza";
            prod001.Preco = 5.75;
            prod001.DadosFornecedor = new Fornecedor();
            prod001.DadosFornecedor.Codigo = "FORN001";
            prod001.DadosFornecedor.Nome = "EMPRESA XYZ";

            var prod002 = new
            {
                id = "PROD002",
                Nome = "Martelo",
                Tipo = "Ferramenta",
                Preco = 50.70
            };

            client.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(
                    "MVPCommunityConnection", "Catalogo"), prod001).Wait();

            client.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(
                    "MVPCommunityConnection", "Catalogo"), prod002).Wait();
        }

        public static void InserirDadosServicos(DocumentClient client)
        {
            Servico serv001 = new Servico();
            serv001.id = "SERV001";
            serv001.Nome = "LIMPEZA PREDIAL";
            serv001.ValorHora = 150.00;

            dynamic serv002 = new ExpandoObject();
            serv002.id = "SERV002";
            serv002.Nome = "GUARDA PATRIMONIAL";

            client.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(
                    "MVPCommunityConnection", "Catalogo"), serv001).Wait();

            client.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(
                    "MVPCommunityConnection", "Catalogo"), serv002).Wait();
        }
    }
}