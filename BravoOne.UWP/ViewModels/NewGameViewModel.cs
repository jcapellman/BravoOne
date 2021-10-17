using BravoOne.lib;
using BravoOne.UWP.ViewModels.Base;

using System;
using System.Collections.Generic;
using System.Linq;

using Windows.Storage;

namespace BravoOne.UWP.ViewModels
{
    public class NewGameViewModel : BaseViewModel
    {
        private string _name;

        public string Name
        {
            get => _name;

            set
            {
                _name = value;
                OnPropertyChanged();

                Validate();
            }
        }

        private bool _enabled;

        public bool Enabled
        {
            get=> _enabled;

            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        private List<string> _logos;

        public List<string> Logos
        {
            get => _logos;

            set
            {
                _logos = value;
                OnPropertyChanged();
            }
        }

        private string _selectedLogo;
        
        public string SelectedLogo
        {
            get => _selectedLogo;

            set
            {
                _selectedLogo = value;
                OnPropertyChanged();
            }
        }

        private void Validate()
        {
            Enabled = !string.IsNullOrEmpty(Name);
        }

        private async void LoadImages()
        {
            Logos = new List<string>();

            StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;

            StorageFolder subFolder = await installedLocation.GetFolderAsync("Assets");
            subFolder = await subFolder.GetFolderAsync("Icons");

            var logos = await subFolder.GetFilesAsync();

            var tmpLogos = new List<string>();

            foreach (var logo in logos)
            {
                tmpLogos.Add(logo.Path);
            }

            Logos = tmpLogos;

            SelectedLogo = Logos.FirstOrDefault();
        }

        public NewGameViewModel(GameWrapper gWrapper) : base(gWrapper)
        {
            Name = string.Empty;


            LoadImages();
        }
    }
}