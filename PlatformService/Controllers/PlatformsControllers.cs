using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Plaftorms");

            var platforms = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformById")] //Usado para o CreatedAtRoute
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            Console.WriteLine($"--> Getting Plaftorm: {id}");

            var platform = _repository.GetPlatformById(id);

            if (platform != null)
                return Ok(_mapper.Map<PlatformReadDto>(platform));

            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var plaformModel = _mapper.Map<Platform>(platformCreateDto);

            _repository.CreatePlatform(plaformModel);
            _repository.SaveChanges();

            return CreatedAtRoute(nameof(GetPlatformById), new { id = plaformModel.Id}, _mapper.Map<PlatformReadDto>(plaformModel));
        }
    }
}