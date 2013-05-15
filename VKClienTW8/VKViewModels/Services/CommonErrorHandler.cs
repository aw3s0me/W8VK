using VKCore;
using VKModel.Entities;
using Windows.UI.Xaml;

namespace VKViewModels.Services
{
    class CommonErrorHandler : ICommonErrorHandler
    {
        private readonly IVkApi vkontakteApi;
        private readonly Application application;
        private readonly ISimpleNavigationService navigationService;

        public CommonErrorHandler(IVkApi vkontakteApi, ISimpleNavigationService navigationService)
        {
            this.vkontakteApi = vkontakteApi;
            this.application = application;
            this.navigationService = navigationService;
        }

        public bool HandleError(Error error)
        {
            if (error.ErrorCode == "5")
            {
                vkontakteApi.DeleteContext();
                navigationService.NavigatToAuthorizationPage();
                return true;
            }
            return false;
        }
    }
}