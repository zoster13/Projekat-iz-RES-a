//-----------------------------------------------------------------------
// <copyright file="Writter.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Reflection;
using System.Threading;
using DumpingBuffer_NS;

namespace RES
{
    public class Writter
    {
        private Codes code;
        private float val;
        private object gate = new object();
        private DumpingBuffer db = new DumpingBuffer();
        
        public Codes Code
        {
            get
            {
                return this.code;
            }

            set
            {
                this.code = value;
            }
        }

        public float Value
        {
            get
            {
                return this.val;
            }

            set
            {
                this.val = value;
            }
        }

        public object Gate
        {
            get
            {
                return this.gate;
            }

            set
            {
                this.gate = value;
            }
        }

        public Writter()
        {
            this.Code = Codes.CODE_DIGITAL;
            this.Value = 0;       
        }

        public Writter(Codes code, float val)
        {
            int tempCode = (int)code;
            if (tempCode < 0 || tempCode > 7)
            {
                throw new ArgumentException("Kod mora biti izmedju 0 i 7");
            }

            if (val < 0 || val > 10)
            {
                throw new ArgumentException("Vrednost mora biti izmedju 0 i 10");
            }

            this.Code = code;
            this.Value = val;
        }

        public void StartWritting()
        {
            Console.WriteLine("\nPress SPACE to switch data flow to Historical\n");
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Spacebar))
            {
                SendToDumpingBuffer();              
            }

            Console.WriteLine("\nPress ENTER to switch data flow to Dumping Buffer\n");
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter))
            {
                ManualWriteToHistory();
            }
        }

        /// <summary>
        /// Sending to Dumping Buffer component
        /// </summary>
        private void SendToDumpingBuffer()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            string functionName = MethodBase.GetCurrentMethod().Name;
            Generate();
            Console.WriteLine("Sending to Dumping Buffer\n");
            lock (Gate)
            {
                Logger.Log(className, functionName, "Sending to Dumping Buffer");                
            }

            //TO DO: make a call to Dumping Buffer 
            db.WriteToDumpingBuffer(Code, Value);
                  
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Sending to historical component
        /// </summary>
        private void ManualWriteToHistory()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            string functionName = MethodBase.GetCurrentMethod().Name;

            Generate();
            Console.WriteLine("Sending to Historical\n");
            lock (Gate)
            {
                Logger.Log(className, functionName, "Sending to Historical");                
            }

            ////TO DO: make a call to Historical
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Generating random Codes and Values from 0 to 10
        /// </summary>
        private void Generate()
        {
            Random rnd = new Random();
            Code = (Codes)rnd.Next(0, 8);
            Value = (float)rnd.NextDouble() * 10;
            Value = (float)((int)(Value * 100 + 0.5) / 100.0);
            Console.WriteLine("Code: {0}", Code);
            Console.WriteLine("Value: {0}", Value);
        }
    }
}
