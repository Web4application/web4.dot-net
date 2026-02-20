using System;
using System.Text;

namespace Keyholes.Assets;

public static class AssetsHelper
{
    public static readonly byte[] WEB4_JS = 
        Encoding.UTF8.GetBytes(new StreamReader(System.Reflection.Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("Keyholes.Assets.web4.js")!
        ).ReadToEnd());

    public static readonly byte[] WEB4_CSS = 
        Encoding.UTF8.GetBytes(new StreamReader(System.Reflection.Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream("Keyholes.Assets.web4.css")!
        ).ReadToEnd());

}
