namespace VolskiNet.ConsoleOperations
{
    using System;

    public class ApplicationSettings
    {
        public ApplicationSettings(string appName, BasicControls basicControls)
        {
            if (string.IsNullOrEmpty(appName))
            {
                throw new ArgumentNullException("appName");
            }
            if (basicControls == null)
            {
                throw new ArgumentNullException("basicControls");
            }

            BasicControls = basicControls;
            AppName = appName;
        }

        public string AppName { get; }

        public BasicControls BasicControls { get; }
    }
}