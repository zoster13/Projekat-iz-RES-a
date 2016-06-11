using CommonLibrary;
using MyGlobals;
using MyGlobals.HistoricalProperties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Historical_NS
{
    public class Historical : IHistorical
    {
        private HistoricalProperty tempProperty;
        private List<HistoricalProperty> historicalProperties = new List<HistoricalProperty>();
        private HistoricalDescription historicalDesc = new HistoricalDescription();

        private ListDescription LD1 = new ListDescription();
        private ListDescription LD2 = new ListDescription();
        private ListDescription LD3 = new ListDescription();
        private ListDescription LD4 = new ListDescription();

        private int dataset = 0;

        private bool valid = false;
        private bool write = false;

        public void WriteToHistory(CollectionDescription CD)
        {
            //Prepakivanje u LD strukturu
            historicalDesc.ID = CD.ID;
            historicalDesc.Dataset = CD.Dataset;

            foreach (DumpingProperty dp in CD.DumpingPropertyCollection.DumpingCollection)
            {
                tempProperty = new HistoricalProperty();
                tempProperty.Code = dp.Code;
                tempProperty.HistoricalValue = dp.DumpingValue;
                tempProperty.Time = DateTime.Now;

                valid = CheckDataset(tempProperty.Code);

                //Property ce biti dodat samo ako su Code i Dataset validni.
                if (valid)
                {
                    historicalProperties.Add(tempProperty);
                }
            }
            historicalDesc.HistoricalProperties = historicalProperties;

            

            //Dodaj u LD strukturu.
            if (historicalDesc.HistoricalProperties.Count == 2)     //bice 2 samo ako su oba prosla CheckDataset().
            {
                switch (dataset)
                {
                    case 1:
                        LD1.ListHistoricalDesc.Add(historicalDesc);
                        break;
                    case 2:
                        LD2.ListHistoricalDesc.Add(historicalDesc);
                        break;
                    case 3:
                        LD3.ListHistoricalDesc.Add(historicalDesc);
                        break;
                    case 4:
                        LD4.ListHistoricalDesc.Add(historicalDesc);
                        break;
                }
            }
            
            switch(dataset)
            {
                case 1:
                    write = CheckDeadband(LD1);
                    break;
                case 2:
                    write = CheckDeadband(LD2);
                    break;
                case 3:
                    write = CheckDeadband(LD3);
                    break;
                case 4:
                    write = CheckDeadband(LD4);
                    break;

            }

            //if (write)
            {
                //Izlazi iz Deadband-a, snimi u XML.
                SaveToXML();
            }

        }

        private void SaveToXML()
        {

            switch (historicalDesc.Dataset)
            {
                case 1:
                    {
                        var serializer1 = new XmlSerializer(typeof(ListDescription));
                        using (var stream1 = File.OpenWrite(@"C:\Users\Rade\Documents\GitHub\ProjekatRES\ProjekatRES-master\RES_Project\LD1.xml"))
                        {
                            serializer1.Serialize(stream1, LD1);
                        }
                    }
                    break;

                case 2:
                    {
                        var serializer2 = new XmlSerializer(typeof(ListDescription));
                        using (var stream2 = File.OpenWrite(@"C:\Users\Rade\Documents\GitHub\ProjekatRES\ProjekatRES-master\RES_Project\LD2.xml"))
                        {
                            serializer2.Serialize(stream2, LD2);
                        }
                    }
                    break;

                case 3:
                    {
                        var serializer3 = new XmlSerializer(typeof(ListDescription));
                        using (var stream3 = File.OpenWrite(@"C:\Users\Rade\Documents\GitHub\ProjekatRES\ProjekatRES-master\RES_Project\LD3.xml"))
                        {
                            serializer3.Serialize(stream3, LD3);
                        }
                    }
                    break;

                case 4:
                    {
                        var serializer4 = new XmlSerializer(typeof(ListDescription));
                        using (var stream4 = File.OpenWrite(@"C:\Users\Rade\Documents\GitHub\ProjekatRES\ProjekatRES-master\RES_Project\LD4.xml"))
                        {
                            serializer4.Serialize(stream4, LD4);
                        }
                    }
                    break;
            }
        }
        
    /// <summary>
    /// Funkcija provjerava da li podaci izlaze iz Deadband-a
    /// </summary>
    /// <returns></returns>
    private bool CheckDeadband(ListDescription LD)
    {
        foreach (HistoricalProperty hp in historicalProperties)
        {
            //Za CODE_DIGITAL se ne provjerava Deadbend
            if (hp.Code == Codes.CODE_DIGITAL)
                return true;

            //Provjera DeadBend-a
            foreach (HistoricalDescription hd in LD.ListHistoricalDesc)
            {
                foreach (HistoricalProperty histProperty in hd.HistoricalProperties)
                {
                    if (hp.Code == histProperty.Code)
                    {
                        if (hp.HistoricalValue < (histProperty.HistoricalValue - histProperty.HistoricalValue * 1.02) ||
                            hp.HistoricalValue > (histProperty.HistoricalValue + histProperty.HistoricalValue * 1.02))
                        {
                            //Izlazi iz Deadband-a, upisi u XML
                            LD.ListHistoricalDesc.Remove(hd);                 //obrisi stari
                            LD.ListHistoricalDesc.Add(historicalDesc);        //dodaj novi

                            return true;
                        }
                    }
                }
            }
        }

        //ne izlazi iz Deadband-a
        return false;
    }

        /// <summary>
        /// Funkcija koja provjerava Dataset na osnovu Code podataka koji su primljeni 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CheckDataset(Codes code)
        {
            int tempCode = (int)code;
            if (tempCode < 0 || tempCode > 7)
            {
                throw new ArgumentException("Kod mora biti izmedju 0 i 7");
            }
            
            if (code == Codes.CODE_ANALOG || code == Codes.CODE_DIGITAL)
            {
                dataset = 1;
                return true;
            }
            else if (code == Codes.CODE_CUSTOM || code == Codes.CODE_LIMITSET)
            {
                dataset = 2;
                return true;
            }
            else if (code == Codes.CODE_SINGLENOE || code == Codes.CODE_MULTIPLENODE)
            {
                dataset = 3;
                return true;
            }
            else if (code == Codes.CODE_CONSUMER || code == Codes.CODE_SOURCE)
            {
                dataset = 4;
                return true;
            }

            return false;
        }
    }
}
