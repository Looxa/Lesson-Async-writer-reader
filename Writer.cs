using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson_writer_reader
{
    internal class Writer
    {
        private string? _path;
        private int _amount;

        public Writer(string? path, int amount)
        {
            _path = path;
            _amount = amount;
        }

        internal void Run(int numberOfStrings)
        {
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
            for (int i = 0; i < _amount; i++)
            {
                var pathForWriter = @$"{_path}\{i}.txt";
                using (FileStream newFile = File.Create(pathForWriter))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("Some fun joke. \n");
                    for (int j = 0; j <= numberOfStrings; j++)
                    {
                        newFile.Write(info);
                    }
                    newFile.Close();

                }
            }
        }
    }

}
