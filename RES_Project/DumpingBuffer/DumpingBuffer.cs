using CommonLibrary;
using Historical_NS;
using MyGlobals;
using System;

namespace DumpingBuffer_NS
{
    public class DumpingBuffer : IDumpingBuffer
    {
        private CollectionDescription CD1 = new CollectionDescription();
        private CollectionDescription CD2 = new CollectionDescription();
        private CollectionDescription CD3 = new CollectionDescription();
        private CollectionDescription CD4 = new CollectionDescription();

        private DumpingProperty dumpingProperty = new DumpingProperty();
        private int dataset = 0;
        private bool updated = false;
           
        private Historical historical = Historical.Instance();     //Singleton pattern

        public DumpingBuffer()
        {
        }

        public void WriteToDumpingBuffer(Codes code, float value)
        {
            int tempCode = (int)code;
            if (tempCode < 0 || tempCode > 7)
            {
                throw new ArgumentException("Kod mora biti izmedju 0 i 7");
            }

            if (value < 0 || value > 10)
            {
                throw new ArgumentException("Vrednost mora biti izmedju 0 i 10");
            }

            if (tempCode < 0 || tempCode > 7)
            {
                throw new ArgumentException("Kod mora biti izmedju 0 i 7");
            }

            if (value < 0 || value > 10)
            {
                throw new ArgumentException("Vrednost mora biti izmedju 0 i 10");
            }

            updated = false;
            dumpingProperty = new DumpingProperty(code, value);

            dataset = CheckDataset(code);            
            CheckUpdate(dataset);       
            if (!updated)      
            {
                //Unique ID & DumpingProperty
                switch (dataset)                //na osnovu dataset-a odredjujemo u koji cemo CD da stavimo
                {
                    case 1:
                        CD1.ID = Guid.NewGuid().ToString();
                        CD1.Dataset = dataset;
                        CD1.dumpingPropertyCollection.DumpingCollection.Add(dumpingProperty);
                        break;
                    case 2:
                        CD2.ID = Guid.NewGuid().ToString();
                        CD2.Dataset = dataset;
                        CD2.dumpingPropertyCollection.DumpingCollection.Add(dumpingProperty);
                        break;
                    case 3:
                        CD3.ID = Guid.NewGuid().ToString();
                        CD3.Dataset = dataset;
                        CD3.dumpingPropertyCollection.DumpingCollection.Add(dumpingProperty);
                        break;

                    case 4:
                        CD4.ID = Guid.NewGuid().ToString();
                        CD4.Dataset = dataset;
                        CD4.dumpingPropertyCollection.DumpingCollection.Add(dumpingProperty);
                        break;
                }
            }

            //Ukoliko postoje 2 razlicita Code-a u okviru istog Dataset-a, salji Historical komponenti.
            if (CheckDumpingPropertyCount())
            {
                SendToHistorical();
            }
        }



        /// <summary>
        /// Funkcija provjerava da li u DumpingPropertyCollection postoje 2 DumpingProperty-a, odnosno da li postoje 2 razlicita koda
        /// Ukoliko posotoje, vrsi se slanje Historical komponenti.
        /// </summary>
        /// <returns></returns>
        private bool CheckDumpingPropertyCount()
        {
            switch (dataset)
            {
                case 1:
                    if (CD1.DumpingPropertyCollection.DumpingCollection.Count == 2)
                    {
                        return true;
                    }

                    return false;

                case 2:
                    if (CD2.DumpingPropertyCollection.DumpingCollection.Count == 2)
                    {
                        return true;
                    }

                    return false;

                case 3:
                    if (CD3.DumpingPropertyCollection.DumpingCollection.Count == 2)
                    {
                        return true;
                    }

                    return false;

                case 4:
                    if (CD4.DumpingPropertyCollection.DumpingCollection.Count == 2)
                    {
                        return true;
                    }

                    return false;
                default:
                    return false;
            }
        }


        /// <summary>
        /// Funkcija na osovu vrijednosti Dataset-a, salje Hisctorial komponenti onaj CD koji odogovara tom Dataset-u.
        /// </summary>
        private void SendToHistorical()
        {
            switch (dataset)
            {
                case 1:
                    historical.WriteToHistory(CD1);
                    CD1.DumpingPropertyCollection.DumpingCollection.Clear();
                    break;
                case 2:
                    historical.WriteToHistory(CD2);
                    CD2.DumpingPropertyCollection.DumpingCollection.Clear();
                    break;
                case 3:
                    historical.WriteToHistory(CD3);
                    CD3.DumpingPropertyCollection.DumpingCollection.Clear();
                    break;
                case 4:
                    historical.WriteToHistory(CD4);
                    CD4.DumpingPropertyCollection.DumpingCollection.Clear();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Funkcija koja osvjezava Value ukoliko se dati Code vec nalazi u CollectionDescription
        /// Ukoliko dodje do izmjene, promjenice "updated" na TRUE.
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public void CheckUpdate(int dataset)
        {
            if (dataset < 1 || dataset > 4)
            {
                throw new ArgumentException("Invalid dataset");
            }

            switch (dataset)
            {
                case 1:
                    foreach (DumpingProperty dp in CD1.DumpingPropertyCollection.DumpingCollection)
                    {
                        if (dp.Code == dumpingProperty.Code)
                        {
                            dp.DumpingValue = dumpingProperty.DumpingValue;
                            updated = true;
                        }
                    }
                    break;
                case 2:
                    foreach (DumpingProperty dp in CD2.DumpingPropertyCollection.DumpingCollection)
                    {
                        if (dp.Code == dumpingProperty.Code)
                        {
                            dp.DumpingValue = dumpingProperty.DumpingValue;
                            updated = true;
                        }
                    }
                    break;
                case 3:
                    foreach (DumpingProperty dp in CD3.DumpingPropertyCollection.DumpingCollection)
                    {
                        if (dp.Code == dumpingProperty.Code)
                        {
                            dp.DumpingValue = dumpingProperty.DumpingValue;
                            updated = true;
                        }
                    }
                    break;
                case 4:
                    foreach (DumpingProperty dp in CD4.DumpingPropertyCollection.DumpingCollection)
                    {
                        if (dp.Code == dumpingProperty.Code)
                        {
                            dp.DumpingValue = dumpingProperty.DumpingValue;
                            updated = true;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// Funkcija koja dodjeljuje Dataset na osnovu Code podataka koji su primljeni 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int CheckDataset(Codes code)
        {
            int tempCode = (int)code;
            if (tempCode < 0 || tempCode > 7)
            {
                throw new ArgumentException("Kod mora biti izmedju 0 i 7");
            }

            //Dodjeljivanje DataSet-a
            if (code == Codes.CODE_ANALOG || code == Codes.CODE_DIGITAL)
            {
                dataset = 1;
            }
            else if (code == Codes.CODE_CUSTOM || code == Codes.CODE_LIMITSET)
            {
                dataset = 2;
            }
            else if (code == Codes.CODE_SINGLENODE || code == Codes.CODE_MULTIPLENODE)
            {
                dataset = 3;
            }
            else if (code == Codes.CODE_CONSUMER || code == Codes.CODE_SOURCE)
            {
                dataset = 4;
            }

            return dataset;
        }
    }
}
