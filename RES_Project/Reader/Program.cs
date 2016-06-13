//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

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
