using Microsoft.AspNetCore.Mvc;

namespace CompaniesRegistry.Api.Extensions;

public static class ActionResultExtensions
{
    public static ActionResult<TDestination> ToActionResult<TDestination>(this TDestination? model) where TDestination : class =>
        model == null
            ? (ActionResult<TDestination>)new NotFoundResult()
            : new OkObjectResult(model);
}
