using Microsoft.AspNetCore.Mvc;
using Projeto.Data;
using Projeto.Models;
using System.Linq;

namespace Projeto.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Reservar()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized("Usuario não estar logado.");
            }
            var usuarioId = int.Parse(userIdString);

            // Recuperar a lista de carros disponíveis
            var carrosDisponiveis = _context.Carros.ToList();
            return View(carrosDisponiveis); // Passar a lista para a view
        }

        [HttpPost]
        public IActionResult ConfirmarReserva(int carroId, DateTime dataInicio, DateTime dataFim)
        {
            var usuarioId = int.Parse(HttpContext.Session.GetString("UserId"));

            // Verificar se a data de início é anterior à data atual
            if (dataInicio < DateTime.Today)
            {
                TempData["MensagemErro"] = "A data de início não pode ser anterior à data de hoje.";
                return RedirectToAction("Reservar"); // Redireciona para a página de reserva
            }

            // Verificar se a data de fim é anterior à data de início
            if (dataFim < dataInicio)
            {
                TempData["MensagemErro"] = "A data de fim não pode ser anterior à data de início.";
                return RedirectToAction("Reservar"); 
            }

            // Verificar se o usuário já possui uma reserva ativa
            var reservaExistente = _context.Reservas
                .FirstOrDefault(r => r.UserId == usuarioId && r.Status == true);

            if (reservaExistente != null)
            {
                TempData["MensagemErro"] = "Você já possui uma reserva ativa.";
                return RedirectToAction("Reservar");
            }

            // Verificar pendências financeiras
            if (UsuarioTemPendenciasFinanceiras())
            {
                TempData["MensagemErro"] = "Você possui pendências financeiras.";
                return RedirectToAction("Reservar");
            }

            // Verificar se o carro está disponível nas datas selecionadas
            var reservaConflitante = _context.Reservas
                .FirstOrDefault(r => r.CarroId == carroId
                                     && r.Status == true
                                     && (dataInicio < r.DataFim && dataFim > r.DataInicio));

            if (reservaConflitante != null)
            {
                var dataDisponivel = reservaConflitante.DataFim.AddDays(1);
                TempData["MensagemErro"] = $"Carro indisponível. Ele estará disponível em: {dataDisponivel:dd/MM/yyyy}";
                return RedirectToAction("Reservar");
            }

            var carro = _context.Carros.FirstOrDefault(c => c.Id == carroId);
            if (carro != null)
            {
                var reserva = new Reserva
                {
                    CarroId = carro.Id,
                    UserId = usuarioId,
                    DataInicio = dataInicio,
                    DataFim = dataFim,
                    Valor = carro.ValorDiaria * (dataFim - dataInicio).Days,
                    Status = true
                };

                _context.Reservas.Add(reserva);
                _context.SaveChanges();

                return View("Resumo", reserva);
            }

            TempData["MensagemErro"] = "Erro ao realizar a reserva. Por favor, tente novamente.";
            return RedirectToAction("Reservar");
        }

        private bool UsuarioTemPendenciasFinanceiras()
        {
            return false;
        }
    }
}
