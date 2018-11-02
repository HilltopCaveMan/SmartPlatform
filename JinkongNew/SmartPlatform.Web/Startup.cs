using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SuperGPS.Startup))]
namespace SuperGPS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            if (!GlobalVariable.p_bLinkCenterON)
            {
                Transfers.ReadConfig();
                Transfers.LinkCenter(GlobalVariable.p_strWGCenterIP, GlobalVariable.p_intWGCenterPort);
            }
        }
    }
}
