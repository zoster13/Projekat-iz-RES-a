//-----------------------------------------------------------------------
// <copyright file="ReaderTest.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

using System;
using NUnit.Framework;
using RES;

namespace ReaderTest
{
    [TestFixture]
    public class ReaderTest
    {
        [Test]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(0)]
        public void Reader_Konstruktor_Dobar(int code)
        {
            Reader r = new Reader((Codes)code);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(8)]
        [TestCase(17)]
        [TestCase(120)]
        public void Reader_Konstruktor_Los(int code)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Reader r = new Reader((Codes)code);
            });
        }

        [Test]
        public void Main_Dobar()
        {
        }
    }
}
