using System;
using System.Globalization;
//task #5
namespace lab71
{
    abstract class Student
    {
        protected string _name;
        protected int _countMissed;

        public Student(string name, int countMissed)
        {
            _name = name;
            _countMissed = countMissed;
        }


        public int CountMissed => _countMissed;

        public virtual void WriteStudent()
        {
            Console.WriteLine($"{_name} {_countMissed}");
        }
    }

    class Math : Student
    {
        private int _markMath;

        public Math(string name, int markMath, int countMissed) : base(name, countMissed)
        {
            _markMath = markMath;
        }

        public override void WriteStudent() 
        {
            Console.WriteLine($"{_name} Mark Math: {_markMath} Count missed: {_countMissed}");
        }

        //public int MarkMath => _markMath;

        public bool NotPassedMath()
        {
            if (_markMath == 2)
                return true;
            else
                return false;
        }
    }

    class Informatics : Student
    {
        private int _markInf;

        public Informatics(string name, int markInf, int countMissed) : base(name, countMissed)
        {
            _markInf = markInf;
        }

        public override void WriteStudent()
        {
            Console.WriteLine($"{_name} Mark Informatics: {_markInf} Count missed: {_countMissed}");
        }

        //public int MarkInf => _markInf;

        public bool NotPassedInf()
        {
            if (_markInf == 2)
                return true;
            else
                return false;
        }
    }

    internal static class Program
    {
        static void Sort(Student[] array, int leftIndex, int rightIndex)
        {
            int i = leftIndex;
            int j = rightIndex;
            int pivot = array[rightIndex / 2].CountMissed;
            while (i <= j)
            {
                while (array[i].CountMissed > pivot)
                    i++;

                while (array[j].CountMissed < pivot)
                    j--;

                if (i <= j)
                {
                    (array[i], array[j]) = (array[j], array[i]);
                    i++;
                    j--;
                }
            }

            if (leftIndex < j)
                Sort(array, leftIndex, j);
            if (i < rightIndex)
                Sort(array, i, rightIndex);
        }

        static void Main()
        {
            //creating arrays of students
            Math[] math = new Math[5]
            {
                new Math("Bob", 2, 3),
                new Math("Alex", 3, 3),
                new Math("John", 4, 4),
                new Math("Mary", 2, 5),
                new Math("Katy", 2, 4)
            };

            Informatics[] inf = new Informatics[5]
            {
                new Informatics("Bob", 3, 1),
                new Informatics("Alex", 4, 2),
                new Informatics("John", 2, 0),
                new Informatics("Mary", 2, 2),
                new Informatics("Katy", 2, 4)
            };

            //counting not passed students & creating arrays of not passed students
            int count = 0;
            for (int i = 0; i < math.Length; i++)
                if (math[i].NotPassedMath())
                    count++;

            Math[] notPassedMath = new Math[count];
            int j = 0;
            for (int i = 0; i < math.Length; i++)
                if (math[i].NotPassedMath())
                {
                    notPassedMath[j] = math[i];
                    j++;
                }

            count = 0;
            for (int i = 0; i < inf.Length; i++)
                if (inf[i].NotPassedInf())
                    count++;

            Informatics[] notPassedInf = new Informatics[count];
            j = 0;
            for (int i = 0; i < inf.Length; i++)
                if (inf[i].NotPassedInf())
                {
                    notPassedInf[j] = inf[i];
                    j++;
                }


            //sorting arrays of not passed students
            //for (int i = 0; i < notPassedMath.Length - 1; i++)
            //    for (int k = 0; k < notPassedMath.Length - i - 1; k++)
            //        if (notPassedMath[k].CountMissed < notPassedMath[k + 1].CountMissed)
            //            (notPassedMath[k], notPassedMath[k + 1]) = (notPassedMath[k + 1], notPassedMath[k]);

            //for (int i = 0; i < notPassedInf.Length - 1; i++)
            //    for (int k = 0; k < notPassedInf.Length - i - 1; k++)
            //        if (notPassedInf[k].CountMissed < notPassedInf[k + 1].CountMissed)
            //            (notPassedInf[k], notPassedInf[k + 1]) = (notPassedInf[k + 1], notPassedInf[k]);

            Sort(notPassedMath, 0, notPassedMath.Length - 1);
            Sort(notPassedInf, 0, notPassedInf.Length - 1);

            //writing arrays of not passed students
            for (int i = 0; i < notPassedMath.Length; i++)
                notPassedMath[i].WriteStudent();

            for (int i = 0; i < notPassedInf.Length; i++)
                notPassedInf[i].WriteStudent();
        }
    }
}
