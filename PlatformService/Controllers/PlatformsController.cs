using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;


namespace PlatformService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : ControllerBase
{
    private readonly IPlatformRepo _repository;

    private readonly ICommandServiceHttpClient _commandServiceClient;
    public readonly IMapper _mapper;

    public PlatformsController(IPlatformRepo repository, ICommandServiceHttpClient commandServiceClient, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        _commandServiceClient = commandServiceClient;
    }

    [HttpPost]
    public async Task<ActionResult<PlatformCreateDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
    {
        var platformModel = _mapper.Map<Platform>(platformCreateDto);
        _repository.CreatePlatform(platformModel);
        _repository.SaveChanges();

        var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

        try
        {
            await _commandServiceClient.SendPlatform(platformReadDto);
        }
        catch (Exception e)
        {
            Console.WriteLine("--> Sending Platform To Command Service Failed!", e);
        }

        return CreatedAtRoute(
            nameof(GetPlatformById), new { Id = platformReadDto.Id}, platformReadDto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        Console.WriteLine("--> Getting Platforms");

        var platforms = _repository.GetAllPlatforms();

        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<PlatformReadDto> GetPlatformById(int id)
    {
        Console.WriteLine($"--> Getting platform by Id {id}");

        var platform = _repository.GetPlatformById(id);

        if (platform != null)
        {
            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }

        return NotFound();

    }
}