using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_writer_reader
{
    internal class Reader
    {
        private string? _path;
        private int _id;
        List<string> textFromFiles = new List<string>();

        public Reader(string? path, int id)
        {
            _id = id;
            _path = path;
        }

        internal void Run()
        {
            using (StreamReader reader = new StreamReader(@$"{_path}\{_id}.txt"))
            {
                var t = reader.ReadToEnd();
                textFromFiles.Add(t);
                reader.Close();
            }

        }

        public void WriteToConsole()
        {
            foreach (var text in textFromFiles)
            {
                Console.WriteLine(text);
            }
        }




    }
}
