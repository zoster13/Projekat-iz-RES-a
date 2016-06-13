using CommonLibrary;
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

        private static ListDescription LD1;
        private static ListDescription LD2;
        private static ListDescription LD3;
        private static ListDescription LD4;

        private int dataset = 0;

        private bool valid = false;
        private bool write = false;

        /// <summary>
        /// Konstruktor bez parametara
        /// </summary>
        public Historical()
        {
        }

        /// <summary>
        /// Singleton pattern, omogucuje da uvijek imamo samo jednu instancu Historical-a
        /// </summary>
        /// <returns></returns>
        public static Historical Instance()
        {
            if (instance == null)
            {
                instance = new Historical();

                LD1 = new ListDescription();
                LD2 = new ListDescription();
                LD3 = new ListDescription();
                LD4 = new ListDescription();
            }
            return instance;
        }

        /// <summary>
        /// Pomocna funkcija
        /// </summary>
        /// <param name="CD"></param>
        public void WriteToHistory(CollectionDescription CD)
        {
            historicalDesc = new HistoricalDescription();
            historicalProperties = new List<HistoricalProperty>();
            
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
                        write = CheckDeadband(LD1, false);

                        if (write)
                        {
                            //Ako izlazi iz Deadband-a, onda upisi
                            SaveToXML();
                        }
                        else
                        {
                            //Ako nije prosao Deadband, obrisi ga iz LD strukture.
                            LD1.ListHistoricalDesc.Remove(historicalDesc);
                        }
                        break;

                    case 2:
                        LD2.ListHistoricalDesc.Add(historicalDesc);
                        write = CheckDeadband(LD1, false);

                        if (write)
                        {
                            //Ako izlazi iz Deadband-a, onda upisi
                            SaveToXML();
                        }
                        else
                        {
                            //Ako nije prosao Deadband, obrisi ga iz LD strukture.
                            LD2.ListHistoricalDesc.Remove(historicalDesc);
                        }
                        break;

                    case 3:
                        LD3.ListHistoricalDesc.Add(historicalDesc);
                        write = CheckDeadband(LD1, false);

                        if (write)
                        {
                            //Ako izlazi iz Deadband-a, onda upisi
                            SaveToXML();
                        }
                        else
                        {
                            //Ako nije prosao Deadband, obrisi ga iz LD strukture.
                            LD3.ListHistoricalDesc.Remove(historicalDesc);
                        }
                        break;

                    case 4:
                        LD4.ListHistoricalDesc.Add(historicalDesc);
                        write = CheckDeadband(LD1, false);

                        if (write)
                        {
                            //Ako izlazi iz Deadband-a, onda upisi
                            SaveToXML();
                        }
                        else
                        {
                            //Ako nije prosao Deadband, obrisi ga iz LD strukture.
                            LD4.ListHistoricalDesc.Remove(historicalDesc);
                        }
                        break;
                }
            }            
        }

        /// <summary>
        /// Funkcija za snimanje LD struktura u XML fajl.
        /// </summary>
        private void SaveToXML()
        {
            switch (dataset)
            {
                case 1:
                    {
                        var serializer1 = new XmlSerializer(typeof(ListDescription));
                        using (var stream1 = File.Create(@"..\..\..\LD1.xml"))
                        {
                            serializer1.Serialize(stream1, LD1);
                        }
                    }
                    break;

                case 2:
                    {
                        var serializer2 = new XmlSerializer(typeof(ListDescription));
                        using (var stream2 = File.Create(@"..\..\..\LD2.xml"))
                        {
                            serializer2.Serialize(stream2, LD2);
                        }
                    }
                    break;

                case 3:
                    {
                        var serializer3 = new XmlSerializer(typeof(ListDescription));
                        using (var stream3 = File.Create(@"..\..\..\LD3.xml"))
                        {
                            serializer3.Serialize(stream3, LD3);
                        }
                    }
                    break;

                case 4:
                    {
                        var serializer4 = new XmlSerializer(typeof(ListDescription));
                        using (var stream4 = File.Create(@"..\..\..\LD4.xml"))
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
        /// <param name="LD"> LD koji se provjerava </param>
        /// <param name="manual"> parametar govori da li se radi o rucnom upisu </param>
        /// <returns></returns> 
        private bool CheckDeadband(ListDescription LD, bool manual)
        {
            if (!manual)
            {
                //Ukoliko LD struktura ima samo 1 HistoricalDescription, upisi ga, nema provjere Deadband-a
                if (LD.ListHistoricalDesc.Count == 1)
                {
                    return true;
                }
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
                            //izlazi id deadband-a
                            if (hp.HistoricalValue < (histProperty.HistoricalValue - (histProperty.HistoricalValue / 100) * 2) ||
                                hp.HistoricalValue > (histProperty.HistoricalValue + (histProperty.HistoricalValue / 100) * 2))
                            {
                                //Izlazi iz Deadband-a
                            }
                            else
                            {
                                if (hp.Time == histProperty.Time)
                                {
                                    //znaci da se radi o istom HistoricalProperty-u
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                        
                    }
                }
            }
            return true;
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
            LoadLD();       //deserijalizacija

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

        /// <summary>
        /// Funkcija vraca listu HistoricalProperty-a koji imaju Code jednak prosledjenom parametru.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private List<HistoricalProperty> GetChangesForCodeAnalogOrDigital(Codes code)
        {
            List<HistoricalProperty> returnList = new List<HistoricalProperty>();

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

        /// <summary>
        /// Funkcija vraca listu HistoricalProperty-a koji imaju Code jednak prosledjenom parametru.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private List<HistoricalProperty> GetChangesForCodeCustomOrLimitset(Codes code)
        {
            List<HistoricalProperty> returnList = new List<HistoricalProperty>();
            
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

        /// <summary>
        /// Funkcija vraca listu HistoricalProperty-a koji imaju Code jednak prosledjenom parametru.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private List<HistoricalProperty> GetChangesForCodeSinglenodeOrMultiplenode(Codes code)
        {
            List<HistoricalProperty> returnList = new List<HistoricalProperty>();

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

        /// <summary>
        /// Funkcija vraca listu HistoricalProperty-a koji imaju Code jednak prosledjenom parametru.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private List<HistoricalProperty> GetChangesForCodeConsumerOrSource(Codes code)
        {
            List<HistoricalProperty> returnList = new List<HistoricalProperty>();

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

        /// <summary>
        /// Rucni upis u History. Writer salje Code i Value unesen rucno, koji se upisuju u LD strukturu.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value"></param>
        public void ManualWriteToHistory(Codes code, float value)
        {
            LoadLD();       //deserijalizacija LD struktura

            //Temp
            tempProperty = new HistoricalProperty();
            historicalDesc = new HistoricalDescription();
            historicalProperties = new List<HistoricalProperty>();

            tempProperty.Code = code;
            tempProperty.HistoricalValue = value;
            tempProperty.Time = DateTime.Now;
            historicalProperties.Add(tempProperty);

            historicalDesc.ID = Guid.NewGuid().ToString();
            CheckDataset(code);
            historicalDesc.Dataset = dataset;
            historicalDesc.HistoricalProperties = historicalProperties;

            switch (dataset)
            {
                case 1:
                    LD1.ListHistoricalDesc.Add(historicalDesc);
                    write = CheckDeadband(LD1, true);
   
                    if (write)
                    {
                        //Ako izlazi iz Deadband-a, onda upisi
                        SaveToXML();
                    }
                    else
                    {
                        //Ako nije prosao Deadband, obrisi ga iz LD strukture.
                        LD1.ListHistoricalDesc.Remove(historicalDesc);
                    }
                    break;

                case 2:
                    LD2.ListHistoricalDesc.Add(historicalDesc);
                    write = CheckDeadband(LD2, true);
                    
                    
                    if (write)
                    {
                        //Ako izlazi iz Deadband-a, onda upisi
                        SaveToXML();
                    }
                    else
                    {
                        //Ako nije prosao Deadband, obrisi ga iz LD strukture.
                        LD2.ListHistoricalDesc.Remove(historicalDesc);
                    }
                    break;

                case 3:
                    LD3.ListHistoricalDesc.Add(historicalDesc);
                    write = CheckDeadband(LD3, true);

                    
                    if (write)
                    {
                        //Ako izlazi iz Deadband-a, onda upisi
                        SaveToXML();
                    }
                    else
                    {
                        //Ako nije prosao Deadband, obrisi ga iz LD strukture.
                        LD3.ListHistoricalDesc.Remove(historicalDesc);
                    }
                    break;

                case 4:
                    LD4.ListHistoricalDesc.Add(historicalDesc);
                    write = CheckDeadband(LD4, true);

                    if (write)
                    {
                        //Ako izlazi iz Deadband-a, onda upisi
                        SaveToXML();
                    }
                    else
                    {
                        //Ako nije prosao Deadband, obrisi ga iz LD strukture.
                        LD4.ListHistoricalDesc.Remove(historicalDesc);
                    }
                    break;
            }
        }

        /// <summary>
        /// Funkcija za deserijalizaciju ListDescription struktura.
        /// </summary>
        public void LoadLD()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ListDescription));

            try
            {
                TextReader reader1 = new StreamReader(@"..\..\..\LD1.xml");
                LD1 = (ListDescription)deserializer.Deserialize(reader1);
                reader1.Close();
            }
            catch { }

            try
            {
                TextReader reader2 = new StreamReader(@"..\..\..\LD2.xml");
                LD2 = (ListDescription)deserializer.Deserialize(reader2);
                reader2.Close();
            }
            catch { }

            try
            {
                TextReader reader3 = new StreamReader(@"..\..\..\LD3.xml");
                LD3 = (ListDescription)deserializer.Deserialize(reader3);
                reader3.Close();
            }
            catch { }

            try
            {
                TextReader reader4 = new StreamReader(@"..\..\..\LD4.xml");
                LD4 = (ListDescription)deserializer.Deserialize(reader4);
                reader4.Close();
            }
            catch { }
        }
    }
}
