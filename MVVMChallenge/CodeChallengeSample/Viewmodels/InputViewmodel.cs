using MVVMbasics.Commands;
using MVVMbasics.Attributes;
using MVVMbasics.Services;
using System;

namespace Core.Viewmodels
{
    public sealed class InputViewmodel : NavigatorAwareBaseViewmodel
    {
        private readonly INavigatorService _navigatorService;

        [MvvmBindable]
        public DateTimeOffset Birthday { get; set; }

        public BaseCommand ConfirmCommand { get; set; }

        public InputViewmodel(INavigatorService navigatorService) : base(navigatorService)
        {
            _navigatorService = navigatorService;

            ConfirmCommand = CreateCommand(Confirm);
        }

        private void Confirm()
        {
            // Wechsle zur zweiten Seite und übergebe den Geburtstags-Wert
            _navigatorService.NavigateTo<ResultViewmodel>("Birthday", Birthday);
        }
    }
}