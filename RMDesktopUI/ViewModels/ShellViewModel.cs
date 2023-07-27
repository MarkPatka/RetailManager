using Caliburn.Micro;
using RMDesktopUI.EventModels;
using System.Threading;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private readonly SalesViewModel _salesViewModel;
        private readonly IEventAggregator _events;
        private readonly SimpleContainer _container;

        #region ctor
        public ShellViewModel(IEventAggregator events, SalesViewModel salesViewModel, 
            SimpleContainer container)
        {
            _events = events; 
            _events.SubscribeOnPublishedThread(this);

            _salesViewModel = salesViewModel;
            _container = container;

            ActivateItemAsync(_container.GetInstance<LoginViewModel>());
        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(_salesViewModel);
        }
        #endregion
    }
}
