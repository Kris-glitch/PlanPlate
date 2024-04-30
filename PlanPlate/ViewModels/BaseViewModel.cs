using CommunityToolkit.Mvvm.ComponentModel;
using PlanPlate.Data;

namespace PlanPlate.ViewModels

{
    public partial class BaseViewModel(IUserRepository repository) : ObservableObject
    {
        protected readonly IUserRepository repository = repository;

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
