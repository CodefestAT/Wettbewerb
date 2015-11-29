using MVVMbasics.Attributes;
using MVVMbasics.Services;
using System;

namespace Core.Viewmodels
{
    public sealed class ResultViewmodel : NavigatorAwareBaseViewmodel
    {
        [MvvmBindable]
        public int Age { get; set; }

        [MvvmBindable]
        public string Label
        {
            get { return String.Format("Du bist {0} Jahre alt.", Age); }
        }

        public ResultViewmodel(INavigatorService navigatorService) : base(navigatorService) { }

        public override void OnNavigatedTo(ParameterList uriParameters, ParameterList parameters, ViewState viewState)
        {
            base.OnNavigatedTo(uriParameters, parameters, viewState);

            // Übergebenen Parameter zwischenspeichern...
            DateTimeOffset birthday;
            if (parameters.TryGet<DateTimeOffset>("Birthday", out birthday))
            {
                // ...und Differenz zwischen Geburtsjahr und aktuellem Jahr berechnen
                var today = DateTime.Today;
                var age = today.Year - birthday.Year;

                // Wenn der Geburtstag im aktuellen Jahr noch nicht vorbei ist, ist das berechnete Alter um ein Jahr zu hoch
                if (today.Month < birthday.Month || (today.Month == birthday.Month && today.Day < birthday.Day))
                    age--;

                // Speichern und anzeigen
                Age = age;
            }
        }
    }
}