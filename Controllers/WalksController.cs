using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalksAPI.Models.Domain;
using WalksAPI.Models.DTOs.WalksDTO;
using WalksAPI.RepositoryUntionOfWork;
using WalksAPI.RepositoryUntionOfWork.IRepositoryInterfaces;
using WalksAPI.Validation;

namespace WalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public WalksController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery
            , [FromQuery] string? sortBy , [FromQuery] bool IsAscending = true,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize=20)
        {
            var walks = await unitOfWork.WalksRepository
                .GetAllAsync(filterOn,filterQuery,sortBy,IsAscending,pageNumber,pageSize);
            // Convert to DTOs if necessary
            var walkDtos = mapper.Map<List<WalksDTO>>(walks);
            return Ok(walkDtos);
        }
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walk = await unitOfWork.WalksRepository.GetByIdAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            // Convert to DTO
            var walkDto = mapper.Map<WalksDTO>(walk);
            return Ok(walkDto);
        }
        [HttpPost("Create")]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] WalksDTO walkDto)
        {
            if (walkDto == null)
            {
                return BadRequest("Walk data is null");
            }
            // Convert DTO to domain model
            var walkModel = mapper.Map<Walks>(walkDto);
            await unitOfWork.WalksRepository.CreateAsync(walkModel);
            await unitOfWork.SaveAsync();
            // Convert domain model to DTO
            var createdWalkDto = mapper.Map<WalksDTO>(walkModel);
            return CreatedAtAction(nameof(GetById), new { id = createdWalkDto.Id }, createdWalkDto);
        }
        [HttpPut("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update(Guid id, [FromBody] WalksDTO walkDto)
        {
            if (walkDto == null || walkDto.Id != id)
            {
                return BadRequest("Walk data is invalid");
            }
            var existingWalk = await unitOfWork.WalksRepository.GetByIdAsync(id);
            if (existingWalk == null)
            {
                return NotFound();
            }
            // Convert DTO to domain model
            var walkModel = mapper.Map<Walks>(walkDto);
            await unitOfWork.WalksRepository.UpdateAsync(id, walkModel);
            await unitOfWork.SaveAsync();
            // Convert domain model to DTO
            var updatedWalkDto = mapper.Map<WalksDTO>(walkModel);
            return Ok(updatedWalkDto);
        }
        [HttpDelete("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existingWalk = await unitOfWork.WalksRepository.GetByIdAsync(id);
            if (existingWalk == null)
            {
                return NotFound();
            }
            await unitOfWork.WalksRepository.DeleteAsync(id);
            await unitOfWork.SaveAsync();
            return Ok(new { Message = "Walk deleted successfully" });
        }
    }
}
