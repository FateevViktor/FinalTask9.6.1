using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinalTask9._6._1.Program;

namespace FinalTask9._6._1
{
    public delegate void NotifySortSurname(int ts, string[] sn); //Делегат
    internal class Program
    {        
        static void Main(string[] args)
        {
            string[] surname = new string[5] { "Иванов", "Петров", "Сидоров", "Пуговкин", "Булкин" };
            int typesorting = 0;
            ProcessSort processSort = new ProcessSort();
            processSort.ProcessCompleted += SortSurname;
            bool check;
            int i = 0;
            Console.WriteLine("Список фамилий:");

            foreach (string element in surname)
            {
                i++;
                Console.WriteLine($"{i}) {element}");
            }

            Console.WriteLine("Как его отсортировать? число 1 (сортировка А-Я), 2 (Я-А):");
            do
            {
                check = false;

                try
                {
                    Console.WriteLine("Введите число 1 или 2");
                    typesorting = Convert.ToInt32(Console.ReadLine());
                    if (typesorting != 1 && typesorting != 2)
                    {
                        throw new MyException();
                    }
                    else
                    {
                        processSort.StartSort(typesorting, surname);
                    }
                }

                //Первое исключение (мое)
                catch (MyException exc)
                {
                    Console.WriteLine("Ошибка при введении цифры:");
                    Console.WriteLine(exc.Message);
                    check = true;
                }

                //Второе исключение - ввели не число
                catch (FormatException exc)
                {
                    Console.WriteLine("Возникла ошибка:");
                    Console.WriteLine("Необходимо ввести число, давайте попробуем еще раз.");
                    Console.WriteLine(exc.Message);
                    check = true;
                }

                //Третье исключение
                catch (ArgumentOutOfRangeException exc)
                {
                    Console.WriteLine("Возникла ошибка:");
                    Console.WriteLine("Аргумент вышел за рамки");
                    Console.WriteLine(exc.Message);
                }

                //Четвёртое исключение
                catch (ArgumentNullException exc)
                {
                    Console.WriteLine("Возникла ошибка:");
                    Console.WriteLine("Аргумент null");
                    Console.WriteLine(exc.Message);
                }

                //Пятое исключение
                catch (ArgumentException exc)
                {
                    Console.WriteLine("Возникла ошибка:");
                    Console.WriteLine("Аргумент не допустимый");
                    Console.WriteLine(exc.Message);
                }

                catch
                {
                    Console.WriteLine("Возникла непредвиденная ошибка:");
                }

                finally
                {
                }
            }while (check);

        }
        //Сортировка
        public static void SortSurname(int typesort, string[] surname)
        {
            int i = 0;
            Console.WriteLine("Событие произошло");
            if (typesort == 1) //Прямая сортировка
            {
                Array.Sort(surname);
            }
            else //Обратная сортировка
            {
                string str;
                Array.Sort(surname);
                for (i = 0; i < surname.Length; i++)
                {
                    if (i < surname.Length - i - 1)
                    {
                        str = surname[i];
                        surname[i] = surname[surname.Length - i - 1];
                        surname[surname.Length - i - 1] = str;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            i = 0;
            Console.WriteLine("Отсортированный список фамилий:");
            foreach (string element in surname)
            {
                i++;
                Console.WriteLine($"{i}) {element}");
            }
        }
    }

    //Создадим своё исключение
    public class MyException : Exception
    {
        public MyException() : base("Допускаются только цифры 1 или 2")
        {

        }
        public MyException(string message) : base(message)
        { 

        }
    }
    //Класс с событием
    public class ProcessSort
    {
        public event NotifySortSurname ProcessCompleted; //Событие

        public void StartSort(int typesort, string[] surname)
        {
            EventProcess(typesort, surname); //Сработка события            
        }
        protected virtual void EventProcess(int typesort, string[] surname)
        {
            ProcessCompleted?.Invoke(typesort, surname);
        }
    }
}
