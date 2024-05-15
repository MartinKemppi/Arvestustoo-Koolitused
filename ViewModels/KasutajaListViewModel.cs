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
        public ObservableCollection<KasutajaViewModel> Kasutaja { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CreateKasutajaCommand { get; protected set; }
        public ICommand DeleteKasutajaCommand { get; protected set; }
        public ICommand SaveKasutajaCommand { get; protected set; }
        public ICommand BackCommand { get; protected set; }
        KasutajaViewModel selectedKasutaja;
        public INavigation Navigation { get; set; }
        public KasutajaListViewModel()
        {
            Kasutaja = new ObservableCollection<KasutajaViewModel>();
            CreateKasutajaCommand = new Command(CreateKasutaja);
            DeleteKasutajaCommand = new Command(DeleteKasutaja);
            SaveKasutajaCommand = new Command(SaveKasutaja);
            BackCommand = new Command(Back);
        }
        public KasutajaViewModel SelectedFriend
        {
            get { return selectedKasutaja; }
            set
            {
                if (selectedKasutaja == value) return;
                KasutajaViewModel temp = value;
                selectedKasutaja = null;
                OnPropertyChanged("SelectedFriend");
                Navigation.PushAsync(new KasutajaPage(temp));
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private void CreateKasutaja() => Navigation.PushAsync(new KasutajaPage(new KasutajaViewModel() { KasutajaListViewModel = this }));
        private void Back() => Navigation.PopAsync();
        private void SaveKasutaja(object friendObj)
        {
            if (friendObj is not KasutajaViewModel friend || friend == null || !friend.IsValid || Kasutaja.Contains(friend)) return;
            Kasutaja.Add(friend);
            Back();
        }
        private void DeleteKasutaja(object friendObj)
        {
            if (friendObj is not KasutajaViewModel friend || friend == null) return;
            Kasutaja.Remove(friend);
            Back();
        }

    }
}
