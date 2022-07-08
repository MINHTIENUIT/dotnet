using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo respository, IMapper mapper)
        {
            _repository = respository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> Get platforms");
            var platforms = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformByID")]
        public ActionResult<PlatformReadDto> GetPlatformByID(int id)
        {
            var platform = _repository.GetPlatformById(id);
            if (platform != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platform));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platform)
        {
            var plat = _mapper.Map<Platform>(platform);
            _repository.createPlatform(plat);
            _repository.SaveChanges();
            var platformReadDto = _mapper.Map<PlatformReadDto>(plat);
            return CreatedAtRoute(nameof(GetPlatformByID), new { Id = platformReadDto.Id }, platformReadDto);
        }
    }
}

