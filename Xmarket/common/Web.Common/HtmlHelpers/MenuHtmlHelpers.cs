using Seguridad.Common;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Linq;
using System.Web.Routing;
using System;

namespace Web.Common.HtmlHelpers
{
    public static class MenuHtmlHelpers
    {
        public static MvcHtmlString ListarMenu(this HtmlHelper helper, IEnumerable<MenuOpcion> opciones)
        {
            var urlhelper = new UrlHelper(helper.ViewContext.RequestContext);

            StringBuilder html_prin = new StringBuilder();

            if (opciones == null) { return new MvcHtmlString(""); }
            
            foreach (var opcion in opciones.OrderBy(x=>x.Secuencia))
            {

                string _controller = urlhelper.RequestContext.RouteData.DataTokens["area"].ToString();
                

                var tag_li_prin = new TagBuilder("li");
                //creando enlace principal
                var tag_lnk_prin = new TagBuilder("a");
                TagBuilder tagDivPrin = new TagBuilder("div");





                var urlgenerate = MenuHtmlHelpers.GenerateUrl(new UrlHelper(helper.ViewContext.RequestContext), opcion.ControllerName, opcion.ActionName, opcion.AttributesRoute);
                tag_lnk_prin.MergeAttribute("href", urlgenerate);


                if (opcion.AttributesRoute != null)
                {
                    string[] v_router = opcion.AttributesRoute.Split(':');
                    if (_controller.Trim().ToUpper() == v_router[1].Trim().ToUpper())
                    {
                        tag_li_prin.AddCssClass("nav-item active");
                        tag_lnk_prin.AddCssClass("nav-link");
                        tag_lnk_prin.Attributes.Add("aria-expanded", "true");
                        tagDivPrin.AddCssClass("collapse show");
                    }
                    else
                    {
                        tag_li_prin.AddCssClass("nav-item");
                        tag_lnk_prin.AddCssClass("nav-link collapsed");
                        tag_lnk_prin.Attributes.Add("aria-expanded", "false");
                        tagDivPrin.AddCssClass("collapse");
                    }
                }

                tag_lnk_prin.Attributes.Add("data-toggle", "collapse");






                tag_lnk_prin.Attributes.Add("data-target", $"#{opcion.CodigoMenu}");
                tag_lnk_prin.Attributes.Add("aria-controls", $"{opcion.CodigoMenu}");

              

                //creando texto relleno
                StringBuilder str_lnk_innerht = new StringBuilder();
                str_lnk_innerht.AppendLine("<i class=\"fas fa-fw fa-wrench\"></i>");
                str_lnk_innerht.AppendLine("<span >" + opcion.NombreMenu + "</span>");
                

                tag_lnk_prin.InnerHtml = str_lnk_innerht.ToString();
                

                //integrando todos los tags
                tag_li_prin.InnerHtml = tag_lnk_prin.ToString();

               


                

                

                tagDivPrin.Attributes.Add("id", opcion.CodigoMenu);
                tagDivPrin.Attributes.Add("aria-labelledby", $"heading{opcion.CodigoMenu}");
                tagDivPrin.Attributes.Add("data-parent", "#accordionSidebar");


                if (opcion.MenuItem.Any())
                {
                    TagBuilder tagDivSecond = new TagBuilder("div");
                    tagDivSecond.AddCssClass("bg-white py-2 collapse-inner rounded");

                    TagBuilder tagh6Title = new TagBuilder("h6");
                    tagh6Title.AddCssClass("collapse-header");
                    tagh6Title.InnerHtml = opcion.NombreMenu;

                    tagDivSecond.InnerHtml = tagh6Title.ToString();

                    tagDivSecond.InnerHtml += MenuHtmlHelpers.ConstruirItems(helper, opcion.MenuItem.Where(x => !x.TipoMenu.Equals("N")), opcion.Nivel);

                    tagDivPrin.InnerHtml = tagDivSecond.ToString();

                }

                tag_li_prin.InnerHtml += tagDivPrin;

                html_prin.AppendLine(tag_li_prin.ToString());

               

            }

            return new MvcHtmlString(html_prin.ToString());
        }

        private static string GenerateUrl(UrlHelper urlhelper, string controllername, string actionname, string attribute)
        {
            if(string.IsNullOrEmpty(controllername) || string.IsNullOrEmpty(actionname)) return "#";
            
            RouteValueDictionary route = null;
            if (string.IsNullOrEmpty(attribute) == false)
            {
                route = new RouteValueDictionary();
                var attrArray = attribute.Split(',');
                for (var i = 0; i < attrArray.Length; i++)
                {
                    var attr = attrArray[i].Split(':');
                    if (attr.Length > 1)
                    {
                        route.Add(attr[0], Convert.ToString(attr[1]));
                    }
                }
            }
            return urlhelper.Action(actionname, controllername, route);
        }

        private static string ConstruirItems(HtmlHelper helper, IEnumerable<MenuOpcion> opciones, int level)
        {




            //  < div id = "collapseUtilities" class="collapse" aria-labelledby="headingUtilities" data-parent="#accordionSidebar">
            //  <div class="bg-white py-2 collapse-inner rounded">
            //    <h6 class="collapse-header">Custom Utilities:</h6>
            //    <a class="collapse-item" href="utilities-color.html">Colors</a>
            //    <a class="collapse-item" href="utilities-border.html">Borders</a>
            //    <a class="collapse-item" href="utilities-animation.html">Animations</a>
            //    <a class="collapse-item" href="utilities-other.html">Other</a>
            //  </div>
            //</div>


       
          
            StringBuilder item_second = new StringBuilder();

            foreach (var opcion in opciones.OrderBy(x=>x.Secuencia) )
            {
                var classIsSelectedLi = helper.IsSelected(null, opcion.ActionName, null);
                var classIsSelectedUL = helper.IsSelected(opcion.ControllerName, null, "in");
                
                


                TagBuilder tag_lnk_second = new TagBuilder("a");



                string v_action = helper.ViewContext.RequestContext.RouteData.Values["Action"].ToString();

                if (opcion.ActionName != null)
                {
                    if (opcion.ActionName.Trim().ToUpper() == v_action.Trim().ToUpper()) {

                        tag_lnk_second.AddCssClass("collapse-item active");
                    }
                    else
                    {
                        tag_lnk_second.AddCssClass("collapse-item");

                    }
                }


                tag_lnk_second.SetInnerText(opcion.NombreMenu);
                if (opcion.TipoMenu == "I"){
                    var urlgenerate = MenuHtmlHelpers.GenerateUrl(new UrlHelper(helper.ViewContext.RequestContext), opcion.ControllerName, opcion.ActionName, opcion.AttributesRoute);
                    tag_lnk_second.MergeAttribute("href", urlgenerate);
                    
                }

        
                item_second.AppendLine(tag_lnk_second.ToString());
            
            }


     

            return item_second.ToString();

        }

        private static string ObtenerLevelClass(int nivel)
        {
            var level = new Dictionary<int, string>();
            level.Add(1, "treeview-menu");
            level.Add(2, "treeview-menu");

            if (level.ContainsKey(nivel)) return level[nivel];
            return string.Empty;
        }

    }
}