using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;



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

        static string getText()
        {
            string text = "";
            string pathSource = @"output.txt"; 
            using (FileStream fsSource = new FileStream(pathSource,
            FileMode.Open, FileAccess.Read))
            { 
                byte[] bytes = new byte[fsSource.Length];
                int numBytesToRead = (int)fsSource.Length;
                int numBytesRead = 0;
                UTF8Encoding temp = new UTF8Encoding(true);
                while (numBytesToRead > 0)
                {
                    int n = fsSource.Read(bytes, numBytesRead, numBytesToRead); 
                    if (n == 0)
                    break;
                    text = temp.GetString(bytes);
                    numBytesRead += n;
                    numBytesToRead -= n;
                } 
            return text;
            }
        }

        static void countAlphaChars(string text){
            
            char[] alphaUpper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
		    char[] alphaLower = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

            for (int i = 0; i < alphaUpper.Length; i++) 
		    {
		  	    char uc = alphaUpper[i];
			    char lc = alphaLower[i];
			    string regMatch = lc + "|" + uc;
			    Regex letterCount = new Regex(@regMatch,
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);
			    MatchCollection letterMatches = letterCount.Matches(text);
			    Console.Write(" Total {1}|{2}: {0} ",
                          letterMatches.Count,
                          lc, uc);
            }
        }
        static void Main(string[] args)
        {
            //download file:
            downloadFile();

            //read file:
            string text = getText();

            //count e alpha character:
            countAlphaChars(text);





        }
    }
}

