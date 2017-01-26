using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/// <summary>
///  DataSource Class
///  
///  This class reads two .txt files, their locations are hard coded 
///  in the ReadMenu and ReadMods methods, but the fileNames are passed
///  in as variables.
///  The menu and modifiers lists are private but can be
///  accessed via the getMods and getMenu methods.
/// </summary>

namespace POS_102
{
    public class DataSource
    {
        private string _fileName;
        //private string pathSource = @"c:\tests\source.txt";
        //private string pathNew = @"";

        private List<MenuItem> _menu;
        private List<Modifier> _mods;

        public DataSource(string menuFile, string modsFile)
        {
            _mods = new List<Modifier>();
            _menu = new List<MenuItem>();
            _fileName = menuFile;
            ReadMenu();
            _fileName = modsFile;
            ReadMods();
            
        }

        // unsuccessful ReadMenuMethod
        //public byte[] ReadTextIn()
        //{
        //    byte[] emptyBytes = new byte[10];
        //    try {
        //        using (FileStream fsSource = new FileStream(pathSource,
        //            FileMode.Open, FileAccess.Read)) {

        //            // Read the source file into byte array
        //            byte[] bytes = new byte[fsSource.Length];
        //            int numBytesToRead = (int)fsSource.Length;
        //            int numBytesRead = 0;

        //            while (numBytesToRead > 0) {

        //                // Read may return anything from 0 to numBytesToRead
        //                int n = fsSource.Read(bytes, numBytesRead, numBytesToRead);

        //                // break when end of the file is reached
        //                if (n == 0)
        //                    break;

        //                numBytesRead += n;
        //                numBytesToRead -= n;
        //            }
        //            // writes the byte array to another new fileStream
        //            //numBytesToRead = bytes.Length;

        //            //using (FileStream fsNew = new FileStream(pathNew,
        //            //    FileMode.Create, FileAccess.Write)) {
        //            //    fsNew.Write(bytes, 0, numBytesToRead);
        //            //}

        //            return bytes;

        //        } // end using fsSource
        //    }// end try
        //    catch (Exception e){
        //        Console.WriteLine(e.Message);
        //    }// end catch
        //    return emptyBytes;
        //}// end ReadTextIn

        //public string[] badReadMenu(object sender, RoutedEventArgs e)
        //{
        //    try {
        //        // note! must add asynchronus to ReadMenu above
        //        //using (StreamReader reader = new StreamReader(_fileName)) {
        //        //    String line = await reader.ReadToEndAsync();
        //        //    var result = line.Split(',');
        //        //    //ResultBlock.Text = line;
        //        //    foreach (var rst in result) {
        //        //        Console.WriteLine(result);
        //        //    }
        //        //}

        //        string[] lines = System.IO.File.ReadAllLines(@"path" + _fileName);
        //        Console.WriteLine("Contents of file: ");
        //        foreach (string line in lines) {
        //            Console.WriteLine("\t" + line);
        //            var result = line.Split(',');
        //            foreach (var r in result) {
        //                Console.WriteLine("r: " + r);
        //            }
        //        }
        //        return lines;

        //    } // end try
        //    catch (Exception ex) {
        //        Console.WriteLine("Error at DataSource.ReadMenu");
        //        Console.WriteLine(ex.Message);
        //        string[] strError = { "Error at ReadMenu" };
        //        return strError;
        //    }
        //}

            /// <summary>
            /// reads menu data from file
            /// location hard coded, file name variable
            /// </summary>
        public void ReadMenu()
        {
            try {

                string[] lines = System.IO.File.ReadAllLines(@"C:\Users\owner\Documents\A_WinProg\POS-102\POS-102\" + _fileName);
                //Console.WriteLine("Contents of file: ");
                foreach (string line in lines) {
                   //Console.WriteLine("\t" + line);
                    var result = line.Split(',');
                    //Console.WriteLine("r: " + r); // debugging code
                    _menu.Add(new MenuItem(result[0], Convert.ToInt32(result[1]), Convert.ToDecimal(result[2])));
                }
            } // end try
            catch (Exception ex) {
                Console.WriteLine("Error at DataSource.ReadMenu");
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// reads modifier data from file
        /// location hard coded, file name variable
        /// </summary>
        public void ReadMods()
        {
            try {
                string[] lines = System.IO.File.ReadAllLines(@"C:\Users\owner\Documents\A_WinProg\POS-102\POS-102\" + _fileName);
                //Console.WriteLine("Contents of file: ");
                foreach (string line in lines) {
                    //Console.WriteLine("\t" + line);
                    var result = line.Split(',');
                    //Console.WriteLine("r: " + r); // write a foreach (r in result) for this
                    _mods.Add(new Modifier(result[0], Convert.ToInt32(result[1])));
                }
            } // end try
            catch (Exception ex) {
                Console.WriteLine("Error at DataSource.ReadMods");
                Console.WriteLine(ex.Message);
            }



        }


        public List<MenuItem> getMenu()
        {
            return _menu;
        }

        public List<Modifier> getMods()
        {
            return _mods;
        }



    }
}
