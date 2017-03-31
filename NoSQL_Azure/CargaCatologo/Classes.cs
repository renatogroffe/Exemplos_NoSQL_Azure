namespace CargaCatologo
{
    public class Fornecedor
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
    }

    public class Produto
    {
        public string id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public double Preco { get; set; }
        public Fornecedor DadosFornecedor { get; set; }
    }

    public class Servico
    {
        public string id { get; set; }
        public string Nome { get; set; }
        public double ValorHora { get; set; }
    }
}