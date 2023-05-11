using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeaselApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutPageMainFlyout : ContentPage
    {
        public ListView ListView;

        public FlyoutPageMainFlyout()
        {
            InitializeComponent();

            BindingContext = new FlyoutPageMainFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        private class FlyoutPageMainFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<FlyoutPageMainFlyoutMenuItem> MenuItems { get; set; }

            public FlyoutPageMainFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<FlyoutPageMainFlyoutMenuItem>(new[]
                {
                    new FlyoutPageMainFlyoutMenuItem { Id = 0, Title = "WeaselControl", TargetType = typeof(MainPage) }
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}