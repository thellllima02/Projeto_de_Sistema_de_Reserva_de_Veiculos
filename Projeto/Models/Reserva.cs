namespace Projeto.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int CarroId { get; set; }
        public int UserId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public float Valor { get; set; }
        public bool Status { get; set; }

        public Carro Carro { get; set; }

        public Reserva() { }

        // Método para calcular o valor total da reserva
        public float CalcularValor(float valorDiaria, DateTime dataInicio, DateTime dataFim)
        {
            int dias = (dataFim - dataInicio).Days;
            return dias * valorDiaria;
        }
    }
}
