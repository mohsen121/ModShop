using AutoMapper;
using FluentValidation.TestHelper;
using ModShop.Application.Common.Mappings;
using ModShop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Application.Common.Interfaces
{
    public abstract class BaseViewModel<TSource, TDestination>
    {

        public abstract IEnumerable<TDestination> Map(IEnumerable<TSource> source);
    }
}
