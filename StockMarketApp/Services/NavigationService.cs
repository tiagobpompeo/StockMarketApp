

using System.Globalization;
using System.Reflection;
using StockMarketApp.ViewModels;

namespace StockMarketApp.Services
{
    public class NavigationService : INavigationService
    {
       
        protected Application CurrentApplication => Application.Current;
       

        public ViewModelBase PreviousPageViewModel
        {
            get;
        }

        ViewModelBase INavigationService.PreviousPageViewModel => throw new NotImplementedException();

        public NavigationService()
        {
           
        }


        public Task InitializeAsync()
        {
            return NavigateToAsync<MainPageViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToModalAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToModalAsync(typeof(TViewModel), null);
        }

        public Task NavigateToModalAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToModalAsync(typeof(TViewModel), parameter);
        }

        public Task RemoveLastFromBackStackAsync()
        {
          
            return Task.FromResult(true);
        }

        public Task RemoveBackStackAsync()
        {
            
            return Task.FromResult(true);
        }

        private async Task InternalNavigateToModalAsync(Type viewModelType, object parameter)
        {
            
        }


        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            try
            {
                

            }
            catch (Exception ex)
            {

            }


        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("Model", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType, object parameter)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }
    }
}