using kw2;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace kw2
{
    abstract class Task
    {
        protected string _text;
        public string Text
        {
            get => _text;
            protected set => _text = value;
        }
        public Task(string text)
        {
            this._text = text;
        }
    }

    class Task1 : Task
    {
        [JsonConstructor]
        public Task1(string text)
        {
            text = text.Replace('!', '.');
            text = text.Replace('?', '.');
            string[] subtexts = text.Split('.');
            int[] countwords = new int[subtexts.Length];
            for (int i = 0; i < subtexts.Length; i++)
            {
                string[] words = subtexts[i].Split(' ');
                countwords[i] = words.Length;
            }

            int imax = 0;
            int maxwords = countwords[0];
            for (int i = 1; i < countwords.Length; i++)
            {
                if (countwords[i] > maxwords)
                {
                    imax = i;
                    maxwords = countwords[i];
                }
            }

            _text = subtexts[imax];
        }

        public override string ToString()
        {
            return this._text;
        }
    }

    class Task2 : Task
    {
        private string[] centralwords;
        [JsonConstructor]
        public Task2(string text)
        {
            text = text.Replace('!', '.');
            text = text.Replace('?', '.');
            string[] subtexts = text.Split('.');
            string[] centralwordsarr = new string[subtexts.Length];
            for (int i = 0; i < subtexts.Length; i++)
            {
                string[] words = subtexts[i].Split(' ');
                centralwordsarr[i] = words[words.Length / 2];
            }
            centralwords = centralwordsarr;
        }

        public string[] Central()
        {
            return centralwords;
        }
    }

    class JsonIO
    {
        public static void Write<T>(T objct, string filepath)
        {
            using (FileStream file = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(file, objct);
            }
        }
        public static T Read<T>(string filepath)
        {
            using (FileStream file = new FileStream(filepath, FileMode.OpenOrCreate))
            {
                return JsonSerializer.Deserialize<T>(file);
            }
            return default(T);
        }
    }
}

internal static class Program
{
    static void Main()
    {
        Task[] tasks = {
            new Task1("abc, sdasas. asdasd, qweqweqweqwe, rt."),
            new Task2("abc, sdasas. asdasd, qweqweqweqwe, rt.")
        };
        Console.WriteLine(tasks[0]);
        Console.WriteLine(tasks[1]);

        //task 3
        string path = "C:/Users/m2310108/Desktop";
        string foldername = "Control work";
        path = Path.Combine(path, foldername);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string filename1 = "cw2_1.json";
        string filename2 = "cw2_2.json";

        filename1 = Path.Combine(path, filename1);
        filename2 = Path.Combine(path, filename2);

        if (!File.Exists(filename1))
        {
            var file1 = File.Create(filename1);
            file1.Close();
        }

        if (!File.Exists(filename2))
        {
            var file2 = File.Create(filename2);
            file2.Close();
        }

        //task 4
        if (!File.Exists(filename2))
        {
            JsonIO.Write<Task1>(tasks[0] as Task1, filename1);
        }
        else
        {
            var text1 = JsonIO.Read<Task1>(filename1);
            var text2 = JsonIO.Read<Task2>(filename2);
            Console.WriteLine(text1);
            Console.WriteLine(text2);
        }
    }
}