using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Reactive.Bindings;
using System.Collections.ObjectModel;
using WorkTime.SQLite;
using WorkTime.Views;
using WorkTime.WorkRecord.Service;

namespace WorkTime.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        //https://webbibouroku.com/Blog/Article/prism-navigation
        private IRegionManager _regionManager;
        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(TestView));
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }
    }
}
