using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koolitused.Models;

namespace Koolitused.ViewModels
{
    internal class OpetajaListViewModel : BindableObject
    {

        private List<Roll> _roles;
        public List<Roll> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
                OnPropertyChanged(nameof(Roles));
            }
        }
    }
}
