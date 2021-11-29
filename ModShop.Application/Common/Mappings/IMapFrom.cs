using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModShop.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
