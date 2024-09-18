using AutoMapper;
using EmployeeAPI.Application.DTOs;
using EmployeeAPI.Domain.Entities;
using EmployeeAPI.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{

    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
    {
        var employees = await _repository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(employees));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<EmployeeDto>(employee));
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeDto>> CreateEmployee(EmployeeDto employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);
        await _repository.AddAsync(employee);
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.Id }, _mapper.Map<EmployeeDto>(employee));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto employeeDto)
    {
        if (id != employeeDto.Id)
        {
            return BadRequest();
        }

        var employee = _mapper.Map<Employee>(employeeDto);
        await _repository.UpdateAsync(employee);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet(nameof(GetError))]
    public IActionResult GetError()
    {
        throw new NotImplementedException();
    }
}
