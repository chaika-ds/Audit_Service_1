using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace AuditService.WebApiApp.Providers;

public class ProduceResponseTypeModelProvider : IApplicationModelProvider
{
    public int Order => 3;

    public void OnProvidersExecuted(ApplicationModelProviderContext context)
    {
    }

    public void OnProvidersExecuting(ApplicationModelProviderContext context)
    {
        foreach (ControllerModel controller in context.Result.Controllers)
        {
            foreach (ActionModel action in controller.Actions)
            {
                Type? returnType = null;
                if (action.ActionMethod.ReturnType.GenericTypeArguments.Any())
                {
                    returnType = action.ActionMethod.ReturnType.GenericTypeArguments[0];
                }

                var methodVerbs = action.Attributes.OfType<HttpMethodAttribute>().SelectMany(x => x.HttpMethods).Distinct();
                bool actionParametersExist = action.Parameters.Any();

                AddUniversalStatusCodes(action, returnType);

                if (actionParametersExist)
                {
                    AddProducesResponseTypeAttribute(action, null, StatusCodes.Status404NotFound);
                }

                if (methodVerbs.Contains("POST"))
                {
                    AddPostStatusCodes(action, returnType, actionParametersExist);
                }
            }
        }
    }

    private void AddProducesResponseTypeAttribute(ActionModel action, Type? returnType, int statusCodeResult)
    {
        if (returnType != null)
        {
            action.Filters.Add(new ProducesResponseTypeAttribute(returnType, statusCodeResult));
        }
        else if (returnType == null)
        {
            action.Filters.Add(new ProducesResponseTypeAttribute(statusCodeResult));
        }
    }

    private void AddUniversalStatusCodes(ActionModel action, Type? returnType)
    {
        AddProducesResponseTypeAttribute(action, returnType, StatusCodes.Status200OK);
        AddProducesResponseTypeAttribute(action, null, StatusCodes.Status401Unauthorized);
        AddProducesResponseTypeAttribute(action, null, StatusCodes.Status403Forbidden);
        AddProducesResponseTypeAttribute(action, null, StatusCodes.Status500InternalServerError);
    }

    private void AddPostStatusCodes(ActionModel action, Type? returnType, bool actionParametersExist)
    {
        AddProducesResponseTypeAttribute(action, returnType, StatusCodes.Status201Created);
        AddProducesResponseTypeAttribute(action, null, StatusCodes.Status400BadRequest);
        if (actionParametersExist == false)
        {
            AddProducesResponseTypeAttribute(action, null, StatusCodes.Status404NotFound);
        }
    }
}