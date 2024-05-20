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
        public string Kasutajanimi
        {
            get { return Kasutaja.Kasutajanimi; }
            set
            {
                if (Kasutaja.Kasutajanimi == value) return;
                Kasutaja.Kasutajanimi = value;
                OnPropertyChanged("Kasutajanimi");
            }
        }
        public string Kasutajasalasona
        {
            get { return Kasutaja.Kasutajasalasona; }
            set
            {
                if (Kasutaja.Kasutajasalasona == value) return;
                Kasutaja.Kasutajasalasona = value;
                OnPropertyChanged("Kasutajasalasona");
            }
        }
        public int Roll
        {
            get { return Kasutaja.Roll; }
            set
            {
                if (Kasutaja.Roll == value) return;
                Kasutaja.Roll = value;
                OnPropertyChanged("Roll");
            }
        }
        public bool IsValid
        {
            get
            {
                return new string[] { Kasutajanimi, Kasutajasalasona, Roll.ToString() }.Any(x => !string.IsNullOrEmpty(x?.Trim()));
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null) return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
