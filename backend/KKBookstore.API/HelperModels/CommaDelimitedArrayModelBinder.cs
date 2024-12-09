using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace KKBookstore.API.HelperModels;

public class CommaDelimitedArrayModelBinder<T> : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        ArgumentNullException.ThrowIfNull(bindingContext);

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult != ValueProviderResult.None)
        {
            var value = valueProviderResult.FirstValue;
            if (value != null)
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                var values = value.Split(',')
                                  .Select(s => (T?)converter.ConvertFromString(s.Trim()))
                                  .ToList();
                bindingContext.Result = ModelBindingResult.Success(values);
            }
        }

        return Task.CompletedTask;
    }
}
