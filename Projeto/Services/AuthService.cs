using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Projeto.Data;
using Projeto.Models;

namespace Projeto.Services
{
    public class AuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> Authenticate(string username, string password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);
        if (user == null)
            return null;

        var hashedPassword = HashPassword(password, user.Salt);
        if (user.PasswordHash != hashedPassword)
            return null;

        return user;
    }

    private string HashPassword(string password, string salt)
    {
        // Implementar a lógica de hash da senha usando o salt
        using var hmac = new System.Security.Cryptography.HMACSHA512(Encoding.UTF8.GetBytes(salt));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hash);
    }
}

}