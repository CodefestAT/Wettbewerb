using Core.Viewmodels;
using MVVMbasics.Views;

namespace CodeChallengeSample.App.Views
{
    public sealed partial class InputPage : BackButtonAwareBaseView, IBindableView<InputViewmodel>
    {
        public InputPage()
        {
            this.InitializeComponent();
        }

        public InputViewmodel Vm { get; set; }
    }
}
