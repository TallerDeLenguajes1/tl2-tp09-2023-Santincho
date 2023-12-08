using Microsoft.AspNetCore.Mvc;
using kanban.Models;
using kanban.Repository;

namespace kanban.Controllers;

[ApiController]
[Route("[controller]")]

public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository usuarioRepository;
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
    }

    [HttpPost]
    public ActionResult<Usuario> CreateUsuario(Usuario user)
    {
        usuarioRepository.CreateUser(user);
        return Created("", user);
    }

    [HttpGet]
    public ActionResult<List<Usuario>> GetAllUsuarios()
    {
        List<Usuario> users = usuarioRepository.UsersList();
        return Ok(users);
    }

    [HttpGet("{userId}", Name = "GetUsuarioById")]
    public ActionResult<List<Usuario>> GetUsuarioById(int userId)
    {
        var user = usuarioRepository.GetUserById(userId);
        if (userId == 0) return NotFound("");
        return Ok(user);
    }

    [HttpPut("{userId}/{NewUsername}")]
    public ActionResult<Usuario> UpdateNombreUsuario(int userId, string NewUsername)
    {
        if (userId == 0) return NotFound($"No existe el usuario");

        var user = usuarioRepository.GetUserById(userId);

        user.NombreDeUsuario = NewUsername;

        usuarioRepository.UpdateUser(user);

        return Ok(user);
    }

    [HttpDelete("DeleteUsuario/{userId}", Name = "DeleteUsuario")]
    public ActionResult<Usuario> DeleteUsuario(int userId)
    {
        if (userId == 0) return NotFound($"No existe el usuario");

        usuarioRepository.DeleteUserById(userId);

        return Ok();
    }
}