namespace Projeto.Models
{
    public class Carro
    {
        public int Id { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Caracteristicas { get; set; }
        public float ValorDiaria { get; set; }

        public Carro() { }
    }
}
