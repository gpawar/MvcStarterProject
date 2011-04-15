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
}