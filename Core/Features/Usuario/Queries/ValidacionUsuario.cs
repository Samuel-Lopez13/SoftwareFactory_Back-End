using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Usuario.Queries;

public record ValidacionUsuario : IRequest<ValidacionUsuarioResponse>
{
    public string Correo { get; set; }
    public string Contrasena { get; set; }
}

public class ValidacionHandler : IRequestHandler<ValidacionUsuario, ValidacionUsuarioResponse>
{
    private readonly FactoryContext _context;
    
    public ValidacionHandler(FactoryContext context)
    {
        _context = context;
    }
    
    public async Task<ValidacionUsuarioResponse> Handle(ValidacionUsuario request, CancellationToken cancellationToken)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Correo == request.Correo && x.Contrasena == request.Contrasena);

        bool valido = usuario != null ? true : false;

        return new ValidacionUsuarioResponse(){ entro = valido};
        
        throw new NotImplementedException();
    }
}

public record ValidacionUsuarioResponse
{
    public bool entro { get; set; }
}