using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Web.Xmarket.Helpers
{
    public static class HtmlExtensions
    {
        public static string IsSelected(this System.Web.Mvc.HtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {

            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller == currentController && action == currentAction ?
                 cssClass : string.Empty;
        }



        public static MvcHtmlString Chosen(this System.Web.Mvc.HtmlHelper html, string name, Dictionary<string, string> data, string placeholder = null, string selectedValue = null)
        {
            var htmlInput = "<select id=\"{0}\" class=\"form-control\" name=\"{0}\" placeholder=\"{1}\">[options]</select>";
            htmlInput = string.Format(htmlInput, name, placeholder);

            var options = new StringBuilder();

            foreach (var d in data)
            {
                var selected = "";
                if (d.Key == selectedValue)
                    selected = "selected";

                options.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", d.Key, selected, d.Value));
            }

            htmlInput = htmlInput.Replace("[options]", options.ToString());

            var scriptHtml = "<script>$(document).ready(function(){ $('#" + name + "').chosen(); })</script>";

            return MvcHtmlString.Create(htmlInput + scriptHtml);
        }

        public static string PageClass(this System.Web.Mvc.HtmlHelper html)
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            return currentAction;
        }
    }
}