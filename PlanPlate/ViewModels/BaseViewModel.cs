using CommunityToolkit.Mvvm.ComponentModel;

namespace PlanPlate.ViewModels

{
    public partial class BaseViewModel : ObservableObject
    {
        private Action<string>? ShowAlert { get; set; }
        private Action<string>? ShowError { get; set; }

        protected void DisplayError(string errorMessage)
        {
            ShowError?.Invoke(errorMessage);
        }
        protected void DisplayAlert(string errorMessage)
        {
            ShowAlert?.Invoke(errorMessage);
        }

        public void SetShowAlertAction(Action<string> showAlertAction)
        {
            ShowAlert = showAlertAction;
        }
        public void SetShowErrorAction(Action<string> showErrorAction)
        {
            ShowError = showErrorAction;
        }
    }
}
