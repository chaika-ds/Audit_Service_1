using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AuditService.Setup.ModelProviders;

public class ResponseHttpCodeModelProvider : IApplicationModelProvider
{
    // It's all in the documentation https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/application-model?view=aspnetcore-6.0#iapplicationmodelprovider
    // Order property is for when you have multiple IApplicationModelProvider and you can set which one runs first. 3 is just a random number!
    public int Order => 3;

    public void OnProvidersExecuted(ApplicationModelProviderContext context)
    {
    }

    public void OnProvidersExecuting(ApplicationModelProviderContext context)
    {
        foreach (var controller in context.Result.Controllers)
        foreach (var action in controller.Actions)
        {
            Type? returnType = null;
            if (action.ActionMethod.ReturnType.GenericTypeArguments.Any())
                returnType = action.ActionMethod.ReturnType.GenericTypeArguments[0];

            var methodVerbs = action.Attributes.OfType<HttpMethodAttribute>().SelectMany(x => x.HttpMethods).Distinct();
            var actionParametersExist = action.Parameters.Any();

            AddUniversalStatusCodes(action, returnType);

            if (actionParametersExist)
                AddProducesResponseTypeAttribute(action, null, StatusCodes.Status404NotFound);

            if (methodVerbs.Contains("POST"))
                AddPostStatusCodes(action, returnType, actionParametersExist);
        }
    }

    private void AddProducesResponseTypeAttribute(IFilterModel action, Type? returnType, int statusCodeResult)
    {
        var newAttr = returnType != null
            ? new ProducesResponseTypeAttribute(returnType, statusCodeResult)
            : new ProducesResponseTypeAttribute(statusCodeResult);

        action.Filters.Add(newAttr);
    }

    private void AddUniversalStatusCodes(IFilterModel action, Type? returnType)
    {
        AddProducesResponseTypeAttribute(action, returnType, StatusCodes.Status200OK);
        AddProducesResponseTypeAttribute(action, null, StatusCodes.Status400BadRequest);
        AddProducesResponseTypeAttribute(action, null, StatusCodes.Status401Unauthorized);
        AddProducesResponseTypeAttribute(action, null, StatusCodes.Status403Forbidden);
        AddProducesResponseTypeAttribute(action, typeof(ProblemDetails), StatusCodes.Status500InternalServerError);
    }

    private void AddPostStatusCodes(IFilterModel action, Type? returnType, bool actionParametersExist)
    {
        AddProducesResponseTypeAttribute(action, returnType, StatusCodes.Status201Created);
        
        if (actionParametersExist == false)
            AddProducesResponseTypeAttribute(action, null, StatusCodes.Status404NotFound);
    }
}