using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace fcmd.http
{
    /// <summary>
    /// A client for File Transfer Protocol
    /// </summary>
    public class WebClient
    {
        public class WebException : Exception
        {
            public WebException(string Message, Exception InnerException) : base(Message, InnerException) { }
        }

        //public int ResponseCode;
        //public string Response;

        public WebClient(string Server, int Port = 80)
        {
            // IPAddress addr;
            // https://msdn.microsoft.com/en-us/library/system.net.ftpwebrequest.enablessl.aspx

            //Console.WriteLine(@"FTP: FTP connection estabilished " + Server);
        }


        public void ReadResponse()
        {

        }

        ~WebClient()
        {
            //if (CommandSocket != null)
            //if (CommandSocket.Connected)
            //    CommandSocket.Close();
        }
    }
}
