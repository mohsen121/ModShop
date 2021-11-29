using ModShop.Application.Common.Interfaces;
using ModShop.RazorPages.Pages._Adminstrator.Products;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModShop.Application.BaseEntities.Queries.GetBaseEntity;
using ModShop.Application.BaseEntities.Commands.EditBaseEntity;
using ModShop.Domain;
using ModShop.Application.Common.Exceptions;

namespace ModShop.RazorPages.Pages._Adminstrator
{
    public abstract class BaseEditPage<TQuery, TCommand, TEntity, TKey> : BaseAdminstratorPage
        where TQuery : IGetBaseEntityQuery<TEntity, TKey>, new()
        where TCommand : IEditBaseEntityCommand<TEntity>, new()
        where TEntity : BaseEntity<TKey>, new()
    {
        private readonly string[] includes;
        private readonly string redirectRoute;

        [BindProperty]
        public virtual TCommand Command { get; set; }
        public BaseEditPage(string[] includes = null, string redirectRoute = null)
        {
            this.includes = includes;
            this.redirectRoute = redirectRoute;
        }

        public virtual async Task<IActionResult> OnGet(TKey id)
        {
            var query = new TQuery
            {
                Id = id,
                Includes = includes
            };
            var entity = await Mediator.Send(query);

            Command = new TCommand { Entity = entity };

            return Page();
        }

        public virtual async Task<IActionResult> OnPost(TKey id)
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                var updatedEntity = await Mediator.Send(Command);
            }
            catch (ValidationException ex)
            {
                foreach (var e in ex.Failures)
                {
                    ModelState.AddModelError(e.Key, string.Join('\n', e.Value));
                }
            }
            TempData["Success_Update"] = true;

            if (string.IsNullOrEmpty(redirectRoute))
            {
                return Page();
            }


            //if (!string.IsNullOrEmpty(_routeParam))
            //{
            //    var routeParamValue = (string)response.GetPropertyValue(_routeParam);

            //    if (string.IsNullOrEmpty(routeParamValue))
            //        throw new ArgumentException($"no {_routeParam} arg found in response");

            //    _redirectPage = _redirectPage.Replace(_routeParam, routeParamValue);
            //}

            return Redirect(redirectRoute);
        }
    }
}
