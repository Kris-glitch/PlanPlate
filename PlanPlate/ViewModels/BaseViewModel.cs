using CommunityToolkit.Mvvm.ComponentModel;
using PlanPlate.Data;

namespace PlanPlate.ViewModels

{
    public partial class BaseViewModel(IUserRepository repository) : ObservableObject
    {
        protected readonly IUserRepository repository = repository;

        protected Action<string>? ShowError { get; set; }
        protected Action<string, Action<bool>>? ShowAlert { get; set; }

        protected void OnShowError(string errorMessage)
        {
            ShowError?.Invoke(errorMessage);
        }

        protected void OnShowAlert(string errorMessage, Action<bool> callback)
        {
            ShowAlert?.Invoke(errorMessage, callback);
        }

    }
}
