using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Common.Helpers;

public class QueryStringHelper
{
    public static T BindQueryStringToClass<T>(IQueryCollection query)
        where T : new()
    {
        // Yeni bir T nesnesi oluşturuyoruz
        var model = new T();

        // T tipinin özelliklerini alıyoruz
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            // Eğer query string'de property adına karşılık gelen bir key varsa
            if (query.ContainsKey(property.Name))
            {
                // Query string'deki değeri uygun tipe çevirip property'e set ediyoruz
                var value = query[property.Name];
                var convertedValue = Convert.ChangeType(value.ToString(), property.PropertyType);
                property.SetValue(model, convertedValue);
            }
        }

        return model;
    }
}
