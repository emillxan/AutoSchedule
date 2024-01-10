using AutoMapper;
using AutoSchedule.BLL.DTOs.Squads;
using AutoSchedule.BLL.Interfaces;
using AutoSchedule.DAL.Interface;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;
using AutoSchedule.Domain.Responce;
using Microsoft.Extensions.Logging;

namespace AutoSchedule.BLL.Services;

public class SquadService : ISquadService
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Squad> _squadRepository;
    private readonly ILogger<SquadService> _logger;

    public SquadService(IMapper mapper,
        ILogger<SquadService> logger,
        IBaseRepository<Squad> squadRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _squadRepository = squadRepository;
    }


    public async Task<IBaseResponse<SquadReadDto>> Create(SquadCreateDto model)
    {
        try
        {
            var newSquad = _mapper.Map<Squad>(model);
            var createdSquad = await _squadRepository.Create(newSquad);

            if (createdSquad == null)
            {
                _logger.LogError("Squad creation failed.");
                return new BaseResponse<SquadReadDto>
                {
                    StatusCode = StatusCode.Error,
                    Description = "Creation failed",
                    Data = null
                };
            }

            var squadReadDto = _mapper.Map<SquadReadDto>(createdSquad);
            _logger.LogInformation($"Squad with id {squadReadDto.Id} created successfully.");
            return new BaseResponse<SquadReadDto>
            {
                StatusCode = StatusCode.Success,
                Description = "Squad created successfully",
                Data = squadReadDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while creating a squad: {ex.Message}");
            return new BaseResponse<SquadReadDto>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<SquadReadDto>>> GetAll()
    {
        try
        {
            var squads = _squadRepository.GetAll();
            var squadDtos = _mapper.Map<IEnumerable<SquadReadDto>>(squads);
            _logger.LogInformation("All squads retrieved successfully.");
            return new BaseResponse<IEnumerable<SquadReadDto>>
            {
                StatusCode = StatusCode.Success,
                Description = "Retrieved successfully",
                Data = squadDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving all squads: {ex.Message}");
            return new BaseResponse<IEnumerable<SquadReadDto>>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<SquadReadDto>> GetById(int id)
    {
        try
        {
            var squad = await _squadRepository.GetById(id);
            if (squad == null)
            {
                _logger.LogWarning($"Squad with id {id} not found.");
                return new BaseResponse<SquadReadDto>
                {
                    Description = "Squad not found",
                    StatusCode = StatusCode.NotFound
                };
            }

            var squadDto = _mapper.Map<SquadReadDto>(squad);
            _logger.LogInformation($"Squad with id {id} retrieved successfully.");
            return new BaseResponse<SquadReadDto>
            {
                Description = "Squad retrieved successfully",
                StatusCode = StatusCode.Success,
                Data = squadDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving squad with id {id}: {ex.Message}");
            return new BaseResponse<SquadReadDto>
            {
                Description = $"Error occurred: {ex.Message}",
                StatusCode = StatusCode.Error
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<SquadReadDto>>> GetByPage(int pageNumber, int pageSize)
    {
        try
        {
            var squads = await _squadRepository.GetByPage(pageNumber, pageSize);
            var squadDtos = _mapper.Map<IEnumerable<SquadReadDto>>(squads);

            _logger.LogInformation($"Page {pageNumber} retrieved successfully with {pageSize} squads.");
            return new BaseResponse<IEnumerable<SquadReadDto>>
            {
                StatusCode = StatusCode.Success,
                Description = "Page retrieved successfully",
                Data = squadDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving page {pageNumber} with {pageSize} squads: {ex.Message}");
            return new BaseResponse<IEnumerable<SquadReadDto>>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<IBaseResponse<int>> GetCount()
    {
        try
        {
            var count = await _squadRepository.GetCount();
            return new BaseResponse<int>
            {
                StatusCode = StatusCode.Success,
                Description = "Squad count retrieved successfully",
                Data = count
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving squad count");
            return new BaseResponse<int>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
                Data = 0
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<SquadReadDto>>> Search(string searchTerm)
    {
        try
        {
            var squads = _squadRepository.Search(searchTerm);
            var squadDtos = _mapper.Map<IEnumerable<SquadReadDto>>(squads);
            return new BaseResponse<IEnumerable<SquadReadDto>>
            {
                StatusCode = StatusCode.Success,
                Description = "Squads retrieved successfully",
                Data = squadDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching squads");
            return new BaseResponse<IEnumerable<SquadReadDto>>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
                Data = Enumerable.Empty<SquadReadDto>().AsEnumerable()
            };
        }
    }

    public async Task<IBaseResponse<SquadReadDto>> Update(SquadUpdateDto model)
    {
        try
        {
            var existingSquad = await _squadRepository.GetById(model.Id);
            if (existingSquad == null)
            {
                _logger.LogWarning($"Squad with id {model.Id} not found for update.");
                return new BaseResponse<SquadReadDto>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Squad not found",
                };
            }

            _mapper.Map(model, existingSquad);
            await _squadRepository.Update(existingSquad);

            var updatedSquadDto = _mapper.Map<SquadReadDto>(existingSquad);
            _logger.LogInformation($"Squad with id {model.Id} updated successfully.");
            return new BaseResponse<SquadReadDto>
            {
                StatusCode = StatusCode.Success,
                Description = "Squad updated successfully",
                Data = updatedSquadDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating squad with id {model.Id}: {ex.Message}");
            return new BaseResponse<SquadReadDto>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<bool>> Delete(int id)
    {
        try
        {
            var squadToDelete = await _squadRepository.GetById(id);
            if (squadToDelete == null)
            {
                _logger.LogWarning($"Squad with id {id} not found for deletion.");
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Squad not found",
                    Data = false
                };
            }

            await _squadRepository.Delete(squadToDelete);
            _logger.LogInformation($"Squad with id {id} deleted successfully.");
            return new BaseResponse<bool>
            {
                StatusCode = StatusCode.Success,
                Description = "Squad deleted successfully",
                Data = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting squad with id: {id}");
            return new BaseResponse<bool>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
                Data = false
            };
        }
    }
}
