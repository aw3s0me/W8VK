using VKModel.Entities;

namespace VKViewModels.Services
{
    public interface ICommonErrorHandler
    {
        bool HandleError(Error error);
    }
}