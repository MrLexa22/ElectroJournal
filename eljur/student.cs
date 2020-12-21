using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace eljur
{
    class student
    {
        public void Authentication(string login)
        {
            Authorization auth = new Authorization();
            string path11 = Directory.GetCurrentDirectory();
            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Студенты");
            foreach (var item in dir.GetFiles())
            {
                if (login + ".dat" == item.Name)
                {
                    Console.Write("Введите пароль: ");
                    string parol = Console.ReadLine();
                    string gr = "", fam = "", ima = "", otch = "", log = "", pas = "";
                    int i = 0;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Студенты\" + login + ".dat", FileMode.Open));
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (i == 0)
                            gr = name;
                        if (i == 1)
                            fam = name;
                        if (i == 2)
                            ima = name;
                        if (i == 3)
                            otch = name;
                        if (i == 4)
                            log = name;
                        if (i == 5)
                            pas = name;
                        i++;
                    }
                    reader.Close();
                    if (parol == pas)
                    {
                        Console.WriteLine("Вы вошли как студент!");
                        Thread.Sleep(1500);
                        Student(login);
                    }
                    if (parol != pas)
                    {
                        Console.WriteLine("Пароль неверный!");
                        Thread.Sleep(1500);
                        auth.Lolkin();
                    }
                }
            }
            prepod pre = new prepod();
            pre.Authentication(login);
        }

        public void Student(string login)
        {
            string gr = "", fam = "", ima = "", otch = "", log = "", pas = "";
            int i = 0;
            string path11 = Directory.GetCurrentDirectory();
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Студенты\" + login + ".dat", FileMode.Open));
            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                if (i == 0)
                    gr = name;
                if (i == 1)
                    fam = name;
                if (i == 2)
                    ima = name;
                if (i == 3)
                    otch = name;
                if (i == 4)
                    log = name;
                if (i == 5)
                    pas = name;
                i++;
            }
            reader.Close();

            Console.Clear();
            if (!File.Exists(path11 + @"\Данные\Студенты\ДР\"+login+".dat"))
                {
                ahg: Console.WriteLine("Внимание! У Вас не указана дата рождения! Необходимо её указать!");
                    Console.Write("Введите дату рождения (формата 01.01.2000): ");
                    string dr = Console.ReadLine();
                    if (!Regex.IsMatch(dr, @"^[0-9.]+$"))
                    {
                        Console.WriteLine("Error!");
                        Thread.Sleep(1500);
                        goto ahg;
                    }
                    Console.Write("Введите свой возраст: ");
                    string god = Console.ReadLine();
                    if (!Regex.IsMatch(god, @"^[0-9]+$"))
                    {
                        Console.WriteLine("Error!");
                        Thread.Sleep(1500);
                        goto ahg;
                    }

                    BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Студенты\ДР\" + login + ".dat", FileMode.CreateNew));
                    writer.Write(dr);
                    writer.Write(god);
                    writer.Close();
                }
                if (File.Exists(path11 + @"\Данные\Студенты\ДР\" + login + ".dat"))
                {
                    goto styu;
                }

            styu: int j = 0;
            string dr1 = "", god1 = "";
            reader = new BinaryReader(File.Open(path11 + @"\Данные\Студенты\ДР\" + login + ".dat", FileMode.Open));
            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                if (j == 0)
                    dr1 = name;
                if (j == 1)
                    god1 = name;
                j++;
            }
            reader.Close();
            Console.WriteLine("Добро пожаловать, " + fam + " " + ima + " " + otch);
            Console.WriteLine("Ваши данные:");
            Console.WriteLine("Группа: " + gr);
            Console.WriteLine("Дата рождения: "+dr1);
            Console.WriteLine("Возраст: " + god1);
            Console.WriteLine();
            Console.WriteLine("Ваши оценки: ");

            string path5 = "";
            if (otch == "" || otch == " " || otch == null)
                path5 = path11 + @"\Данные\Журналы\" + gr + @"\" + fam + " " + ima;

            if (!(otch == "" || otch == " " || otch == null))
                path5 = path11 + @"\Данные\Журналы\" + gr + @"\" + fam + " " + ima+" "+otch;

            int j3 = 0;
            string[] nums5 = new string[100];
            DirectoryInfo dir5 = new DirectoryInfo(path5);

            foreach (var item in dir5.GetFiles())
            {
                nums5[j3] = item.Name;
                j3++;
            }

            string otbr1 = "", otbr2 = "", otbr3 = "";

            i = 0;
            if (j3 != 0)
            {
            hhj:

                reader = new BinaryReader(File.Open(path5 + @"\"+ nums5[i], FileMode.Open));
                int h = 0;
                while (reader.PeekChar() > -1)
                {
                    string name = reader.ReadString();
                    if (h == 0)
                        otbr1 = name;
                    if (h == 1)
                        otbr2 = name;
                    if (h == 2)
                        otbr3 = name;
                    h++;
                    if (h == 3)
                    {
                        Console.WriteLine("По дисциплине " + otbr1 + " оценка " + otbr2 + " за " + otbr3);
                    }
                }

                if (i < j3)
                {
                    i++;
                    if (i >= j3)
                    {
                        reader.Close();
                        Console.WriteLine();
                        Console.WriteLine("Для выхода из аккаунта нажмите Enter");
                        while (true)
                        {
                            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                            {
                                Authorization auth = new Authorization();
                                auth.Lolkin();
                            }
                        }
                    }
                    else
                    {
                        reader.Close();
                        goto hhj;
                    }
                }
            }
            if (j3 == 0)
            {
                Console.WriteLine("Оценок нет!");
                Console.WriteLine();
                Console.WriteLine("Для выхода из аккаунта нажмите Enter");
                while (true)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        MenuVibor lol = new MenuVibor();
                        lol.Vibor();
                    }
                }
            }
        }
    }
}
