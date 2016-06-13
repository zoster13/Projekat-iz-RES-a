//-----------------------------------------------------------------------
// <copyright file="Logger.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

using System;
using System.IO;

namespace RES
{
    /// <summary>
    ///  Logger class is used for making log files and monitoring all data
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Method for making logs
        /// </summary>
        /// <param name="className">Represents the name of the class making the log</param>
        /// <param name="method">Represents the name of the method  making the log</param>
        /// <param name="logMessage">Message to be logged</param>
        /// <param name="w">TextWriter instance</param>
        public static void Log(string className, string method, string logMessage)
        {
            if (className == null || method == null || logMessage == null)
            {
                throw new ArgumentNullException("Parameter is missing or it is null");
            }

            if (className == string.Empty || method == string.Empty || logMessage == string.Empty)
            {
                throw new ArgumentException("Parameters can't be empty text");
            }

            using (StreamWriter w = File.AppendText("log.txt"))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                w.WriteLine("Class  :{0}", className);
                w.WriteLine("Function  :{0}", method);
                w.WriteLine("Message  :{0}", logMessage);
                w.WriteLine("-------------------------------");
            }
        }

        /// <summary>
        /// Method for reading logs
        /// </summary>
        public static void DumpLog()
        {
            using (StreamReader reader = new StreamReader("log.txt"))
            {
                if (reader == null)
                {
                    throw new ArgumentNullException("Argument can't be null");
                }

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
    }
}
