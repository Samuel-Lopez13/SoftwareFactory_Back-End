using System.Net;
using System.Net.Mail;
using System.Text;
using Core.Domain.Exceptions;
using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Usuario.Queries;

public record ObtenerUsuarioExistente : IRequest<TokenCorreoResponse>
{
    public string Correo { get; set; }
}

public class TokenCorreo : IRequestHandler<ObtenerUsuarioExistente, TokenCorreoResponse>
{
    private readonly FactoryContext _context;

    public TokenCorreo(FactoryContext context)
    {
        _context = context;
    }
    
    public async Task<TokenCorreoResponse> Handle(ObtenerUsuarioExistente request, CancellationToken cancellationToken)
    {
        var correo = await _context.Usuarios.Where(u => u.Correo == request.Correo).FirstOrDefaultAsync();

        if (correo == null)
        {
            throw new BadRequestException($"No se encontro el correo {correo.Correo}");
        }
        
        string tokenGenerado = GenerarToken();

        EnviarCorreoVerificacion(request.Correo, tokenGenerado);

        var enviarTokenResponse = new TokenCorreoResponse
        {
            Token = tokenGenerado
        };

        return enviarTokenResponse;
    }
    
    private string GenerarToken()
    {
        var random = new Random();
        const string caracteresPermitidos = "0123456789";
        var token = new StringBuilder();

        for (int i = 0; i < 6; i++)
        {
            token.Append(caracteresPermitidos[random.Next(caracteresPermitidos.Length)]);
        }

        return token.ToString();
    }

    private async Task EnviarCorreoVerificacion(string correoElectronico, string token)
    {
        var email = new MailMessage
        {
            From = new MailAddress("n3xustech@gmail.com"),
            Subject = "Recuperación de contraseña",
            IsBodyHtml = true,
            Body = @"
                <html>
                <head>
                    <style>
                        body {
                            text-align: center;
                            font-family: Arial, sans-serif;
                        }
                        .container {
                            background-color: #f2f2f2;
                            padding: 20px;
                            width: 100%;
                        }
                        .caja{
                            width: 100%;
                            display: flex;
                            justify-content: center;    
                        }
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='caja'>
                            <h1>Tu token de verifacion es: </h1>
                        </div>
                        <p style='font-size: 30px; font-weight: bold; text-align: center; letter-spacing: 30px; color: blue'>" + token + @"</p>
                    </div>
                </body>
                </html>"
        };
        email.To.Add(correoElectronico);

        var smtpClient = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            Credentials = new NetworkCredential("n3xustech@gmail.com", "qciykuwcfcsccdoa"),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(email);
    }
}


public record TokenCorreoResponse
{
    public string Token { get; set; }
}