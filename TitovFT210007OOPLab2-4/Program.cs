using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Services;
using GeometricFigures;

namespace TitovFT210007OOPLab2_4
{
    //расширение включающее сортировку
    static class ArrayExtensions
    {
        public static void Swap(this Array arr, int i, int j)
        {
            object temp = arr.GetValue(i);
            arr.SetValue(arr.GetValue(j), i);
            arr.SetValue(temp, j);
        }
        public static void BubbleSort(this Array arr, IComparer comparer)
        {
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    var element1 = arr.GetValue(j);
                    var element0 = arr.GetValue(j - 1);
                    if (comparer.Compare(element1, element0) > 0)
                    {
                        arr.Swap(j - 1, j);
                    }
                }
            }
        }

    } 
    //признак сравнения по площади
    class SComparer : IComparer
    {
        public double FigureS(Figure f)
        {
            return f.S;
        }

        public int Compare(object x, object y)
        {
            return FigureS((Figure)x).CompareTo(FigureS((Figure)y));
        }
    }
    //признак сравнени по периметру
    class PComparer : IComparer
    {
        public double FigureP(Figure f)
        {
            return f.P;
        }

        public int Compare(object x, object y)
        {
            return FigureP((Figure)x).CompareTo(FigureP((Figure)y));
        }
    }

    internal class Program
    {
        //инициализаци треугольника
        public static Triange TriangeInit()
        {
            Point[] sides = new Point[3];

            for (int i = 0; i < sides.Length; i++)
            {
                Console.WriteLine("Enter x point of vertex number " + ((int)i+1));
                int x = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter y point of vertex: ");
                int y = int.Parse(Console.ReadLine());
                sides[i] = new Point(x, y);
            }

            return new Triange(sides[0], sides[1], sides[2]);
        }

        //инициализаци четырехугольника
        public static Square SquareInit()
        {
            Point[] sides = new Point[4];

            for (int i = 0; i < sides.Length; i++)
            {
                Console.WriteLine("Enter x point of vertex number " + ((int)i + 1));
                int x = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter y point of vertex number " + ((int)i + 1));
                int y = int.Parse(Console.ReadLine());
                sides[i] = new Point(x, y);
            }

            return new Square(sides[0], sides[1], sides[2], sides[3]);
        }

        //инициализаци окружности
        public static Сircle CircleInit()
        {
            Console.WriteLine("Enter x point of center: ");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter y point of center: ");
            int y = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter radius: ");
            int rad = int.Parse(Console.ReadLine());

            Point center = new Point(x, y);

            return new Сircle(center, rad);
        }






        static void Main(string[] args)
        {
            int b = 1;
            List<Figure> figures = new List<Figure>();

            while (b == 1) 
            {
                Console.WriteLine("Enter the type of figure");
                Console.WriteLine("0 - Triangle, 1 - Square, 2 - Circle");
                int checkF = int.Parse(Console.ReadLine());
                if (checkF == 0)
                {
                    figures.Add(TriangeInit());
                }
                else if(checkF == 1)
                {
                    figures.Add(SquareInit());
                }
                else if (checkF == 2)
                {
                    figures.Add(CircleInit());
                }
                else
                {
                    Console.WriteLine("Wrong type!");
                }

                Console.WriteLine("Enter 0 to exit/ 1 to continue");
                b = int.Parse(Console.ReadLine());
            }

            Figure[] res = figures.ToArray();

            Console.WriteLine("Not sorted: ");

            for (int i = 0; i < res.Length; i++)
            {
                Console.WriteLine("{0}, S = {1}, P = {2}", res[i].type, res[i].S, res[i].P);
            }

            Console.WriteLine();
            Console.WriteLine("Sorted by perimetr: ");

            res.BubbleSort(new PComparer());
            for (int i = 0; i < res.Length; i++)
            {
                Console.WriteLine("{0}, S = {1}, P = {2}", res[i].type, res[i].S, res[i].P);
            }

            Console.WriteLine();
            Console.WriteLine("Sorted by square: ");

            res.BubbleSort(new SComparer());

            for (int i = 0; i < res.Length; i++)
            {
                Console.WriteLine("{0}, S = {1}, P = {2}", res[i].type, res[i].S, res[i].P);
            }

        }
    }
}
