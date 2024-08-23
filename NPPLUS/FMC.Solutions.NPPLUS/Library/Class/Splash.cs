using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.Class
{
    public class Splash
    {
        public static void Visible(SplashScreenManager splash,bool visible)
        {
            try
            {
                if (visible)
                {
                    if (!splash.IsSplashFormVisible)
                        splash.ShowWaitForm();
                }
                else
                {
                    if (splash.IsSplashFormVisible)
                        splash.CloseWaitForm();
                }
            }
            catch { }
        }
    }
}
