using TimeCapture.Forms.Shared;

namespace TimeCapture.rev2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            bool IsBusinessModel = Convert.ToBoolean(Convert.ToInt32(_configuration.isBusiness));
            
            if (IsBusinessModel)
                Application.Run(new Login());
            else
                Application.Run(new TimeCapture());

        }
    }
}