using InvestorData;

namespace Investor.Core
{
    /// <summary>
    /// Provides an abstraction for an endpoint client service, which manages <see cref="Category"/> models.
    /// </summary>
    public interface ICategoryClient : IBusinessEntityClient<Category, CategoryOutputDto, CategoryCreateInputDto, CategoryUpdateInputDto>
    {



    }
}
