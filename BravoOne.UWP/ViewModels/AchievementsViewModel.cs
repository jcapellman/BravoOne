using BravoOne.lib;
using BravoOne.lib.Managers;
using BravoOne.lib.UIObjects;
using BravoOne.UWP.ViewModels.Base;

using System.Collections.Generic;

namespace BravoOne.UWP.ViewModels
{
    public class AchievementsViewModel : BaseViewModel
    {
        private List<AchievementsListingItem> _achievementListing;

        public List<AchievementsListingItem> AchievementListing
        {
            get => _achievementListing;

            set
            {
                _achievementListing = value;

                OnPropertyChanged();
            }
        }

        public AchievementsViewModel(GameWrapper wrapper) : base(wrapper)
        {  
        }

        public async void LoadListing()
        {
            AchievementListing = await gWrapper.GetManager<AchievementManager>().GetAchievementListingAsync();
        }
    }
}