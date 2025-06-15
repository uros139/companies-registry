using Microsoft.AspNetCore.Mvc;

namespace CompaniesRegistry.Api.Extensions;

public static class ActionResultExtensions
{
    public static ActionResult<TDestination> ToActionResult<TDestination>(this TDestination? model) where TDestination : class =>
        model == null
            ? (ActionResult<TDestination>)new NotFoundResult()
            : new OkObjectResult(model);

    public static ActionResult<TDestination> ToCreatedActionResult<TDestination>(
    this TDestination model,
    string actionName,
    object routeValues)
    where TDestination : class =>
        new CreatedAtActionResult(actionName, null, routeValues, model);
}
