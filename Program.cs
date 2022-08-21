using System;
using System.IO;
using CommandLine;
using System.Security.Cryptography;
using fdf;
using fdf.Options;

namespace QuickStart
{
    class Program
    {

        static void Main(string[] args)
        {

            Parser.Default.ParseArguments<MainOptions>(args)
                   .WithParsed<MainOptions>(o =>
                   {
                       string _sourcePath = o.SourcePath.Trim();
                       string _targetPath = o.TargetPath.Trim();

                       string _copyDifferencePath = "";
                       if (o.CopyDifferencePath != null) _copyDifferencePath = o.CopyDifferencePath.Trim();

                       #region Validation source and target directory
                       try
                       {
                           if (!Directory.Exists(_sourcePath)) throw new DirectoryNotFoundException($"Folder does not exist: {_sourcePath}");
                           if (!Directory.Exists(_targetPath)) throw new DirectoryNotFoundException($"Folder does not exist: {_targetPath}");
                       }
                       catch (DirectoryNotFoundException dirExt)
                       {
                           Console.WriteLine(dirExt.Message);
                           Environment.Exit(0);
                       }
                       #endregion

                       #region Validation output directory
                       if (_copyDifferencePath.Length > 0 && !Directory.Exists(_copyDifferencePath))
                       {
                           Console.WriteLine($"Output directory not found ({_copyDifferencePath}). Create it? yes/no");
                           var answer = Console.ReadLine();

                           if (answer != null && answer.Equals("yes", StringComparison.CurrentCultureIgnoreCase))
                           {
                               var dirInfo = Directory.CreateDirectory(_copyDifferencePath);
                               Console.WriteLine($"New directory: {dirInfo.FullName}");
                           }
                           else
                           {
                               Console.WriteLine($"Sorry! I don't know where to put the result of the program execution.");
                               Environment.Exit(0);
                           }
                       }
                       #endregion

                       #region Get all paths 
                       Console.WriteLine($"Get all paths ->");
                       string[] sourcePath = Directory.GetFiles(_sourcePath, "*.*", SearchOption.AllDirectories);
                       string[] targetPath = Directory.GetFiles(_targetPath, "*.*", SearchOption.AllDirectories);
                       #endregion

                       #region Search difference
                       Console.WriteLine($"Search difference ->");
                       var foundFiles = new List<string>();
                       var filesNotFound = new List<string>();

                       var percentCount = sourcePath.Length/100;
                       var processCount = 0;

                       foreach (var srcFile in sourcePath)
                       {
                           var fFlag = false;
                           // TODO: Implement different types of search!
                           //    var srcFileHash = Helper.GetMD5Checksum(srcFile);
                           var srcFileInfo = new FileInfo(srcFile);

                           foreach (var trgFile in targetPath)
                           {
                            // TODO: Implement different types of search!
                            //    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            //    Console.Write("{0}\b\b\b", trgFile);
                               var trgFileInfo = new FileInfo(trgFile);

                               if (srcFileInfo.Name == trgFileInfo.Name && srcFileInfo.Length == trgFileInfo.Length) {
                                   fFlag = true;
                                   break;
                               }
                            // TODO: Implement different types of search!
                            //    var trgFileHash = Helper.GetMD5Checksum(trgFile);
                            //    if (trgFileHash.Equals(srcFileHash))
                            //    {
                            //        fFlag = true;
                            //        break;
                            //    }
                           }

                           if (fFlag)
                           {
                               foundFiles.Add(srcFile);
                           }
                           else
                           {
                               filesNotFound.Add(srcFile);
                           }

                           processCount++;
                           Console.SetCursorPosition(0, Console.CursorTop);
                           Console.Write("{0}%   ", processCount/percentCount);
                       }
                       #endregion

                       Console.WriteLine("Found files ------------------------------------------------------------");
                       foundFiles.ForEach(Console.WriteLine);

                       Console.WriteLine("  Files not found --------------------------------------------------------");
                       if (_copyDifferencePath.Length > 0 && Directory.Exists(_copyDifferencePath)) {
                        filesNotFound.ForEach(f => {
                            var cpFilePath = Path.Combine(_copyDifferencePath, Path.GetFileName(f));
                            File.Copy(f, cpFilePath, true);
                            Console.WriteLine($"{f} => {cpFilePath}");
                        });
                       } else {
                        filesNotFound.ForEach(Console.WriteLine);
                       }
                   });
        }
    }
}