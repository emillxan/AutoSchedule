using AutoMapper;
using AutoSchedule.BLL.DTOs.Departments;
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

public class DepartmentService : IDepartmentService
{
    private readonly IMapper _mapper;
    private readonly IBaseRepository<Department> _departmentRepository;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(IMapper mapper, 
        ILogger<DepartmentService> logger, 
        IBaseRepository<Department> departmentRepository)
    {
        _mapper = mapper;
        _logger = logger;
        _departmentRepository = departmentRepository;
    }


    public async Task<IBaseResponse<DepartmentReadDto>> Create(DepartmentCreateDto model)
    {
        try
        {
            var newDepartment = _mapper.Map<Department>(model);
            var createdDepartment = await _departmentRepository.Create(newDepartment);

            if (createdDepartment == null)
            {
                _logger.LogError("Department creation failed.");
                return new BaseResponse<DepartmentReadDto>
                {
                    StatusCode = StatusCode.Error,
                    Description = "Creation failed",
                };
            }

            var departmentReadDto = _mapper.Map<DepartmentReadDto>(createdDepartment);
            _logger.LogInformation($"Department with id {departmentReadDto.Id} created successfully.");
            return new BaseResponse<DepartmentReadDto>
            {
                StatusCode = StatusCode.Success,
                Description = "Department created successfully",
                Data = departmentReadDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating department");
            return new BaseResponse<DepartmentReadDto>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<DepartmentReadDto>>> GetAll()
    {
        try
        {
            var departments = _departmentRepository.GetAll();
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentReadDto>>(departments);
            _logger.LogInformation("All departments retrieved successfully.");
            return new BaseResponse<IEnumerable<DepartmentReadDto>>
            {
                StatusCode = StatusCode.Success,
                Description = "Departments retrieved successfully",
                Data = departmentDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving departments");
            return new BaseResponse<IEnumerable<DepartmentReadDto>>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
            };
        }
    }

    public async Task<IBaseResponse<DepartmentReadDto>> GetById(int id)
    {
        try
        {
            var department = await _departmentRepository.GetById(id);
            if (department == null)
            {
                _logger.LogWarning($"Department with id {id} not found.");
                return new BaseResponse<DepartmentReadDto>
                {
                    Description = "Department not found",
                    StatusCode = StatusCode.NotFound
                };
            }

            var departmentDto = _mapper.Map<DepartmentReadDto>(department);
            _logger.LogInformation($"Department with id {id} retrieved successfully.");
            return new BaseResponse<DepartmentReadDto>
            {
                Description = "Department retrieved successfully",
                StatusCode = StatusCode.Success,
                Data = departmentDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving department with id: {id}");
            return new BaseResponse<DepartmentReadDto>
            {
                Description = $"Error occurred: {ex.Message}",
                StatusCode = StatusCode.Error
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<DepartmentReadDto>>> GetByPage(int pageNumber, int pageSize)
    {
        try
        {
            var departments = await _departmentRepository.GetByPage(pageNumber, pageSize);
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentReadDto>>(departments);

            _logger.LogInformation($"Page {pageNumber} retrieved successfully with {pageSize} departments.");
            return new BaseResponse<IEnumerable<DepartmentReadDto>>
            {
                StatusCode = StatusCode.Success,
                Description = "Page retrieved successfully",
                Data = departmentDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving page {pageNumber} with {pageSize} departments: {ex.Message}");
            return new BaseResponse<IEnumerable<DepartmentReadDto>>
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
            var count = await _departmentRepository.GetCount(); 
            return new BaseResponse<int>
            {
                StatusCode = StatusCode.Success,
                Description = "Department count retrieved successfully",
                Data = count
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving department count");
            return new BaseResponse<int>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
                Data = 0
            };
        }
    }

    public async Task<IBaseResponse<IEnumerable<DepartmentReadDto>>> Search(string searchTerm)
    {
        try
        {
            var departments = _departmentRepository.Search(searchTerm);
            var departmentDtos = _mapper.Map<IEnumerable<DepartmentReadDto>>(departments);
            return new BaseResponse<IEnumerable<DepartmentReadDto>>
            {
                StatusCode = StatusCode.Success,
                Description = "Departments retrieved successfully",
                Data = departmentDtos
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while searching departments");
            return new BaseResponse<IEnumerable<DepartmentReadDto>>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
                Data = Enumerable.Empty<DepartmentReadDto>().AsEnumerable()
            };
        }
    }

    public async Task<IBaseResponse<DepartmentReadDto>> Update(DepartmentUpdateDto model)
    {
        try
        {
            var existingDepartment = await _departmentRepository.GetById(model.Id);
            if (existingDepartment == null)
            {
                _logger.LogWarning($"Department with id {model.Id} not found for update.");
                return new BaseResponse<DepartmentReadDto>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Department not found",
                };
            }

            _mapper.Map(model, existingDepartment);
            await _departmentRepository.Update(existingDepartment);

            var updatedDepartmentDto = _mapper.Map<DepartmentReadDto>(existingDepartment);
            _logger.LogInformation($"Department with id {model.Id} updated successfully.");
            return new BaseResponse<DepartmentReadDto>
            {
                StatusCode = StatusCode.Success,
                Description = "Department updated successfully",
                Data = updatedDepartmentDto
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating department with id {model.Id}: {ex.Message}");
            return new BaseResponse<DepartmentReadDto>
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
            var departmentToDelete = await _departmentRepository.GetById(id);
            if (departmentToDelete == null)
            {
                _logger.LogWarning($"Department with id {id} not found for deletion.");
                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.NotFound,
                    Description = "Department not found",
                    Data = false
                };
            }

            await _departmentRepository.Delete(departmentToDelete);
            _logger.LogInformation($"Department with id {id} deleted successfully.");
            return new BaseResponse<bool>
            {
                StatusCode = StatusCode.Success,
                Description = "Department deleted successfully",
                Data = true
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting department with id: {id}");
            return new BaseResponse<bool>
            {
                StatusCode = StatusCode.Error,
                Description = $"Error occurred: {ex.Message}",
                Data = false
            };
        }
    }
}
