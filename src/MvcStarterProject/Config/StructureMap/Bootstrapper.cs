using System.Web;
using HttpInterfaces;
using StructureMap;

namespace MvcStarterProject.Config.StructureMap
{
    public static class Bootstrapper
    {
        public static bool AlreadyStarted { get; private set; }
        public static void Bootstrap()
        {
            if (AlreadyStarted)
                return;

            AlreadyStarted = true;

            new StructureMapConfiguration().Configure();
        }
    }
    public class MvcConfigration
    {
    }
}