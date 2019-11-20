namespace APILivraria.Models
{
    public class Livro
    {
        private string _codigoBarras;
        public string CodigoBarras
        {
            get => _codigoBarras;
            set => _codigoBarras = value?.Trim().ToUpper();
        }

        private string _nome;
        public string Titulo
        {
            get => _nome;
            set => _nome = value?.Trim();
        }

        public double Preco { get; set; }

        public string Editora { get; set; }

        public string Autor { get; set; }
    }
}