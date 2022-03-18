using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Lesson_writer_reader;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Lesson_writer_reader
{
    public class MainForWriterReader
    {
        public static void Main()
        {
            var taskCount = Environment.ProcessorCount;
            Console.WriteLine("Колличество потоков процессора: " + taskCount);
            Console.WriteLine("Укажите путь к создаваемым файлам");
            //var path = Console.ReadLine();
            var path = @"C:\SomeDir";
            Console.WriteLine("Укажите количество создаваемых файлов");
            var filesCount = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Укажите количество повторяющихся строк");
            var numberOfStringsMain = Int32.Parse(Console.ReadLine());

            Stopwatch sw = Stopwatch.StartNew();

            Writer write = new Writer(path, filesCount);
            write.Run(numberOfStringsMain);

            Task[] startThreads = new Task[taskCount];
            List<Task> listOfTasks = new List<Task>();
            int fileForTask = filesCount / taskCount;


            for (int i = 1; i - 1 < taskCount; i++)
            {
                int begin = (i - 1) * fileForTask;
                int end = i * fileForTask;
                int endNew = end + (filesCount % i);

                if (filesCount % i != 0)
                {
                    if (i == taskCount)
                    {
                        var newTask = Task.Run(() =>
                        {
                            StartReader(begin, endNew, path);
                        });
                        listOfTasks.Add(newTask);


                    }
                    else
                    {
                        var newTask = Task.Run(() =>
                       {
                           StartReader(begin, end, path);
                       });
                        listOfTasks.Add(newTask);

                    }
                }
                else
                {
                    var newTask = Task.Run(() =>
                    {
                        StartReader(begin, end, path);
                    });
                    listOfTasks.Add(newTask);

                }
                Task.WaitAll(listOfTasks[i - 1]);
                /*
                var newTask = Task.Run(() =>
                {
                    StartReader(begin, end, path);
                });
                listOfTasks.Add(newTask);
                Task.WaitAll(listOfTasks[i]);
            */
            }

            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalSeconds);


        }

        private static void StartReader(int begin, int end, string path)
        {
            for (int id = begin; id < end; id++)
            {
                Console.WriteLine("id: " + id);
                Reader read = new Reader(path, id);
                read.Run();
            }
;

        }
    }
}
