//-----------------------------------------------------------------------
// <copyright file="LoggerTest.cs" company="company">
//      Copyright (c) company. All rights reserved.
// </copyright>
// <author>zosterTeam</author>
// <email> rade.zekanovic@gmail.com </email>
// <email> lesansa00@gmail.com </email>
//-----------------------------------------------------------------------

using System;
using NUnit.Framework;
using RES;

namespace LoggerTest
{
    [TestFixture]
    public class LoggerTest
    {        
        [Test]
        [TestCase("class", "method", "logmessage")]
        public void DobarLog(string className, string method, string logMessage)
        {            
            Assert.DoesNotThrow(() =>
            {
                Logger.Log(className, method, logMessage);
            });                                   
        }

        [Test]
        [TestCase(null, "method", "logmessage")]
        [TestCase(null, null, "logmessage")]
        [TestCase(null, null, null)]
        public void ArgumentNullLog(string className, string method, string logMessage)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
            Logger.Log(className, method, logMessage);
            });            
        }

        [Test]
        public void DobarReadLog()
        {            
            Assert.DoesNotThrow(() =>
            {
                Logger.DumpLog();
            });            
        }        
    }
}
