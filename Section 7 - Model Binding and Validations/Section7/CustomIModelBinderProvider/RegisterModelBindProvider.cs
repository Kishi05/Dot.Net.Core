using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Section7.CustomModelBuilder;
using Section7.Models;

namespace Section7.CustomIModelBinderProvider
{
    public class RegisterModelBindProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(RegisterCommon))
            {
                return new BinderTypeModelBinder(typeof(RegisterCustomBinder));
            }
            return null;
        }
    }
}
