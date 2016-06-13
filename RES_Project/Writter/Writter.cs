//-----------------------------------------------------------------------
// <copyright file="Writter.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

using System;
using System.Reflection;
using System.Threading;
using DumpingBuffer_NS;
using Historical_NS;

namespace RES
{
    public class Writter
    {
        // Singleton pattern, postoji samo po 1 instanca DumpingBuffer i Historical-a
        private DumpingBuffer db = DumpingBuffer.Instance();
        private Historical historical = Historical.Instance();

        private Codes code;
        private float value;
        private object gate = new object();
        
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
                return this.value;
            }

            set
            {
                this.value = value;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Writter"/> class.
        /// </summary>
        public Writter()
        {
            this.Code = Codes.CODE_DIGITAL;
            this.Value = 0;       
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Writter"/> class.
        /// </summary>
        /// <param name="code">Code</param>
        /// <param name="val">Value</param>
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

            Console.WriteLine("\nPress BACKSPACE to switch data flow to Dumping Buffer\n");
            while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Backspace))
            {
                SendToHistoricalManual();
            }
        }

        /// <summary>
        /// Function automatically sending Code and Value to DumpingBuffer
        /// </summary>
        public void SendToDumpingBuffer()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            string functionName = MethodBase.GetCurrentMethod().Name;
            Generate();
            Console.WriteLine("Sending to Dumping Buffer\n");
            lock (Gate)
            {
                Logger.Log(className, functionName, "Sending to Dumping Buffer");                
            }

            // TO DO: make a call to Dumping Buffer 
            db.WriteToDumpingBuffer(Code, Value);
                  
            Thread.Sleep(2000);
        }

        /// <summary>
        /// Function sending Code and Value to DumpingBuffer which are manual entered
        /// </summary>
        public void SendToHistoricalManual()
        {
            string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
            string functionName = MethodBase.GetCurrentMethod().Name;

            lock (Gate)
            {
                Logger.Log(className, functionName, "Sending to Historical");                
            }

            bool validanUnos = false;
            Code = (Codes)Meni();
            
            if ((int)Code == 8)
            {
                StartWritting();
            }

            while (!validanUnos)
            {
                try
                {
                    Console.Write(">>Unesite vrijednost: ");
                    Value = float.Parse(Console.ReadLine());
                    validanUnos = true;
                }
                catch
                {
                    Console.WriteLine("Pogresan unos!");
                    validanUnos = false;
                }
            }

            // Call Historical
            Console.WriteLine("Code: {0}", Code);
            Console.WriteLine("Value: {0}", Value);
            Console.WriteLine("Sending to Historical...");
            historical.ManualWriteToHistory(Code, Value);
        }

        /// <summary>
        /// Generating random Codes and Values from 0 to 10
        /// </summary>
        public void Generate()
        {
            Random rnd = new Random();
            Code = (Codes)rnd.Next(0, 8);
            Value = (float)rnd.NextDouble() * 10;
            Value = (float)((int)((Value * 100) + 0.5) / 100.0);
            Console.WriteLine("Code: {0}", Code);
            Console.WriteLine("Value: {0}", Value);
        }

        /// <summary>
        /// Select code for send
        /// </summary>
        /// <returns> Menu choice </returns>
        public int Meni()
        {
            int menuchoice = 0;
            while (menuchoice != 8)
            {
                Console.WriteLine("Unesite opciju za zeljeni kod: \n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. CODE_ANALOG");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("2. CODE_DIGITAL");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("3. CODE_CUSTOM");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("4. CODE_LIMITSET");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("5. CODE_SINGLENODE");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("6. CODE_MULTIPLENODE");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("7. CODE_CONSUMER");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("8. CODE_SOURCE\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("9. WriteToDumpingBuffer\n");
                Console.ResetColor();
                try
                {
                    menuchoice = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\nUnesite broj\n");
                }

                switch (menuchoice)
                {
                    case 1:
                        return 0;
                    case 2:
                        return 1;
                    case 3:
                        return 2;
                    case 4:
                        return 3;
                    case 5:
                        return 4;
                    case 6:
                        return 5;
                    case 7:
                        return 6;
                    case 8:
                        return 7;
                    case 9:
                        return 8;
                    default:
                        Console.WriteLine("Invalidna selekcija\n");
                        return -1;
                }
            }

            return -1;
        }
    }
}
