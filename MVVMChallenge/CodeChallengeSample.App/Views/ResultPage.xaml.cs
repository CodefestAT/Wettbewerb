using Core.Viewmodels;
using MVVMbasics.Views;

namespace CodeChallengeSample.App.Views
{
    public sealed partial class ResultPage : BackButtonAwareBaseView, IBindableView<ResultViewmodel>
    {
        public ResultPage()
        {
            this.InitializeComponent();
        }

        public ResultViewmodel Vm { get; set; }
    }
}
