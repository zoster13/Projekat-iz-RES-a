//-----------------------------------------------------------------------
// <copyright file="DumpingBufferTest.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

using DumpingBuffer_NS;
using NUnit.Framework;
using System;

namespace DumpingBufferTest
{
    [TestFixture]
    public class DumpingBufferTest
    {
        DumpingBuffer db;
        [SetUp]
        public void SetUp()
        {
            db = new DumpingBuffer();
        }

        [Test]
        [TestCase(2, (float)5.21)]
        [TestCase(5, (float)2.23)]
        [TestCase(3, (float)7.8)]
        [TestCase(7, (float)1.12)]
        public void WriteToDumpingBuffer_DobarTest(int code,float value)
        {
            Assert.DoesNotThrow(() =>
            {
                db.WriteToDumpingBuffer((Codes)code, value);
            });
        }

        [Test]
        [TestCase(11,(float)15.2)]
        [TestCase(9, (float)-3)]
        [TestCase(-1, (float)10.2)]
        [TestCase(8, (float)-0.2)]
        public void WriteToDumpingBuffer_LosTest(int code, float value)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                db.WriteToDumpingBuffer((Codes)code, value);
            });
        }

        [Test]
        [TestCase(3)]
        [TestCase(2)]
        [TestCase(1)]
        [TestCase(4)]
        public void CheckUpdate_Dobar(int dataset)
        {
            Assert.DoesNotThrow(() =>
            {
                db.CheckUpdate(dataset);
            });
        }

        [Test]
        [TestCase(7)]
        [TestCase(-1)]
        [TestCase(5)]
        [TestCase(0)]
        public void CheckUpdate_Los(int dataset)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                db.CheckUpdate(dataset);
            });
        }

        [Test]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(7)]
        public void CheckDataset_Dobar(int code)
        {
            int ds;
            Assert.DoesNotThrow(() =>
            {
                db.CheckDataset((Codes)code);
            });

            ds = db.CheckDataset((Codes)code);
            if (code == 0 || code == 1)
            {
                Assert.AreEqual(ds, 1);
            }
            else if (code == 2 || code == 3)
            {
                Assert.AreEqual(ds, 2);
            }
            else if (code == 4 || code == 5)
            {
                Assert.AreEqual(ds, 3);
            }
            else
            {
                Assert.AreEqual(ds, 4);
            }
        }

        [Test]
        [TestCase(8)]
        [TestCase(-5)]
        [TestCase(11)]
        [TestCase(13)]
        [TestCase(-1)]
        public void CheckDataset_Los(int code)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                db.CheckDataset((Codes)code);
            });
        }
    }
}
