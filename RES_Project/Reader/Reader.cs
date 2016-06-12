using CommonLibrary;
using Historical_NS;
using MyGlobals.HistoricalProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RES
{
    public  class Reader
    {
        private Historical historical = Historical.Instance();      //Singleton pattern

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

        public bool SendToHistorical()
        {
            Code = (Codes)Meni();
            if((int)Code == 8)
            {
                return false;
            }
            Console.WriteLine("Poslato : {0}", Code);
            List<HistoricalProperty> properties =  historical.GetChangesForInterval(code);

            ShowAllProperties(properties);
            return true;
            
        }

        private void ShowAllProperties(List<HistoricalProperty> properties)
        {
            foreach(HistoricalProperty hp in properties)
            {
                Console.WriteLine("\n-----------------------------------------------");
                Console.WriteLine("\tCode: {0}", hp.Code);
                Console.WriteLine("\tHistoricalValue: {0}", hp.HistoricalValue);
                Console.WriteLine("\tTime: {0}", hp.Time);
                Console.WriteLine("-----------------------------------------------\n");
            }
        }

        public int Meni()
        {
            int menuchoice = 0;
            while (menuchoice != 8)
            {
                Console.WriteLine("Unesite opciju za zeljeni kod: \n");
                Console.WriteLine("1. CODE_ANALOG");
                Console.WriteLine("2. CODE_DIGITAL");
                Console.WriteLine("3. CODE_CUSTOM");
                Console.WriteLine("4. CODE_LIMITSET");
                Console.WriteLine("5. CODE_SINGLENODE");
                Console.WriteLine("6. CODE_MULTIPLENODE");
                Console.WriteLine("7. CODE_CONSUMER");
                Console.WriteLine("8. CODE_SOURCE\n");
                Console.WriteLine("9. EXIT\n");
                try
                {
                    menuchoice = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
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
