using Microsoft.AspNetCore.Mvc;
using kanban.Models;
using kanban.Repository;

namespace kanban.Controllers;

[ApiController]
[Route("[controller]")]

public class TareaController : ControllerBase
{
    private readonly ITareaRepository tareaRepository;
    private readonly ILogger<TareaController> _logger;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
    }

    [HttpPost]
    public ActionResult<Usuario> CreateTarea(int boardId, Tarea task)
    {
        tareaRepository.CreateTask(boardId, task);
        return Created("", task);
    }

    [HttpGet("{taskId}", Name = "GetTareaById")]
    public ActionResult<List<Tablero>> GetTareaById(int taskId)
    {
        var task = tareaRepository.GetTaskById(taskId);
        if (taskId == 0) return NotFound("");
        return Ok(task);
    }

    [HttpGet("{userId}", Name = "GetTareasByUsuario")]
    public ActionResult<List<Tablero>> GetTareasByUsuario(int userId)
    {
        var tasks = tareaRepository.GetTasksByUser(userId);
        if (userId == 0) return NotFound("");
        return Ok(tasks);
    }

    [HttpGet("{boardId}", Name = "GetTareasByTablero")]
    public ActionResult<List<Tablero>> GetTareasByTablero(int boardId)
    {
        var tasks = tareaRepository.GetTasksByBoard(boardId);
        if (boardId == 0) return NotFound("");
        return Ok(tasks);
    }
    
    [HttpDelete("DeleteTarea/{taskId}", Name = "DeleteTarea")]
    public ActionResult<Usuario> DeleteTarea(int taskId)
    {
        if (taskId == 0) return NotFound($"No existe el usuario");

        tareaRepository.DeleteTaskById(taskId);
        
        return Ok();
    }

    [HttpPut("{idUser}/{idTask}")]
    public ActionResult<Tarea> UpdateNombreTarea(int idUser, int idTask)
    {
        if (idTask == 0) return NotFound();

        tareaRepository.AssingUserToTask(idUser, idTask);

        return Ok();
    }
}