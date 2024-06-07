using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace StockMarketApp.ViewModels
{
	public class ViewModelBase : ExtendedBindableObject
    {

        private bool _alreadyAttached = false;
        private bool _isBusy;
        private NavigationPage _navigationPage;
        public Page CurrentPage { get; set; }

        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ViewModelBase()
        {
            //DialogService = ViewModelLocator.Resolve<IDialogService>();
            //NavigationService = ViewModelLocator.Resolve<INavigationService>();
            //var settingsService = ViewModelLocator.Resolve<ISettingsService>();
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
        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult(false);
        }


        public virtual void ViewIsAppearing(object sender, EventArgs e)
        {
            if (!_alreadyAttached)
                AttachPageWasPoppedEvent();
        }

        public virtual void ViewIsDisappearing(object sender, EventArgs e)
        {

        }

        void AttachPageWasPoppedEvent()
        {
            var navPage = (this.CurrentPage.Parent as NavigationPage);
            if (navPage != null)
            {
                _navigationPage = navPage;
                _alreadyAttached = true;
                navPage.Popped += HandleNavPagePopped;
            }
        }

        private async void HandleNavPagePopped(object sender, NavigationEventArgs e)
        {
            if (e.Page == this.CurrentPage)
            {
                await RaisePageWasPopped();
            }
        }

        private async Task RaisePageWasPopped()
        {
            var navPage = (this.CurrentPage.Parent as NavigationPage);
            if (navPage != null)
                navPage.Popped -= HandleNavPagePopped;

            if (_navigationPage != null)
                _navigationPage.Popped -= HandleNavPagePopped;

            CurrentPage.Appearing -= ViewIsAppearing;
            CurrentPage.Disappearing -= ViewIsDisappearing;

            CurrentPage.BindingContext = null;
        }
    }
}
	

public abstract class ExtendedBindableObject : BindableObject
{
    public void RaisePropertyChanged<T>(Expression<Func<T>> property)
    {
        var name = GetMemberInfo(property).Name;
        OnPropertyChanged(name);
    }

    private MemberInfo GetMemberInfo(Expression expression)
    {
        MemberExpression operand;
        LambdaExpression lambdaExpression = (LambdaExpression)expression;
        if (lambdaExpression.Body as UnaryExpression != null)
        {
            UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
            operand = (MemberExpression)body.Operand;
        }
        else
        {
            operand = (MemberExpression)lambdaExpression.Body;
        }
        return operand.Member;
    }
}


