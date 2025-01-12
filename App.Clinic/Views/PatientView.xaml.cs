using App.Clinic.ViewModels;
using Library.Clinic.Models;
using Library.Clinic.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace App.Clinic.Views;

[QueryProperty(nameof(PatientId), "patientId")]
public partial class PatientView : ContentPage
{
    public int PatientId { get; set; }
    public PatientView()
	{
		InitializeComponent();
	}

    private void ReturnClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("//Patients");
	}

    private void ConfirmClicked(object sender, EventArgs e)
    {
		(BindingContext as PatientViewModel)?.ExecuteAdd();
    }
    private void PatientView_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (PatientId > 0)
        {
            var model = PatientServiceProxy.Current.
                Patients.FirstOrDefault(p => p.Id == PatientId);
            if (model != null)
            {
                BindingContext = new PatientViewModel(model);
            }
            else 
            { 
                BindingContext = new PatientViewModel(); 
            }
        }
        else
        {
            BindingContext = new PatientViewModel();
        }
    }
}