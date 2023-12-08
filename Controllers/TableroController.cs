using Microsoft.AspNetCore.Mvc;
using kanban.Models;
using kanban.Repository;

namespace kanban.Controllers;

[ApiController]
[Route("[controller]")]

public class TableroController : ControllerBase
{
    private readonly ITableroRepository tableroRepository;
    private readonly ILogger<TableroController> _logger;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
    }

    [HttpPost]
    public ActionResult<Usuario> CreateTablero(Tablero tablero)
    {
        tableroRepository.CreateBoard(tablero);
        return Created("", tablero);
    }

    [HttpGet]
    public ActionResult<List<Tablero>> GetAllTableros()
    {
        List<Tablero> boards = tableroRepository.ListBoards();
        return Ok(boards);
    }

    [HttpGet("{boardId}", Name = "GetTableroById")]
    public ActionResult<List<Tablero>> GetTableroById(int boardId)
    {
        var board = tableroRepository.GetTableroById(boardId);
        if (boardId == 0) return NotFound("");
        return Ok(board);
    }

    [HttpPut("{boardId}/{NewBoard}")]
    public ActionResult<Usuario> UpdateTablero(int boardId, Tablero NewBoard)
    {
        if (boardId == 0) return NotFound($"No existe el tablero");

        tableroRepository.ModifyBoardById(boardId, NewBoard);

        return Ok();
    }

    [HttpGet("{userId}", Name = "GetTablerosByUser")]
    public ActionResult<List<Tablero>> GetTablerosByUser(int userId)
    {
        var boards = tableroRepository.GetBoardsByUser(userId);
        if (userId == 0) return NotFound("");
        return Ok(boards);
    }

    [HttpDelete("DeleteTablero/{boardId}", Name = "DeleteTablero")]
    public ActionResult<Tablero> DeleteTablero(int boardId)
    {
        if (boardId == 0) return NotFound($"No existe el usuario");

        tableroRepository.DeleteBoardById(boardId);
        
        return Ok();
    }
}