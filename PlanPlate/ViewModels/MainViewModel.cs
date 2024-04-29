
using CommunityToolkit.Mvvm.ComponentModel;
using PlanPlate.Data.Model;

namespace PlanPlate.ViewModels
{
    [QueryProperty(nameof(MyUser), nameof(MyUser))]
    public partial class MainViewModel : BaseViewModel
    {
        [ObservableProperty]
        MyUser? myUser;
    }
}
