using Koolitused.ViewModels;

namespace Koolitused.Views;

public partial class KasutajaPage : ContentPage
{
    public KasutajaViewModel ViewModel { get; private set; }
    public KasutajaPage(KasutajaViewModel vm)
    {
        InitializeComponent();
        ViewModel = vm;
        BindingContext = ViewModel;
    }
}