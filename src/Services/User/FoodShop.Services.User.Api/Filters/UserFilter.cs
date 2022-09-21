using FoodShop.Services.User.Api.Dto;
using FoodShop.Services.User.Api.Filters.Contracts;
using FoodShop.Services.User.Api.Models;
using FoodShop.Services.User.Api.Specification.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Linq;

namespace FoodShop.Services.User.Api.Filters
{
    internal sealed class UserFilter : IFilter<UserModel>
    {
        public IEnumerable<UserModel> Filter(IEnumerable<UserModel> collection, ISpecification<UserModel> specification)
        {
            return collection.Where(item => specification.IsSatisfied(item));
        }
    }
}
