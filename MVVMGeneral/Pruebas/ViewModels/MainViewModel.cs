using MVVMGeneric;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Pruebas.ViewModels
{
    public class MainViewModel : CloseWindowVM
    {
        private int _id;
        [Required]
        public int Id
        {
            get { return _id; }
            set { _id = value; Notify(); }
        }
        private string _name;
        [Required,MinLength(5),MaxLength(10)]
        public string Name
        {
            get { return _name; }
            set { _name = value; Notify(); }
        }

        Dictionary<int, string> lista = new Dictionary<int, string>();

        public ICommand AddCommand { get; set; }

        public MainViewModel()
        {
            AddCommand = new RelayCommand<Object>(AddExect, CanAddexect);

        }

        private void AddExect(object obj)
        {
            lista.Add(_id, _name);
        }

        private bool CanAddexect(object obj)
        {
            bool v = lista.Any(x => x.Key == _id);
            if (_name == null)
            {
                return false;
            }
            return ((_id != 0 && !v ) && !_name.Trim().Equals(""));
        }
    }
}
