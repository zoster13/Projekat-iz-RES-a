﻿using CommonLibrary;
using MyGlobals;
using MyGlobals.HistoricalProperties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Historical_NS
{
    public class Historical : IHistorical
    {
        private static Historical instance;

        private HistoricalProperty tempProperty;
        private List<HistoricalProperty> historicalProperties;
        private HistoricalDescription historicalDesc;

        private ListDescription LD1 = new ListDescription();
        private ListDescription LD2 = new ListDescription();
        private ListDescription LD3 = new ListDescription();
        private ListDescription LD4 = new ListDescription();

        private int dataset = 0;

        private bool valid = false;
        private bool write = false;

        /// <summary>
        /// Singleton pattern
        /// </summary>
        /// <returns></returns>
        public static Historical Instance()
        {
            if (instance == null)
                instance = new Historical();

            return instance;
        }

        public void WriteToHistory(CollectionDescription CD)
        {
            historicalDesc = new HistoricalDescription();
            historicalProperties = new List<HistoricalProperty>();
            ///historicalDesc.HistoricalProperties.Clear();        //resetuj pomocnu lokalnu prom
            //historicalProperties.Clear();   //reset

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

            if (write)
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
                        using (var stream1 = File.OpenWrite(@"..\..\..\LD1.xml"))
                        {
                            serializer1.Serialize(stream1, LD1);
                        }
                    }
                    break;

                case 2:
                    {
                        var serializer2 = new XmlSerializer(typeof(ListDescription));
                        using (var stream2 = File.OpenWrite(@"..\..\..\LD2.xml"))
                        {
                            serializer2.Serialize(stream2, LD2);
                        }
                    }
                    break;

                case 3:
                    {
                        var serializer3 = new XmlSerializer(typeof(ListDescription));
                        using (var stream3 = File.OpenWrite(@"..\..\..\LD3.xml"))
                        {
                            serializer3.Serialize(stream3, LD3);
                        }
                    }
                    break;

                case 4:
                    {
                        var serializer4 = new XmlSerializer(typeof(ListDescription));
                        using (var stream4 = File.OpenWrite(@"..\..\..\LD4.xml"))
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
            //Ukoliko LD struktura ima samo 1 HistoricalDescription, upisi ga, nema provjere Deadband-a
            if (LD.ListHistoricalDesc.Count == 1)
            {
                return true;
            }

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
                        if (hp.HistoricalValue < (histProperty.HistoricalValue - (histProperty.HistoricalValue/100)*2) ||
                            hp.HistoricalValue > (histProperty.HistoricalValue + (histProperty.HistoricalValue / 100) * 2))
                            {
                            //Izlazi iz Deadband-a, upisi u XML
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
            else if (code == Codes.CODE_SINGLENODE || code == Codes.CODE_MULTIPLENODE)
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

        /// <summary>
        /// Funkcija vraca listu HistoricalProperty sa prosledjenim kodom, u hronoloskom redosledu
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public List<HistoricalProperty> GetChangesForInterval(Codes code)
        {
            switch(code)
            {
                case Codes.CODE_ANALOG:
                case Codes.CODE_DIGITAL:
                    return GetChangesForCodeAnalogOrDigital(code);

                case Codes.CODE_CUSTOM:
                case Codes.CODE_LIMITSET:
                    return GetChangesForCodeCustomOrLimitset(code);

                case Codes.CODE_SINGLENODE:
                case Codes.CODE_MULTIPLENODE:
                    return GetChangesForCodeSinglenodeOrMultiplenode(code);


                case Codes.CODE_CONSUMER:
                case Codes.CODE_SOURCE:
                    return GetChangesForCodeConsumerOrSource(code);

                default:
                    return null;
            }
        }


        private List<HistoricalProperty> GetChangesForCodeAnalogOrDigital(Codes code)
        {
            List<HistoricalProperty> returnList = new List<HistoricalProperty>();

            XmlSerializer deserializer = new XmlSerializer(typeof(ListDescription));
            TextReader reader = new StreamReader(@"..\..\..\LD1.xml");
            ListDescription LD1  = (ListDescription)deserializer.Deserialize(reader);
            reader.Close();

            foreach(HistoricalDescription hd in LD1.ListHistoricalDesc)
            {
                foreach(HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if(hp.Code == code)
                    {
                        returnList.Add(hp);
                    }
                }
            }

            return returnList;
        }

        private List<HistoricalProperty> GetChangesForCodeCustomOrLimitset(Codes code)
        {
            List<HistoricalProperty> returnList = new List<HistoricalProperty>();

            XmlSerializer deserializer = new XmlSerializer(typeof(ListDescription));
            TextReader reader = new StreamReader(@"..\..\..\LD2.xml");
            ListDescription LD2 = (ListDescription)deserializer.Deserialize(reader);
            reader.Close();

            foreach (HistoricalDescription hd in LD2.ListHistoricalDesc)
            {
                foreach (HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if (hp.Code == code)
                    {
                        returnList.Add(hp);
                    }
                }
            }

            return returnList;
        }

        private List<HistoricalProperty> GetChangesForCodeSinglenodeOrMultiplenode(Codes code)
        {
            List<HistoricalProperty> returnList = new List<HistoricalProperty>();

            XmlSerializer deserializer = new XmlSerializer(typeof(ListDescription));
            TextReader reader = new StreamReader(@"..\..\..\LD3.xml");
            ListDescription LD3 = (ListDescription)deserializer.Deserialize(reader);
            reader.Close();

            foreach (HistoricalDescription hd in LD3.ListHistoricalDesc)
            {
                foreach (HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if (hp.Code == code)
                    {
                        returnList.Add(hp);
                    }
                }
            }

            return returnList;
        }

        private List<HistoricalProperty> GetChangesForCodeConsumerOrSource(Codes code)
        {
            List<HistoricalProperty> returnList = new List<HistoricalProperty>();

            XmlSerializer deserializer = new XmlSerializer(typeof(ListDescription));
            TextReader reader = new StreamReader(@"..\..\..\LD4.xml");
            ListDescription LD4 = (ListDescription)deserializer.Deserialize(reader);
            reader.Close();

            foreach (HistoricalDescription hd in LD4.ListHistoricalDesc)
            {
                foreach (HistoricalProperty hp in hd.HistoricalProperties)
                {
                    if (hp.Code == code)
                    {
                        returnList.Add(hp);
                    }
                }
            }

            return returnList;
        }
    }
}
