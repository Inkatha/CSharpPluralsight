using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MoveDirectories
{
    class Program
    {
        static void Main(string[] args)
        {
            DropVendorUploadIntoHotfolder();
        }

        public static void DropVendorUploadIntoHotfolder()
        {
            while (true)
            {
                string directoryName = @"C:\dev\DirectoryMover";
                DirectoryInfo dirInfo = new DirectoryInfo(directoryName);

                if (dirInfo.Exists == false)
                {
                    Directory.CreateDirectory(directoryName);
                }

                List<string> testFiles =
                    Directory.GetFiles(@"C:\dev\Testing", "*.*", SearchOption.AllDirectories).ToList();

                GatherFilesFromFolder(testFiles, dirInfo);
                
                System.Threading.Thread.Sleep(4000);
            }
        }

        public static void MoveToFolder(string file, DirectoryInfo directoryInfo)
        {
            FileInfo mFile = new FileInfo(file);

            if (new FileInfo(directoryInfo + "\\" + mFile.Name).Exists == false)
            {
                mFile.MoveTo(directoryInfo + "\\" + mFile.Name);
            }
        }

        public static void GatherFilesFromFolder(List<string> files, DirectoryInfo dirInfo)
        {
            if (files.Count > 0)
            {
                string articleFileName = Path.GetFileNameWithoutExtension(files[0]); ;
                const int articleNameLength = 7;

                if (articleFileName != null && articleFileName.Contains("Article"))
                {
                    int index = articleFileName.IndexOf("Article", StringComparison.Ordinal);
                    if (index > -1)
                    {
                        string manufacturerNumber = articleFileName.Substring(articleNameLength);

                        foreach (string file in files)
                        {
                            if (file.Contains(manufacturerNumber))
                            {
                                MoveToFolder(file, dirInfo);
                            }
                        }
                    }
                }
            }
        }
    }
}
