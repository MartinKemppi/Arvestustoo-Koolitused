using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koolitused.Models;

namespace Koolitused.ViewModels
{
    public class KasutajaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        KasutajaListViewModel klvm;
        public Kasutaja Kasutaja { get; set; }
        public KasutajaViewModel()
        {
            Kasutaja = new Kasutaja();
        }
        public KasutajaListViewModel KasutajaListViewModel
        {
            get { return klvm; }
            set
            {
                if (klvm == value) return;
                klvm = value;
                OnPropertyChanged("KasutajaListViewModel");
            }
        }
        public string Name
        {
            get { return Kasutaja.Kasutajanimi; }
            set
            {
                if (Kasutaja.Kasutajanimi == value) return;
                Kasutaja.Kasutajanimi = value;
                OnPropertyChanged("Name");
            }
        }
        public string Pass
        {
            get { return Kasutaja.Kasutajasalasona; }
            set
            {
                if (Kasutaja.Kasutajasalasona == value) return;
                Kasutaja.Kasutajasalasona = value;
                OnPropertyChanged("Kasutajasalasona");
            }
        }
        public string Phone
        {
            get { return Friend.Phone; }
            set
            {
                if (Friend.Phone == value) return;
                Friend.Phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public bool IsValid
        {
            get
            {
                return new string[] { Name, Email, Phone }.Any(x => !string.IsNullOrEmpty(x?.Trim()));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
