using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SandBox.ActionFilters
{
    public interface IValidationPolicy : IFilterMetadata
    {
    }

    public class AutoValidateModelStateAttribute : ActionFilterAttribute, IValidationPolicy
    {
        public AutoValidateModelStateAttribute()
        {
            // S'assurer que ce filtre soit exécuté en dernier
            Order = int.MaxValue;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid && context.IsEffectivePolicy<IValidationPolicy>(this))
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowInvalidModelStateAttribute : Attribute, IValidationPolicy, IOrderedFilter
    {
        public int Order { get => int.MaxValue; }
    }

}
