using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using fcmd.ftps;

namespace fcmd.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Ftp_SSL_ReadDir()
        {
            var ssl = new FtpSecure();
            // ssl.Connect()
            ssl.CurrentDirectory = "ftp://localhost";

            var list = ssl.DirectoryContent;
        }
    }
}
