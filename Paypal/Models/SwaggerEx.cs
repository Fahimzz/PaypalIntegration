using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerExcludeAttribute:Attribute
    {

    }
}
