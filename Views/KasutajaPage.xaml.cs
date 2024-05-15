using Koolitused.ViewModels;

namespace Koolitused.Views;

public partial class KasutajaPage : ContentPage
{
    public KastuajaViewModel ViewModel { get; private set; }
    public KasutajaPage(KastuajaViewModel vm)
    {
        InitializeComponent();
        ViewModel = vm;
        BindingContext = ViewModel;
    }
}