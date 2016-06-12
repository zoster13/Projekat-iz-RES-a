using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RES
{
    class Program
    {
        static void Main(string[] args)
        {
            Reader r = new Reader();
            bool cont = true;

            while (cont)
                cont = r.SendToHistorical();
            
        }        
    }
}
