using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;

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
		public ActionResult<IEnumerable<PlatformReadDto>> getPlatforms()
        {
			Console.WriteLine("--> Get platforms");
			var platforms = _repository.GetAllPlatforms();
			return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }
	}
}

