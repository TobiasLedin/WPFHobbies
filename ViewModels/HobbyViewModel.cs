using WPFHobbies.Models;
using WPFHobbies.MVVM;

namespace WPFHobbies.ViewModels
{
    public class HobbyViewModel : ViewModelBase
    {
        private readonly Hobby _model;

        public string Name
        {
            get { return _model.Name; }
            set
            { 
                _model.Name = value;
                RaisePropertyChanged();
            }
        }
        public string Description
        {
            get { return _model.Description; }
            set
            { 
                _model.Description = value; 
                RaisePropertyChanged();
            }
        }

        public HobbyViewModel(Hobby model)
        {
            _model = model;
        }
    }
}
