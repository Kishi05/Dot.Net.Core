using Microsoft.AspNetCore.Mvc.ModelBinding;
using Section7.Models;

namespace Section7.CustomModelBuilder
{
    public class RegisterCustomBinder : IModelBinder
    {
        //In Custom Model Binder all the property has to be handled manually even assigning it to the correct property
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            RegisterCommon register = new RegisterCommon();
            if (bindingContext.ValueProvider.GetValue("Id").Count() > 0)
            {
                register.Id = int.Parse(bindingContext.ValueProvider.GetValue("Id").FirstValue);
            }
            if (bindingContext.ValueProvider.GetValue("Name").Count() > 0)
            {
                register.Name = bindingContext.ValueProvider.GetValue("Name").FirstValue;
            }
            if (bindingContext.ValueProvider.GetValue("Email").Count() > 0)
            {
                register.Email = bindingContext.ValueProvider.GetValue("Email").FirstValue;
            }
            if (bindingContext.ValueProvider.GetValue("Phone").Count() > 0)
            {
                register.Phone = bindingContext.ValueProvider.GetValue("Phone").FirstValue;
            }
            bindingContext.Result = ModelBindingResult.Success(register);
            return Task.CompletedTask;
        }
    }
}
