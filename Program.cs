using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;



namespace ringba_test
{
    class Program

        
    {
        static void downloadFile(){
            var wc = new System.Net.WebClient();
            string remoteUri = "http://ringba-test-html.s3-website-us-west-1.amazonaws.com/TestQuestions/";
            string fileName = "output.txt", myStringWebResource = null;
            myStringWebResource = remoteUri + fileName;
            Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
            wc.DownloadFile(myStringWebResource,fileName);		
            Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
            Console.WriteLine("\nDownloaded file saved in the following file system folder:\n\t" + "Application.StartupPath");

        }
        static void Main(string[] args)
        {
            //download file:
            downloadFile();

            



        }
    }
}

