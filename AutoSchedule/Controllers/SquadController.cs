using AutoSchedule.BLL.DTOs.Squads;
using AutoSchedule.BLL.Interfaces;
using AutoSchedule.Domain.Responce;
using Microsoft.AspNetCore.Mvc;

namespace AutoSchedule.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class SquadController(ILogger<SquadController> logger,
    ISquadService squadService) : Controller
{
    private readonly ILogger<SquadController> _logger = logger;
    private readonly ISquadService _squadService = squadService;

    /// <summary>
    /// Создает новый элемент.
    /// </summary>
    /// <param name="squadCreateDto">Данные для создания элемента</param>
    /// <returns>Созданный элемент</returns>
    /// <response code="201">Элемент успешно создан</response>
    /// <response code="400">Неверные данные для создания элемента</response>
    [HttpPost]
    public async Task<ActionResult<BaseResponse<SquadReadDto>>> Create([FromBody] SquadCreateDto squadCreateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _logger.LogInformation("Creating a new squad");
        var response = await _squadService.Create(squadCreateDto);
        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.Success => Ok(response),
            _ => BadRequest(response)
        };
    }
    
    /// <summary>
    /// Получает список всех элементов.
    /// </summary>
    /// <returns>Список элементов</returns>
    /// <response code="200">Возвращает список элементов</response>
    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<SquadReadDto>>>> GetAll()
    {
        _logger.LogInformation("Fetching all squads");
        var response = await _squadService.GetAll();
        return Ok(response);
    }

    /// <summary>
    /// Получает элемент по указанному ID.
    /// </summary>
    /// <param name="id">ID элемента</param>
    /// <returns>Элемент с соответствующим ID</returns>
    /// <response code="200">Элемент найден</response>
    /// <response code="404">Элемент с указанным ID не найден</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<BaseResponse<SquadReadDto>>> GetById(int id)
    {
        _logger.LogInformation($"Fetching squad with id: {id}");
        var response = await _squadService.GetById(id);
        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.Success => Ok(response),
            Domain.Enums.StatusCode.NotFound => NotFound(response),
            _ => BadRequest(response.Data)
        };
    }

    /// <summary>
    /// Извлекает данные о 'squads' постранично. Позволяет клиентам получать данные в удобной форме пагинации.
    /// </summary>
    /// <param name="pageNumber">Номер страницы для извлечения данных. Должен быть целым числом.</param>
    /// <param name="pageSize">Размер страницы, определяющий количество объектов 'SquadReadDto' на одной странице. По умолчанию равен 10. (опционально)</param>
    /// <returns>Возвращает пагинированный список объектов 'SquadReadDto' в ответе.</returns>
    /// <response code="200">Возвращает пагинированные данные 'SquadReadDto'.</response>
    /// <response code="400">Возвращается, если обнаружены ошибки валидации запроса.</response>
    [HttpGet("page/{pageNumber:int}")]
    public async Task<ActionResult<BaseResponse<IEnumerable<SquadReadDto>>>> GetByPage(int pageNumber, [FromQuery] int pageSize = 10)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _logger.LogInformation($"Fetching squads page {pageNumber} with page size {pageSize}");
        var response = await _squadService.GetByPage(pageNumber, pageSize);
        return Ok(response.Data);
    }

    /// <summary>
    /// Получает количество всех элементов.
    /// </summary>
    /// <returns>Количество элементов</returns>
    /// <response code="200">Возвращает количестов элементов</response>
    [HttpGet]
    public async Task<ActionResult<BaseResponse<int>>> GetCount()
    {
        _logger.LogInformation("Fetching squad count");
        var response = await _squadService.GetCount();
        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.Success => Ok(response),
            _ => BadRequest(response)
        };
    }

    /// <summary>
    /// Получает элемент по указанному Search Term.
    /// </summary>
    /// <param name="searchTerm">Term элемента для нахождения</param>
    /// <returns>Элемент с соответствующим Name</returns>
    /// <response code="200">Элемент найден</response>
    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<SquadReadDto>>>> Search(string searchTerm)
    {
        _logger.LogInformation($"Searching squads with term: {searchTerm}");
        var response = await _squadService.Search(searchTerm);
        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.Success => Ok(response),
            _ => BadRequest(response)
        };
    }

    /// <summary>
    /// Обновляет существующий элемент по ID.
    /// </summary>
    /// <param name="squadUpdateDto">Обновленные данные элемента</param>
    /// <returns>Обновленный элемент</returns>
    /// <response code="200">Элемент успешно обновлен</response>
    /// <response code="404">Элемент для обновления не найден</response>
    [HttpPut]
    public async Task<ActionResult<BaseResponse<SquadReadDto>>> Update([FromBody] SquadUpdateDto squadUpdateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _logger.LogInformation($"Updating squad with id: {squadUpdateDto.Id}");
        var response = await _squadService.Update(squadUpdateDto);
        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.Success => Ok(response.Data),
            Domain.Enums.StatusCode.NotFound => NotFound(response),
            _ => BadRequest(response)
        };
    }

    /// <summary>
    /// Удаляет элемент по указанному ID.
    /// </summary>
    /// <param name="id">ID элемента для удаления</param>
    /// <returns>Результат удаления</returns>
    /// <response code="200">Элемент успешно удален</response>
    /// <response code="404">Элемент для удаления не найден</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult<BaseResponse<bool>>> Delete(int id)
    {
        _logger.LogInformation($"Request received to delete squad with id: {id}");
        var response = await _squadService.Delete(id);
        return response.StatusCode switch
        {
            Domain.Enums.StatusCode.Success => Ok(response),
            Domain.Enums.StatusCode.NotFound => NotFound(response),
            _ => BadRequest(response)
        };
    }
}
