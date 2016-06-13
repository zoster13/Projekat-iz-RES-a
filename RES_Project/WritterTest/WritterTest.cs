//-----------------------------------------------------------------------
// <copyright file="WritterTest.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

using System;
using NUnit.Framework;
using RES;

namespace WritterTest
{
    [TestFixture]
    public class WritterTest
    {        
        [Test]
        public void WritterKonstruktor()
        {
            Writter w = new Writter();
            Assert.AreNotEqual(w, null);
        }

        [Test]
        [TestCase((float)1.3, 1)]
        [TestCase((float)5.2, 5)]
        [TestCase((float)9.3, 7)]
        [TestCase((float)0.3, 4)]
        [TestCase((float)9.9, 2)]
        public void WritterKonstruktorDobar(float value, int code)
        {                       
            Writter w = new Writter((Codes)code, value);
            Assert.AreEqual(w.Value, value);
            Assert.AreEqual(w.Code, (Codes)code);           
        }

        [Test]
        [TestCase((float)11, 1)]
        [TestCase((float)5.2, 15)]
        [TestCase((float)-9.3, 27)]
        [TestCase((float)0.3, 54)]
        [TestCase((float)19.9, 2)]
        public void WritterKonstruktorLos(float value, int code)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Writter w = new Writter((Codes)code, value);
            });
        }

        [Test]
        [TestCase((float)10.3)]
        [TestCase((float)11.23)]
        [TestCase((float)-0.3)]
        [TestCase((float)-51.3)]
        public void WritterKonstruktorLosValue(float val)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Codes code = Codes.CODE_ANALOG;
                Writter w = new Writter(code, val);
            });
        }

        [Test]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(-2)]
        [TestCase(11)]
        public void WritterKonstruktorLosCode(int code)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                float val = 5;
                Writter w = new Writter((Codes)code, val);
            });
        }
    }
}
