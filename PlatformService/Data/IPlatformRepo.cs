using PlatformService.Models;

namespace PlatformService.Data;
public interface IPlatformRepo 
{
    bool SaveChanges();

    /// <summary>Get all platforms</summmary>
    IEnumerable<Platform> GetAllPlatforms();
    Platform GetPlatformById(int id);

    void CreatePlatform(Platform platform);

}
