#pragma checksum "/Users/zackmckevitt/Desktop/Computer_Science/CSCI_3308/BUFFiMG/BUFFiMG/Views/User/Welcome.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "534631b11cebac85b43e8349feebe6828c672ff8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Welcome), @"mvc.1.0.view", @"/Views/User/Welcome.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/zackmckevitt/Desktop/Computer_Science/CSCI_3308/BUFFiMG/BUFFiMG/Views/_ViewImports.cshtml"
using BUFFiMG;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/zackmckevitt/Desktop/Computer_Science/CSCI_3308/BUFFiMG/BUFFiMG/Views/_ViewImports.cshtml"
using BUFFiMG.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"534631b11cebac85b43e8349feebe6828c672ff8", @"/Views/User/Welcome.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3ad4e612d317f5b73978c6c5190f3c0c2f4905a1", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Welcome : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/Users/zackmckevitt/Desktop/Computer_Science/CSCI_3308/BUFFiMG/BUFFiMG/Views/User/Welcome.cshtml"
  
    ViewData["Title"] = "UserPage";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    <h2>");
#nullable restore
#line 5 "/Users/zackmckevitt/Desktop/Computer_Science/CSCI_3308/BUFFiMG/BUFFiMG/Views/User/Welcome.cshtml"
   Write(ViewData["Message"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\n\n<ul>\n");
#nullable restore
#line 8 "/Users/zackmckevitt/Desktop/Computer_Science/CSCI_3308/BUFFiMG/BUFFiMG/Views/User/Welcome.cshtml"
     for (int i = 0; i < (int)ViewData["NumTimes"]; i++)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <li>Picture</li>\n");
#nullable restore
#line 11 "/Users/zackmckevitt/Desktop/Computer_Science/CSCI_3308/BUFFiMG/BUFFiMG/Views/User/Welcome.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</ul>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
