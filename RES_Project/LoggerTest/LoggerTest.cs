//-----------------------------------------------------------------------
// <copyright file="LoggerTest.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

using NUnit.Framework;
using RES;
using System;
using System.IO;

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
