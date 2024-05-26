using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Koolitused.Views;

namespace Koolitused.ViewModels
{
    public class KasutajaListViewModel
    {
        //public ObservableCollection<KasutajaViewModel> Kasutajad { get; set; }
        //public event PropertyChangedEventHandler PropertyChanged;
        //public ICommand CreateKasutajaCommand { get; protected set; }
        //public ICommand DeleteKasutajaCommand { get; protected set; }
        //public ICommand SaveKasutajaCommand { get; protected set; }
        //public ICommand BackCommand { get; protected set; }
        //private KasutajaViewModel _selectedKasutaja;
        //public INavigation Navigation { get; set; }

        //public KasutajaListViewModel()
        //{
        //    Kasutajad = new ObservableCollection<KasutajaViewModel>();
        //    CreateKasutajaCommand = new Command(CreateKasutaja);
        //    DeleteKasutajaCommand = new Command(DeleteKasutaja);
        //    SaveKasutajaCommand = new Command<object>(SaveKasutaja);
        //    BackCommand = new Command(Back);
        //}

        //public KasutajaViewModel SelectedKasutaja
        //{
        //    get { return _selectedKasutaja; }
        //    set
        //    {
        //        if (_selectedKasutaja == value) return;
        //        _selectedKasutaja = value;
        //        OnPropertyChanged("SelectedKasutaja");
        //        Navigation.PushAsync(new KasutajaPage(/*value*/));
        //    }
        //}

        //protected void OnPropertyChanged(string propName)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        //}

        //private void CreateKasutaja() => Navigation.PushAsync(new KasutajaPage(new KasutajaViewModel() { KasutajaListViewModel = this }));

        //private void Back() => Navigation.PopAsync();

        //private void SaveKasutaja(object friendObj)
        //{
        //    if (!(friendObj is KasutajaViewModel friend) || friend == null || !friend.IsValid || Kasutajad.Contains(friend)) return;
        //    Kasutajad.Add(friend);
        //    Back();
        //}

        //private void DeleteKasutaja(object friendObj)
        //{
        //    if (!(friendObj is KasutajaViewModel friend) || friend == null) return;
        //    Kasutajad.Remove(friend);
        //    Back();
        //}

    }
}
