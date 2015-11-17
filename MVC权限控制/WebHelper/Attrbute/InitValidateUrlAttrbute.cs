using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebHelper.Attrbute
{
    public class InitValidateUrlAttribute : Attribute
    {
        public string Action { get; set; }
        public InitValidateUrlAttribute(string actionName)
        {
            this.Action = actionName;
        }
    }
}
