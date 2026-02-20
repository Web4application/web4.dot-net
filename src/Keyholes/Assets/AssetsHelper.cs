using System;
using System.Text;

namespace Web4.Keyholes.Assets;

public static class AssetsHelper
{
    public static readonly byte[] WEB4_JS = 
        Encoding.UTF8.GetBytes(new StreamReader(System.Reflection.Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("Web4.Keyholes.Assets.web4.js")!
        ).ReadToEnd());

    public static readonly byte[] WEB4_CSS = 
        Encoding.UTF8.GetBytes(new StreamReader(System.Reflection.Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("Web4.Keyholes.Assets.web4.css")!
        ).ReadToEnd());

}
