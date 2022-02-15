using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepo
    {
        bool SaveChanges();

        /// <summary>Get all platforms</summmary>
        IEnumerable<Platform> GetAllPlatforms();
        Platform GetAllPlatformsById(int id);

        void CreatePlatform(Platform platform);

    }
}
