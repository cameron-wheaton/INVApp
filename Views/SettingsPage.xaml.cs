using INVApp.ContentViews;
using INVApp.Services;
using INVApp.ViewModels;
using System.ComponentModel;

namespace INVApp.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();

        var databaseService = new DatabaseService(); 
        BindingContext = new SettingsViewModel(databaseService);

        App.NotificationService.OnNotify += message => NotificationBanner.Show(message);
    }
    public class LoyaltyPointsViewModel : INotifyPropertyChanged
    {
        private double _purchaseAmount;
        public double PurchaseAmount
        {
            get => _purchaseAmount;
            set
            {
                _purchaseAmount = value;
                OnPropertyChanged(nameof(PurchaseAmount));
                CalculatePoints();
            }
        }

        private double _pointsPercentage;
        public double PointsPercentage
        {
            get => _pointsPercentage;
            set
            {
                _pointsPercentage = value;
                OnPropertyChanged(nameof(PointsPercentage));
                CalculatePoints();
            }
        }

        private double _earnedPoints;
        public double EarnedPoints
        {
            get => _earnedPoints;
            private set
            {
                _earnedPoints = value;
                OnPropertyChanged(nameof(EarnedPoints));
            }
        }

        private void CalculatePoints()
        {
            EarnedPoints = (PurchaseAmount * PointsPercentage) / 100;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}