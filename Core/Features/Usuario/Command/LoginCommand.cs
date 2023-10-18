using Core.Domain.Exceptions;
using Core.Domain.Services;
using Core.Infraestructure.Persistance;
using MediatR;

namespace Core.Features.Usuario.Command;

public record LoginCommand : IRequest<LoginResponse>
{
    public string Correo { get; set; }
    public string Contrasena { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly FactoryContext _context;
    private readonly IAuthService _authService;
    
    public LoginCommandHandler(FactoryContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }
    
    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Correo) || string.IsNullOrEmpty(request.Contrasena))
            throw new BadRequestException("Correo y contraseña son obligatorios");
        
        var token = await _authService.AuthenticateAsync(request.Correo, request.Contrasena);

        if (token == null)
            throw new UnauthorizedAccessException("Correo o contraseña incorrectos.");
        
        return new LoginResponse
        {
            Token = token
        };
    }
}

public record LoginResponse
{
    public string Token { get; set; }
}
