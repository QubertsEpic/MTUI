using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTUI.Classes.Data
{
    public class Log
    {
        public StreamWriter writer;
        public string FileLocation;
        public FileStream FileStream;
        public Log(string fileLocation = null)
        {
            if (fileLocation == null)
            {
                fileLocation = Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MTUILog\\");
                if (!Directory.Exists(fileLocation))
                    Directory.CreateDirectory(fileLocation);
                fileLocation = Path.GetFullPath(fileLocation + "\\Log.txt");
            }
            FileStream stream = File.Create(fileLocation);
            writer = new StreamWriter(stream);
        }

        public bool Write(string message)
        {
            if (writer == null)
                return false;
            writer.WriteLine(message);
            writer.Flush();
            return true;
        }

    }
}
