using ModShop.Application.Common.Interfaces;
using ModShop.Common.Util;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ModShop.Application.BaseEntities.Commands.CreateBaseEntity;
using ModShop.Domain;
using ModShop.Application.Common.Exceptions;

namespace ModShop.RazorPages.Pages._Adminstrator
{
    public abstract class BaseCreatePage<TCommand, TEntity> : BaseAdminstratorPage
        where TCommand : ICreateBaseEntityCommand<TEntity>
        where TEntity : BaseEntity, new()
    {
        private string _redirectPage;

        public BaseCreatePage()
        {

        }
        public BaseCreatePage(string redirectPage)
        {
            _redirectPage = redirectPage;
        }

        [BindProperty]
        public virtual TCommand Command { get; set; }

        public virtual IActionResult OnGet()
        {
            return Page();
        }

        public virtual async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            TEntity response;
            try
            {
                response = await Mediator.Send(Command);
            }
            catch (ValidationException ex)
            {
                foreach (var e in ex.Failures)
                {
                    ModelState.AddModelError(e.Key, string.Join('\n', e.Value));
                }
                return Page();
            }

            TempData["Success_Create"] = true;

            if (string.IsNullOrEmpty(_redirectPage))
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

            return Redirect(_redirectPage);
        }
    }
}
