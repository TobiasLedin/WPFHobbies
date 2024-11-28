﻿using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using WPFHobbies.Models;
using WPFHobbies.MVVM;
using WPFHobbies.View;

namespace WPFHobbies.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<HobbyViewModel> _hobbies;
        private HobbyViewModel? _selectedHobby;
        private string? _newHobbyName;
        private string? _newHobbyDescription;

        public DelegateCommand CreateCommand { get; }
        public DelegateCommand DeleteCommand { get; }
        public DelegateCommand OpenWindowCommand { get; }
        public DelegateCommand CloseWindowCommand { get; }
        public Window? EntryWindow { get; set; }

        public ObservableCollection<HobbyViewModel> Hobbies
        {
            get { return _hobbies; }
            set
            {
                _hobbies = value;
                RaisePropertyChanged();
            }
        }

        public HobbyViewModel? SelectedHobby
        {
            get { return _selectedHobby; }
            set
            {
                _selectedHobby = value;
                RaisePropertyChanged();
            }
        }

        public string? NewHobbyName
        {
            get { return _newHobbyName; }
            set 
            {
                _newHobbyName = value;
                RaisePropertyChanged();
            }
        } 

        public string? NewHobbyDescription
        {
            get { return _newHobbyDescription; }
            set
            {
                _newHobbyDescription = value;
                RaisePropertyChanged();
            }
        } 

        public MainWindowViewModel()
        {
            CreateCommand = new DelegateCommand(AddHobby);
            DeleteCommand = new DelegateCommand(DeleteHobby);
            OpenWindowCommand = new DelegateCommand(OpenEntryWindow);
            CloseWindowCommand = new DelegateCommand(CloseEntryWindow);

            _hobbies = [];
            _hobbies.Add(new HobbyViewModel(
                    new Hobby("Go-kart", "Fast-paced racing with tiny motorized vehicles.")));
            _hobbies.Add(new HobbyViewModel(
                    new Hobby("Knitting", "Making seemingly impossible things out of a thin strands of cotton.")));
            _hobbies.Add(new HobbyViewModel(
                    new Hobby("Ice hockey", "A group of people wearing ice skates chasing a puck with clubs.")));
        }

        // Creates a new HobbyViewModel with a new Hobby based in inputted name and description.
        // To enable save to collection, field for Name cannot be empty.
        private void AddHobby(object? parameter)
        {
            if (!string.IsNullOrEmpty(_newHobbyName))
            {
                Hobbies.Add(new HobbyViewModel(new Hobby(_newHobbyName, _newHobbyDescription ?? string.Empty)));
                CloseEntryWindow(null);
            } 
        }

        // Deletes the selected object from the collection.
        private void DeleteHobby(object? parameter)
        {
            if (_selectedHobby != null)
            {
                var mainWindow = Application.Current.MainWindow;
                var result = MessageBox.Show(mainWindow,$"This action will delete hobby {_selectedHobby.Name}. Press OK to confirm", "Delete hobby", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    Hobbies.Remove(_selectedHobby);
                }
                
            }
        }

        // Open modal with user input fields.
        // Whilst modal is open, reduces opacity of mainWindow.
        private void OpenEntryWindow(object? parameter)
        {
            var mainWindow = Application.Current.MainWindow;

            EntryWindow = new EntryWindow
            {
                DataContext = this,
                Owner = mainWindow
            };

            mainWindow.Opacity = 0.5;
            EntryWindow.ShowDialog();
            mainWindow.Opacity = 1;

        }

        // Close modal
        private void CloseEntryWindow(object? parameter)
        {
            if (EntryWindow != null)
            {
                _newHobbyName = string.Empty;
                _newHobbyDescription = string.Empty;
                EntryWindow.Close();
            }
        }
    }
}