using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milesi_Nasatti_WebServices
{
    public class commenti
    {
        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }

        override
        public string ToString()
        {
            return "{"+Environment.NewLine+"'postId': " + postId + ","+Environment.NewLine+"'id': " + id + ","+Environment.NewLine+"'name': " + name + ","+Environment.NewLine+"'email': " + email + ","+ Environment.NewLine +"'body': " + body + Environment.NewLine +"}";
        }
    }
}
