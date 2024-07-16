using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Testovoe_Zadanie__TereshkinDA
{
    /*Задание 2. Напишите вспомогательный класс PaginationHelper, инициализирующийся массивом данных и количеством элементов на 
каждой странице, который должен уметь:
• возвращать количество элементов массива данных
• возвращать количество доступных страниц
• возвращать количество элементов на указанной странице (нумерация страницы с нуля)
• возвращать индекс страницы, где расположен элемент массива по переданному индексу массив*/
    public class PaginationHelper<T>// Создаем вспомогательный класс
    {
        private List<T> collection;// Создаем массив классе
        private int itemsPerPage;// создаем количество элементов на одной странице
        public PaginationHelper(List<T> collection, int itemsPerPage)// Создаем для класса вводные данные для подключение к массиву и номеру страницы  вне класс
        {
            this.collection = collection;
            this.itemsPerPage = itemsPerPage;
        }
        public int ItemCount// создаем метод по выяснению количества элементов в массиве
        {
            get { return collection.Count; } // возвращаем значение длинны массива
        }
        public int PageCount// создаем метод с перечисляющий страницы в массиве
        {
            get { return (int)Math.Ceiling((double)collection.Count / itemsPerPage); } // Вычислление количества страниц массива с округлением в большую сторону с помощью Math.Celling
        }
        public int PageItemCount(int pageIndex) // Создаем метод по опредлению количества элементов в странице
        {
            if (pageIndex < 0 || pageIndex >= PageCount || pageIndex == PageCount - 1)// Проверка на наличие целостности страницы, естли страница целая, то возвращает количество элементов в массиве
            {
                return collection.Count % itemsPerPage;
            }
            return itemsPerPage;
        }
        public int PageIndex(int itemIndex) // Создаем метод по опредлению количества элементов в странице
        {
            if (itemIndex < 0 || itemIndex >= collection.Count)//Проверка на выход из массив. Если массив выходит, то возвращается значение -1, в ином случае индекс массива вычисляется
            {
                return -1;
            }
            return itemIndex / itemsPerPage;
        }
    }
    internal class Program
    {
        /*Задание 3. Напишите метод, который принимает строку фигурных скобок и определяет, допустим ли порядок фигурных 
скобок. В ответе возвращается true, если строка действительна, и false, если она недействительна*/
        static bool CheckBrackets(string str)// Создаем  метод, который берет строку
        {
            Stack<char> stack = new Stack<char>();// Преобразование строки в контейнер типа Stack  
            foreach (char ch in str)// проход по каждому элементу контейнера с проверкой на совместимость скобок  
            {
                if (ch == '(' || ch == '{' || ch == '[')
                {
                    stack.Push(ch);
                }
                else if (ch == ')' || ch == '}' || ch == ']')
                {
                    if (stack.Count == 0)
                    {
                        return false;
                    }
                    char last = stack.Pop();
                    if ((ch == ')' && last != '(') || (ch == '}' && last != '{') || (ch == ']' && last != '['))
                    {
                        return false;
                    }
                }
            }
            return stack.Count == 0;
        }
        static void Main(string[] args)
        {
            /*Задание 1. Напишите программу, которая принимает на вход строку и возвращает другую строку, где вместо каждого 
уникального символа знак "(" и знак ")" вместо каждого повторяющегося. Регистр у входящего строки 
игнорируюется.*/
            Console.Write("Введите набор символов в строку: ");
            string input_line = Console.ReadLine();// Вводим новую строку
            input_line = input_line.ToLower();// Делаем все символы в нижнем регистре, тем самым прировняв их.
            char[] input_chars = input_line.ToCharArray();// Разбиваем строку на массив символов
            for (int i = 0; i < input_line.Length; i++)// Проходим по каждому символу с помощью цикла for
            {
                char ch = input_chars[i]; // Создаем значение ch, с помощью которого мы мы будем сверять с схожестью элемента массива
                int n = 0;// Создается счетки где данный элемент встречается повторно, с каждым новым шагом цикла, счетчик обнуляется
                for (int j = i; j < input_line.Length; j++) // С помощью цикла for проходим поочередно по другим элеметам для сравнения
                {
                    if (input_chars[j] == ch)
                    {
                        n++; // Если элемент массива равен проверочному элементу, то счетчик пополняется на единицу
                    }
                }
                if (n >= 2) input_line = input_line.Replace(ch, ')');// После рассчета идет проверка счетчика, если символов не менее двух, то мы изменяем все элементы в строке на ')'
                else input_line = input_line.Replace(ch, '(');// В протимном случае символ меняется на '('
            }
            Console.WriteLine(input_line);// Вводим новую строку    
            /*Проверка задания 2*/
            Console.WriteLine("Введите длину массива: ");
            int length = Convert.ToInt32(Console.ReadLine());
            List<int> Array = new List<int>(length);
            Random rand = new Random();
            for (int i = 0; i < length; i++) Array.Add(rand.Next(0, 100));
            for (int i = 0; i < length; i++) Console.WriteLine(Array[i]);
            Console.WriteLine("Введите количество элементов на одной странице: ");
            int n_on_page = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите номер страницы: ");
            int n_page = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите индекс массива: ");
            int index_array = Convert.ToInt32(Console.ReadLine());
            PaginationHelper<int> helper = new PaginationHelper<int>(Array, n_on_page);
            Console.WriteLine("Количество элементов: " + helper.ItemCount);
            Console.WriteLine("Количество страниц: " + helper.PageCount);
            Console.WriteLine($"Количество элементов на странице {n_on_page}: " + helper.PageItemCount(n_page));
            Console.WriteLine($"Индекс страницы для элемента с индексом {index_array}: " + helper.PageIndex(index_array));
            /*Проверка задания 3*/
            Console.Write("Введите набор скобок в строку: ");
            string bricketline = Console.ReadLine();
            bool result = CheckBrackets(bricketline);
            Console.WriteLine(result);
        }
    }
}
