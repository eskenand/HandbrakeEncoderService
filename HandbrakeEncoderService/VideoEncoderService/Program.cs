using System.ServiceProcess;

namespace VideoEncoderService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] servicesToRun;
            servicesToRun = new ServiceBase[]
            {
                new VideoEncoderService()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}