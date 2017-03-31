using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace APICotacoes
{
    public class CotacaoEntity : TableEntity
    {
        public CotacaoEntity(
            string siglaMoeda, string dataCotacao, double valorComercial)
        {
            PartitionKey = siglaMoeda;
            RowKey = dataCotacao;

            SiglaMoeda = siglaMoeda;
            DataCotacao = Convert.ToDateTime(dataCotacao);
            ValorComercial = valorComercial;
        }

        public CotacaoEntity() { }

        public string SiglaMoeda { get; set; }
        public DateTime DataCotacao { get; set; }
        public double ValorComercial { get; set; }
    }

    public class CotacaoDolarEntity : CotacaoEntity
    {
        public CotacaoDolarEntity(
            string siglaMoeda, string dataCotacao,
            double valorComercial, double valorTurismo) :
            base(siglaMoeda, dataCotacao, valorComercial)
        {
            ValorTurismo = valorTurismo;
        }

        public CotacaoDolarEntity() { }

        public double ValorTurismo { get; set; }
    }
}