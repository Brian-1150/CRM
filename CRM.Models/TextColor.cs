using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models
{

    //Class created in an attempt to assign color to specific item in EnumDropDownList
    public sealed class TextColor
    {
        public string CssClass { get; }

        private static IDictionary<string, TextColor> _instances = new Dictionary<string, TextColor>();

        private TextColor(string cssClass)
        {
            CssClass = cssClass;
        }

        private static TextColor GetInstance(string cssClass)
        {
            if (!_instances.ContainsKey(cssClass))
            {
                _instances[cssClass] = new TextColor(cssClass);
            }

            return _instances[cssClass];
        }

        public static TextColor Primary => GetInstance("text-primary");
        public static TextColor Secondary => GetInstance("text-secondary");

        // Add others here
    }
}