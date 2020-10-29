using System;
using System.IO;
using System.Text;

namespace ringba_test
{
    class Program
    {
        static void Main(string[] args)
        {
            // var wc = new System.Net.WebClient();
            // string remoteUri = "http://ringba-test-html.s3-website-us-west-1.amazonaws.com/TestQuestions/";
            // string fileName = "output.txt", myStringWebResource = null;
            // myStringWebResource = remoteUri + fileName;
            // Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
            // wc.DownloadFile(myStringWebResource,fileName);		
            // Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
            // Console.WriteLine("\nDownloaded file saved in the following file system folder:\n\t" + "Application.StartupPath");

           // var fs = new FileStream("file.txt", FileMode.Open);
            string path = @"output.txt";
            using (FileStream fs = File.OpenRead(path))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b,0,b.Length) > 0)
                {
                    Console.WriteLine(temp.GetString(b));
                }
            }
    




        }
    }
}

