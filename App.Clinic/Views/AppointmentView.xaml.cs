using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
namespace App.Clinic.Views;

[QueryProperty(nameof(AppointmentId), "apptId")]
public partial class AppointmentView : ContentPage
{
    public int AppointmentId { get; set; }
	public AppointmentView()
	{
		InitializeComponent();
        BindingContext = new AppointmentViewModel(); 
	}
    private void ReturnClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Appointments");
    }

    private void ConfirmClicked(object sender, EventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.AddOrUpdate();
        Shell.Current.GoToAsync("//Appointments");
    }

    private void StartTime_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.RefreshTime();
    }

    private void EndTime_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        (BindingContext as AppointmentViewModel)?.RefreshTime();
    }

    private void AppointmentView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (AppointmentId > 0)
        {
            var model = AppointmentServiceProxy.Current
                .Appointments.FirstOrDefault(a => a.Id == AppointmentId);
            if (model != null)
            {
                BindingContext = new AppointmentViewModel(model);
            }
            else
            {
                BindingContext = new AppointmentViewModel();
            }

        }
        else
        {
            BindingContext = new AppointmentViewModel();
        }
    }
}