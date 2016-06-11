//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
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
