//-----------------------------------------------------------------------
// <copyright file="Reader.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

using Historical_NS;
using MyGlobals.HistoricalProperties;
using System;
using System.Collections.Generic;

namespace RES
{
    public  class Reader
    {
        private Historical historical = Historical.Instance();      
        private Codes code;

        public Codes Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }

        /// <summary>
        /// Konstruktor bez parametara.
        /// </summary>
        public Reader()
        {
            Code = Codes.CODE_DIGITAL;
        }

        public Reader(Codes code)
        {
            if((int)code < 0 || (int)code >7)
            {
                throw new ArgumentException("Invalid code");
            }
            this.Code = code;
        }

        /// <summary>
        /// Pomocna funkcija
        /// </summary>
        /// <returns></returns>
        public bool SendToHistorical()
        {
            Code = (Codes)Meni();       //izbor Code koji zelimo da citamo
            if((int)Code == 8)
            {
                return false;
            }
            Console.WriteLine("Zahtjev za: {0}", Code);
            List<HistoricalProperty> properties =  historical.GetChangesForInterval(code);

            ShowAllProperties(properties);
            return true;
        }

        /// <summary>
        /// Prikaz svih HistoricalProperty-a, koji imaju prosledjeni Code.
        /// </summary>
        /// <param name="properties"></param>
        private void ShowAllProperties(List<HistoricalProperty> properties)
        {
            switch(code)
            {
                case Codes.CODE_ANALOG:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;

                case Codes.CODE_DIGITAL:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;

                case Codes.CODE_CUSTOM:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;

                case Codes.CODE_LIMITSET:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;

                case Codes.CODE_SINGLENODE:
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;

                case Codes.CODE_MULTIPLENODE:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;

                case Codes.CODE_CONSUMER:
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;

                case Codes.CODE_SOURCE:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.WriteLine("\n-----------------------------------------------");
            foreach (HistoricalProperty hp in properties)
            {
                Console.WriteLine("\tCode: {0}", hp.Code);
                Console.WriteLine("\tHistoricalValue: {0}", hp.HistoricalValue);
                Console.WriteLine("\tTime: {0}", hp.Time);
                Console.WriteLine();
            }
            Console.WriteLine("-----------------------------------------------\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Izbor Code-a
        /// </summary>
        /// <returns></returns>
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
                Console.WriteLine("9. EXIT\n");
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
