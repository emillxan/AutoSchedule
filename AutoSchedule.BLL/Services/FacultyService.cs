using AutoMapper;
using AutoSchedule.BLL.DTOs.Faculties;
using AutoSchedule.BLL.DTOs.Squads;
using AutoSchedule.BLL.Interfaces;
using AutoSchedule.DAL.Interface;
using AutoSchedule.DAL.Repositories;
using AutoSchedule.Domain.Entities;
using AutoSchedule.Domain.Enums;
using AutoSchedule.Domain.Responce;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSchedule.BLL.Services;

public class FacultyService : IFacultyService
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Faculty> _facultyRepository;
    private readonly ILogger<FacultyService> _logger;

    public FacultyService(IMapper mapper, 
        ILogger<FacultyService> logger, 
        IBaseRepository<Faculty> facultyRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _facultyRepository = facultyRepository;
    }


    public async Task<IBaseResponse<FacultyReadDto>> Create(FacultyCreateDto model)
    {
        try
        {
            var newFaculty = _mapper.Map<Faculty>(model);
            var createdFaculty = await _facultyRepository.Create(newFaculty);

            if (createdFaculty == null)
            {
                _logger.LogError("Faculty creation failed.");
                return new BaseResponse<FacultyReadDto>
                {
                    StatusCode = StatusCode.Error,
                    Description = "Creation failed",
                };
            }

            var facultyReadDto = _mapper.Map<FacultyReadDto>(createdFaculty);
            _logger.LogInformation($"Faculty with id {facultyReadDto.Id} created successfully.");
            return new BaseResponse<FacultyReadDto>
            {
                StatusCode = StatusCode.Success,
                Description = "Faculty created successfully",
                Data = facultyReadDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating faculty");
            return new BaseResponse<FacultyReadDto>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<FacultyReadDto>>> GetAll()
    {
        try
        {
            var faculties = _facultyRepository.GetAll();
            var facultyDtos = _mapper.Map<IEnumerable<FacultyReadDto>>(faculties);
            _logger.LogInformation("All faculties retrieved successfully.");
            return new BaseResponse<IEnumerable<FacultyReadDto>>
            {
                StatusCode = StatusCode.Success,
                Description = "Faculties retrieved successfully",
                Data = facultyDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving faculties");
            return new BaseResponse<IEnumerable<FacultyReadDto>>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<FacultyReadDto>> GetById(int id)
    {
        try
        {
            var faculty = await _facultyRepository.GetById(id);
            if (faculty == null)
            {
                _logger.LogWarning($"Faculty with id {id} not found.");
                return new BaseResponse<FacultyReadDto>
                {
                    Description = "Faculty not found",
                    StatusCode = StatusCode.NotFound
                };
            }

            var facultyDto = _mapper.Map<FacultyReadDto>(faculty);
            _logger.LogInformation($"Faculty with id {id} retrieved successfully.");
            return new BaseResponse<FacultyReadDto>
            {
                Description = "Faculty retrieved successfully",
                StatusCode = StatusCode.Success,
                Data = facultyDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving faculty with id: {id}");
            return new BaseResponse<FacultyReadDto>
            {
                Description = $"Error occurred: {ex.Message}",
                StatusCode = StatusCode.Error
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<FacultyReadDto>>> GetByPage(int pageNumber, int pageSize)
    {
        try
        {
            var faculties = await _facultyRepository.GetByPage(pageNumber, pageSize);
            var facultyDtos = _mapper.Map<IEnumerable<FacultyReadDto>>(faculties);

            _logger.LogInformation($"Page {pageNumber} retrieved successfully with {pageSize} faculties.");
            return new BaseResponse<IEnumerable<FacultyReadDto>>
            {
                StatusCode = StatusCode.Success,
                Description = "Page retrieved successfully",
                Data = facultyDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving page {pageNumber} with {pageSize} faculties: {ex.Message}");
            return new BaseResponse<IEnumerable<FacultyReadDto>>
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
            var count = await _facultyRepository.GetCount();
            return new BaseResponse<int>
            {
                StatusCode = StatusCode.Success,
                Description = "Faculty count retrieved successfully",
                Data = count
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving faculty count");
            return new BaseResponse<int>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
                Data = 0 
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<FacultyReadDto>>> Search(string searchTerm)
    {
        try
        {
            var faculties = _facultyRepository.Search(searchTerm);
            var facultyDtos = _mapper.Map<IEnumerable<FacultyReadDto>>(faculties);
            return new BaseResponse<IEnumerable<FacultyReadDto>>
            {
                StatusCode = StatusCode.Success,
                Description = "Faculties retrieved successfully",
                Data = facultyDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching faculties");
            return new BaseResponse<IEnumerable<FacultyReadDto>>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
                Data = Enumerable.Empty<FacultyReadDto>().AsEnumerable() 
            };
        }
    }

    public async Task<IBaseResponse<FacultyReadDto>> Update(FacultyUpdateDto model)
    {
        try
        {
            var existingFaculty = await _facultyRepository.GetById(model.Id);
            if (existingFaculty == null)
            {
                _logger.LogWarning($"Faculty with id {model.Id} not found for update.");
                return new BaseResponse<FacultyReadDto>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Faculty not found",
                };
            }

            _mapper.Map(model, existingFaculty);
            await _facultyRepository.Update(existingFaculty);

            var updatedFacultyDto = _mapper.Map<FacultyReadDto>(existingFaculty);
            _logger.LogInformation($"Faculty with id {model.Id} updated successfully.");
            return new BaseResponse<FacultyReadDto>
            {
                StatusCode = StatusCode.Success,
                Description = "Faculty updated successfully",
                Data = updatedFacultyDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating faculty with id {model.Id}: {ex.Message}");
            return new BaseResponse<FacultyReadDto>
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
            var facultyToDelete = await _facultyRepository.GetById(id);
            if (facultyToDelete == null)
            {
                _logger.LogWarning($"Faculty with id {id} not found for deletion.");
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Faculty not found",
                    Data = false
                };
            }

            await _facultyRepository.Delete(facultyToDelete);
            _logger.LogInformation($"Faculty with id {id} deleted successfully.");
            return new BaseResponse<bool>
            {
                StatusCode = StatusCode.Success,
                Description = "Faculty deleted successfully",
                Data = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting faculty with id: {id}");
            return new BaseResponse<bool>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
                Data = false
            };
        }
    }
}
