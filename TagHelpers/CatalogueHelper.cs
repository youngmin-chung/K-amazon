using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using K_amazon.Models;
using K_amazon.Utils;
using System.Text;
using Newtonsoft.Json;

namespace K_amazon.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("catalogue", Attributes = BrandIdAttribute)]
    public class CatalogueHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public CatalogueHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private const string BrandIdAttribute = "brand";
        [HtmlAttributeName(BrandIdAttribute)]
        public string BrandId { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (_session.Get<ProductViewModel[]>(SessionVariables.Product) != null && Convert.ToInt32(BrandId) > 0)
            {
                var innerHtml = new StringBuilder();
                ProductViewModel[] product = _session.Get<ProductViewModel[]>(SessionVariables.Product);
                innerHtml.Append("<h5>Items</h5>");
                innerHtml.Append("<div class=\"row w-100 m-1\" style=\"overflow-y:scroll; height:60vh;\">");
                foreach (ProductViewModel item in product)
                {
                    if (item.BrandId == Convert.ToInt32(BrandId))
                    {
                        // remove apostrophe from JSON
                        item.Description = item.Description.Contains("'") ? item.Description.Replace("'", "") : item.Description;
                        var itemJson = JsonConvert.SerializeObject(item);
                        var btn = "branbtn" + item.Id;
                        innerHtml.Append("<div id=\"item\" class=\"col-sm-3 col-xs-12 text-center border border-info\" style =\"border:solid;\">");
                        innerHtml.Append("<img src =\"/images/" + item.GNAME + "\"style = \"height:95px;width:160px;\"/><br />");

                        if (item.Description.Length > 25)
                        {
                            innerHtml.Append("<span class=\"m-0\" style=\"font-size:large;font-weight:bold;\">" + item.Description.Substring(0, 15) + "...</span>");
                        }
                        else
                        {
                            innerHtml.Append("<span class=\"m-0\" style=\"font-size:large;font-weight:bold;\">" + item.Description + "...</span>");
                        }
                        innerHtml.Append("<p><span style=\"font-size:medium\">Item info. in details</span >");
                        innerHtml.Append("<p><a id=\"" + btn + "\" href=\"#details_popup\" data-toggle=\"modal\"");
                        innerHtml.Append(" class=\"btn btn-info pt-2 pb-2\" data-id=" + item.Id);
                        innerHtml.Append(" data-details='" + itemJson + "'>Details</a></div>");
                    }
                }
                output.Content.SetHtmlContent(innerHtml.ToString());
            }
        }
    }
}
