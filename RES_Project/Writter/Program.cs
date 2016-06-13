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
    /// <summary>
    /// Class used for starting Writer
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starting writer
        /// </summary>
        /// <param name="args">Arguments</param>
        public static void Main(string[] args)
        {
            Writter w = new Writter();       
            while (true)
            {
                w.StartWritting();                        
            }
        }       
    }
}
