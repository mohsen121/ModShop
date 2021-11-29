using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ModShop.Common.Util
{
    public static class PropertyTools
    {

        public static object GetPropertyValue(this object obj, string objName)
        {
            var objType = obj.GetType();
            var properties = new List<PropertyInfo>(objType.GetProperties());

            return properties.FirstOrDefault(x => x.Name == objName);
        }
    }
}
