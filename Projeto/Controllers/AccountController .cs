using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto.Data;
using Projeto.Models;
using Projeto.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;



namespace Projeto.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _authService;
        private readonly ApplicationDbContext _context;

        public AccountController(AuthService authService, ApplicationDbContext context)
        {
            _authService = authService;
            _context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _authService.Authenticate(username, password);
            if (user == null)
            {
                ModelState.AddModelError("", "Tentativa de login inválida.");
                
                return View();
            }
            // autenticar o usuário na sessão
            HttpContext.Session.SetString("UserId", user.Id.ToString());

            // Redireciona para a página de reserva
            return RedirectToAction("Reservar","Reserva");
        }
        public async Task<IActionResult> Logout()
{
    await HttpContext.SignOutAsync("CookieAuth"); // Encerra o login baseado em cookies
    HttpContext.Session.Clear(); // Limpa a sessão
    return RedirectToAction("Login", "Account"); // Redireciona para a página de login
}


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "As senhas tem que ser iguais.");
                    return View(model);
                }

                // Verificar se o nome de usuário já está em uso
                var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "O nome de usuário já está em uso.");
                    return View(model);
                }

                // Gerar um salt e hash para a senha
                var salt = GenerateSalt();
                var hashedPassword = HashPassword(model.Password, salt);

                // Criar o novo usuário
                var user = new User
                {
                    Username = model.Username,
                    PasswordHash = hashedPassword,
                    Salt = salt
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            return View(model);
        }

        private string GenerateSalt()
        {
            using var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var bytes = new byte[16];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        private string HashPassword(string password, string salt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512(Encoding.UTF8.GetBytes(salt));
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        


    }


}