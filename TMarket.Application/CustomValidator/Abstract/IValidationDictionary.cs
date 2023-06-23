using TMarket.Application.Services.Abstract;

namespace TMarket.Application.CustomValidator.Abstract
{
    public interface IValidationDictionary : IService
    {
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
    }
}
