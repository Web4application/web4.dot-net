using Microsoft.AspNetCore.Builder;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Routing;
using MicroHtml;
using Web4.Keyholes.Composers;

namespace Web4.WebSocket;

public static partial class Extensions
{
    public static IEndpointConventionBuilder MapGet(
        this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string pattern,
        Func<Html> template)
    {
        return endpoints.Map(pattern, async httpContext =>
        {
            // var pipeWriter = httpContext.Response.BodyWriter;
            // pipeWriter.Write($"{template()}");
            // await pipeWriter.FlushAsync(httpContext.RequestAborted);

            await HttpResponseComposer
                .Reuse(httpContext.Response)
                .WriteAsync($"{template()}");
        });
    }
}