using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EticaretAPI.API.Extensions;

public class QueryRequestModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
    var modelType = bindingContext.ModelType;
    var modelInstance = Activator.CreateInstance(modelType);

    var properties = modelType.GetProperties();
    foreach (var prop in properties)
    {
    // Route parametrelerini ilgili özelliklere bağlama
    var value = bindingContext.ActionContext.RouteData.Values[prop.Name]?.ToString();
    if (value != null && prop.CanWrite)
    {
    var convertedValue = Convert.ChangeType(value , prop.PropertyType);
    prop.SetValue(modelInstance , convertedValue);
    }
    }

    bindingContext.Result = ModelBindingResult.Success(modelInstance);
    return Task.CompletedTask;
    }
}
