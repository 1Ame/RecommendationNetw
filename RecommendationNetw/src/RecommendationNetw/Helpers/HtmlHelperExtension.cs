using Microsoft.AspNet.Mvc.Rendering;
using RecommendationNetw.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecommendationNetw
{
    public static class HtmlHelperExtension
    {
        public static IEnumerable<SelectListItem> GetVariantSelectList(this IHtmlHelper helper, ICollection<Variant> variants)
        {
            return variants.OrderBy(x=>x.NumericValue).Select(x => new SelectListItem() { Text = x.TextValue, Value = x.NumericValue.ToString() });
        }
    }
}
