using Core.Features.Usuario.Command;
using Core.Features.Usuario.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class UsuarioController
{
    private readonly IMediator _mediator;

    public UsuarioController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Lista a todos los usuarios
    /// </summary>
    /// <remarks>
    /// Devuelve una lista de todos los usuarios registrados en el sistema
    /// </remarks>
    /// <returns>
    /// <code>200</code>
    /// Es un valor nuevo
    /// </returns>
    
    [AllowAnonymous]
    [HttpGet("Usuarios")]
    public async Task<List<ListaUsuarioResponse>> ObtenerUsuarios()
    {
        return await _mediator.Send(new ListaUsuarioCommand());
    }
    
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<LoginResponse> Login([FromBody] LoginCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [AllowAnonymous]
    [HttpPost("Indentificacion")]
    public async Task<TokenCorreoResponse> GenerarTokenCorreo([FromBody] ObtenerUsuarioExistente command)
    {
        return await _mediator.Send(command);
    }
    
    [AllowAnonymous]
    [HttpPost("Validacion")]
    public async Task<ValidacionUsuarioResponse> Validacion([FromBody] ValidacionUsuario command)
    {
        return await _mediator.Send(command);
    }
}