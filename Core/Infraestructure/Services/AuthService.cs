using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Features.Usuario.Command;

public class AuthService : IAuthService
{
    private readonly FactoryContext _context;
    private readonly IConfiguration _configuration;
    
    public AuthService(FactoryContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    
    public async Task<string> AuthenticateAsync(string email, string password)
    {
        if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            throw new ArgumentException("El usuario y la contraseña no pueden ser nulos o vacíos.");
        
        var user = await _context.Usuarios.Where(x => x.Correo == email && x.Contrasena == password).FirstOrDefaultAsync();
        
        if(user == null)
            throw new ArgumentException("El usuario o la contraseña son incorrectos.");
        
        //Genera un token JWT
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Usuario_Id.ToString()),
                new Claim(ClaimTypes.Email, user.Correo),
            }),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
}