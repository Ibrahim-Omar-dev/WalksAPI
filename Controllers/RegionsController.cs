using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalksAPI.Models.Domain;
using WalksAPI.Models.DTOs.Regions;
using WalksAPI.RepositoryUntionOfWork;
using WalksAPI.Validation;

namespace WalksAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RegionsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await unitOfWork.RegionRepository.GetAllAsync();
            //convert to DTOs if necessary
            var regionDto=mapper.Map<List<RegionDTO>>(regions);

            return Ok(regionDto);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var region = await unitOfWork.RegionRepository.GetByIdAsync(id);
            if(region == null)
            {
                return NotFound();
            }
            //convert to DTO
            var regionDTO = mapper.Map<RegionDTO>(region);
            return Ok(regionDTO);
        }
        [HttpPost]
        [Route("Create")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //convert DTO to domain model
            var regionModel=mapper.Map<Region>(addRegionRequestDTO);

             await unitOfWork.RegionRepository.CreateAsync(regionModel);
            await unitOfWork.SaveAsync();

            //convert domain model to DTO
            var regionDTO = mapper.Map<RegionDTO>(regionModel);
            return CreatedAtAction(nameof(GetById), new {id= regionDTO.Id }, regionDTO );
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] EditRegionREquestDTO editRegionREquestDTO)
        {
    
            var regionModel = await unitOfWork.RegionRepository.GetByIdAsync(id);
            if(regionModel == null)
            {
                return NotFound();
            }
            //convert DTO to domain model
            var region = mapper.Map<Region>(editRegionREquestDTO);
            await unitOfWork.RegionRepository.UpdateAsync(id, region);
            await unitOfWork.SaveAsync();
            //convert domain model to DTO
            var regionDTO = mapper.Map<RegionDTO>(region);
            return Ok(regionDTO);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var region = await unitOfWork.RegionRepository.GetByIdAsync(id);
            if(region == null)
            {
                return NotFound();
            }
            await unitOfWork.RegionRepository.DeleteAsync(id);
            await unitOfWork.SaveAsync();
            return Ok(new { Message = "Region deleted successfully." });
        }
    }
}
