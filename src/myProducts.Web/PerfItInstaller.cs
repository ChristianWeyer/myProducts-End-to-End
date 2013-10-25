using PerfIt;
using System.Collections;
using System.ComponentModel;

namespace MyProducts.Web
{
    [RunInstaller(true)]
    public partial class PerfItInstaller : System.Configuration.Install.Installer
    {
        public PerfItInstaller()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            PerfItRuntime.Install();
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            PerfItRuntime.Uninstall();
        }
    }
}
