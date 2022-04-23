using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibrary.StringFormatHelpers
{
    public class FileParser : IFileParser
    {
        public FileParser()
        {

        }
        public IEnumerable<string> ParseFile(string path)
        {
            var listOfData = new List<string>();
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                listOfData.Add(line);
            }
            return listOfData;
        }

    }
}
