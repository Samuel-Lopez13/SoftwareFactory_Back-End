using Core.Infraestructure.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Features.Usuario.Queries;

public record ListaUsuarioCommand : IRequest<List<ListaUsuarioResponse>>;

public class ListaUsuarioCommandHandler : IRequestHandler<ListaUsuarioCommand, List<ListaUsuarioResponse>>
{
    private readonly FactoryContext _context;

    public ListaUsuarioCommandHandler(FactoryContext context)
    {
        _context = context;
    }

    public async Task<List<ListaUsuarioResponse>> Handle(ListaUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuarios = await _context.Usuarios.ToListAsync();

        var respuesta = usuarios.Select(usuario => new ListaUsuarioResponse
        {
            Usuario_Id = usuario.Usuario_Id,
            Nombre = usuario.Nombre,
            Correo = usuario.Correo,
            Descripcion = usuario.Descripcion,
            Contrasena = usuario.Contrasena,
            FotoPerfil = usuario.FotoPerfil
        }).ToList();
        
        return respuesta;
    }
}

public record ListaUsuarioResponse
{
    public int Usuario_Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Correo { get; set; } = null!;
    public string? Descripcion { get; set; }
    public string Contrasena { get; set; } = null!;
    public string? FotoPerfil { get; set; }
}
