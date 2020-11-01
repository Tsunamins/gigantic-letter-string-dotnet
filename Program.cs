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

        static void countCapitals(string text){
            Regex capitals = new Regex(@"(?-i)[A-Z]",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);
			MatchCollection capitalMatches = capitals.Matches(text);
			Console.WriteLine("\nTotal Capital Letters: {0}",
                          capitalMatches.Count);
        }

        static string addSpaces(string text){
            var findCapitals = 
    		Regex.Matches(text, @"([A-Z][a-z]+)")
			.Cast<Match>()
    		.Select(m => m.Value);
		    var textWithSpaces = string.Join(" ", findCapitals);
            return textWithSpaces;
        }

        static string[] changeToArray(string text){
		    string[] textArray = text.Split(' ');
            return textArray;
        }

        static void mostCommonWord(string[] textArray){
            Dictionary<string, int> countWords = new Dictionary<string, int>();
            for(int i = 0; i<textArray.Length; i++){
			    string word = textArray[i];
			    
			    if(countWords.ContainsKey(word)){
					countWords[word] = (int)countWords[word] + 1;
			    } else {
				    countWords[word] = 1;
			    }
            }
           
            var sortedDict = from entry in countWords orderby entry.Value descending select entry;
            var mostFrequent = sortedDict.First();
            string displayResult = "Most Common word is: " + mostFrequent.Key + " found: " + mostFrequent.Value +  " times.";
            Console.WriteLine(displayResult);
        }

        static void findWordsContaining(string[] textArray, string prefix){
            List<string> wordList = new List<string>();
             for(int i = 0; i<textArray.Length; i++){
                if(textArray[i].Contains(prefix) && textArray[i].Length > prefix.Length){
                    wordList.Add(textArray[i]);
                }
             }
            string wordsContaining = string.Join( ",", wordList);
            Console.WriteLine(wordsContaining);
        }

        static void checkPrefix(string[] textArray, int prefixSize){
           Dictionary<string, int> prefixDictionary = new Dictionary<string, int>(); 
		    for(int i = 0; i<textArray.Length; i++){
                string word = textArray[i];

                if(word.Length > prefixSize){             
			        string prefixLetters = textArray[i].Substring(0,prefixSize);
			
                    if(prefixDictionary.ContainsKey(prefixLetters)){
                        prefixDictionary[prefixLetters] = (int)prefixDictionary[prefixLetters] + 1;
                    } else {
                        prefixDictionary[prefixLetters] = 1;
                    }
                } 
            }

            var sortedDict = from entry in prefixDictionary orderby entry.Value descending select entry;
            var mostFrequent = sortedDict.First();
            string displayResult = "\nMost Common " + prefixSize + " letter prefix is: " + mostFrequent.Key + " found: " + mostFrequent.Value +  " times.";
            Console.WriteLine(displayResult);
            Console.WriteLine("In the following words: ");
            findWordsContaining(textArray, mostFrequent.Key); 
        }

        static void mostCommonComplex(string[] textArray){
            Console.WriteLine("\nMost common of prefixes, greater than 1: ");
            for(int i = 2; i < 8; i++){
                checkPrefix(textArray, i);
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

            //count all Capitals:
            countCapitals(text);

            //add spaces to text:
            string textWithSpaces = addSpaces(text);

            //change to Array:
            string[] textArray = changeToArray(textWithSpaces);

             //find most common word:
            mostCommonWord(textArray);

            //most common 2 letter prefix only:
            checkPrefix(textArray, 2);

            //the bonus:
            //prefixes greater than 1: 
            mostCommonComplex(textArray);

        }
    }
}

