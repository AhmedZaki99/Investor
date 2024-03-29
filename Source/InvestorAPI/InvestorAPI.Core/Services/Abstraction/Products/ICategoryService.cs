﻿using InvestorData;

namespace InvestorAPI.Core
{
    /// <summary>
    /// Provides an abstraction for a service responsible for handling and processing <see cref="Category"/> data.
    /// </summary>
    public interface ICategoryService : IBusinessEntityService<Category, CategoryOutputDto, CategoryCreateInputDto, CategoryUpdateInputDto>
    {



    }
}
