using System;
using System.IO;

namespace RenameFiles {

    class Program {

        static void Main(string[] args) {

            #region Process arguments
            if(args.Length < 3 || args.Length > 4) {
                Console.WriteLine("Error: Invalid number of arguments! Must be 3 or 4.");
                ShowAbout();
                return;
            }

            string currentFilePath = args[0];
            string newFilePath = args[1];
            int startNumber = int.Parse(args[2]);

            int increment = 1;
            if(args.Length == 4) {
                if(args[3] == "-reverse")
                    increment = -1;
            }
            #endregion

            string[] fileList = Directory.GetFiles(currentFilePath);
            int fileCount = fileList.Length;
            string newName;

            foreach(string f in fileList) {
                newName = AddZeros(fileCount, startNumber) + Path.GetExtension(f);
                Console.WriteLine("{0} -> {1}", f, newName);
                File.Copy(f, Path.Combine(newFilePath, newName));
                startNumber = startNumber + increment;
            }

        }

        private static string AddZeros(int fileCount, int startNumber) {
            int numberOfZeros = (int)Math.Floor(Math.Log10(fileCount)+1);

            return startNumber.ToString().PadLeft(numberOfZeros, '0');
        }

        private static void ShowAbout() {
            Console.WriteLine(@"Example: RenameFiles.exe ..\Files ..\Renamed 1 [-reverse]");
            Console.WriteLine("(c) Irbis");
        }

    }
}
