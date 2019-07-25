using System.Linq;
using System.Diagnostics;

namespace BaseSolution.Helpers
{
    internal class HookHelper
    {
        public static void KillBrowsers()
        {
            foreach (Process P in Process.GetProcessesByName("chromedriver"))
                P.Kill();
            foreach (Process P in Process.GetProcessesByName("chrome"))
                P.Kill();
        }

    }
}
