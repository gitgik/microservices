using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http;
public interface ICommandServiceHttpClient
{
    /// <summary>Sends platform to command service</summary>
    Task SendPlatform(PlatformReadDto platformServiceDto);
}