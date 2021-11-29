using AutoMapper;
using ModShop.Application.Common.Interfaces;
using ModShop.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ModShop.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                //var t = type;
                //if (type.IsGenericType && typeof(BaseViewModel<,>) == type)
                //{
                //    continue;
                //    Type classType = typeof(BaseViewModel<,>);
                //    var typeParams =  type.GetGenericArguments();
                //    var newType = classType.MakeGenericType(typeParams);
                //    var obj = Activator.CreateInstance(newType);
                //}


                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
