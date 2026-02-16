using System.Runtime.CompilerServices;
using MicroHtml;
using MicroHtml.Composers;
using Microsoft.AspNetCore.Http;

namespace Web4.Keyholes.Composers;

public class HttpResponseComposer(HttpResponse httpResponse) : HtmlComposer(httpResponse.BodyWriter)
{
    private HttpResponse _httpResponse = httpResponse;
    private string? _oneShotOptimization = null;

    public override bool OnMarkup(ref Html parent, ref string literal, int relativeOrder = -1)
    {
        if (LiteralLength > 0 && KeyholeCount <= 1)
        {
            _oneShotOptimization = literal;
            return true;
        }
        
        return base.OnMarkup(ref parent, ref literal, relativeOrder);
    }

    public Task WriteAsync([InterpolatedStringHandlerArgument("")] ref Html html)
    {
        var oneShot = _oneShotOptimization;
        var response = _httpResponse;

        html.Dispose();

        if (oneShot != null)
        {
            response.ContentLength = oneShot.Length;
            return response.WriteAsync(oneShot, response.HttpContext.RequestAborted);
        }

        return Task.CompletedTask;
    }

    public override void Reset()
    {
        _httpResponse = null!;
        _oneShotOptimization = null;
        base.Reset();
    }

    [ThreadStatic] static HttpResponseComposer? reusable;
    public static HttpResponseComposer Reuse(HttpResponse httpResponse)
    {
        if (reusable is {} composer)
        {
            composer._httpResponse = httpResponse;
            composer.Writer = httpResponse.BodyWriter;
            return composer;
        }

        return reusable = new HttpResponseComposer(httpResponse);
    }
}
