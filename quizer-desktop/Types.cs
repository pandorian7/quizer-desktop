using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quizer_desktop
{
    public class Credentials
    {
        public string username { set; get; }
        public string password { set; get; }
    }

    public class SvelteError : Exception
    {
        public SvelteError(string message) : base(message)
        {
        }
    }
    public class SvelteJSONError
    {
        public string message { set; get; }
    }

}
