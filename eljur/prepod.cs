using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace eljur
{
    class prepod
    {
        public void Authentication(string login)
        {
            Authorization auth = new Authorization();
            string path11 = Directory.GetCurrentDirectory();
            DirectoryInfo dir2 = new DirectoryInfo(path11 + @"\Данные\Преподаватели");
            foreach (var item in dir2.GetFiles())
            {
                if (login + ".dat" == item.Name)
                {
                    Console.Write("Введите пароль: ");
                    string parol = Console.ReadLine();
                    string fam = "", ima = "", otch = "", log = "", pas = "";
                    int i = 0;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Преподаватели\" + login + ".dat", FileMode.Open));
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (i == 0)
                            fam = name;
                        if (i == 1)
                            ima = name;
                        if (i == 2)
                            otch = name;
                        if (i == 3)
                            log = name;
                        if (i == 4)
                            pas = name;
                        i++;
                    }
                    reader.Close();
                    if (parol == pas)
                    {
                        Console.WriteLine("Вы вошли как преподаватель!");
                        Thread.Sleep(1500);
                        Console.Clear();
                        Prepodavat(login);
                    }
                    if (parol != pas)
                    {
                        Console.WriteLine("Пароль неверный!");
                        Thread.Sleep(1500);
                        auth.Lolkin();
                    }
                }
            }
            Console.WriteLine("Логин не найден!");
            Thread.Sleep(1500);
            auth.Lolkin();
        }

        public void Prepodavat(string login)
        {
            string fam = "", ima = "", otch = "", log = "", pas = "";
            int i = 0;
            string path11 = Directory.GetCurrentDirectory();
            BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Преподаватели\" + login + ".dat", FileMode.Open));
            while (reader.PeekChar() > -1)
            {
                string name = reader.ReadString();
                if (i == 0)
                    fam = name;
                if (i == 1)
                    ima = name;
                if (i == 2)
                    otch = name;
                if (i == 3)
                    log = name;
                if (i == 4)
                    pas = name;
                i++;
            }
            reader.Close();
            string path1 = "";

            path1 = path11 + @"\Данные\Преподаватели\ДР\" + login + ".dat";
            if (!File.Exists(path1))
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

                BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Преподаватели\ДР\" + login + ".dat", FileMode.CreateNew));
                writer.Write(dr);
                writer.Write(god);
                writer.Close();
            }
            if (File.Exists(path1))
                goto perg;

            perg:
            string path2 = path11 + @"\Данные\Преподаватели\ДР\" + login + ".dat";
            reader = new BinaryReader(File.Open(path2, FileMode.Open));
            string dr1 = "", god1 = "";
            int j = 0;
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
        start: Console.WriteLine("Добро пожаловать, " + fam + " " + ima + " " + otch);
            Console.WriteLine("Ваши данные:");
            Console.WriteLine("Дата рождения: " + dr1);
            Console.WriteLine("Возраст: " + god1);
            Console.WriteLine();

            Console.WriteLine("Нажмите: ");
            Console.WriteLine("Enter: войти в журнал");
            Console.WriteLine("F5: Выйти из аккаунта");

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    goto strat;
                }
                if (Console.ReadKey(true).Key == ConsoleKey.F5)
                {
                    MenuVibor lol = new MenuVibor();
                    lol.Vibor();
                }
            }

        strat:
            j = 1;
            string[] nums = new string[100];
            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Журналы");
            foreach (var item in dir.GetDirectories())
            {
                nums[j] = item.Name;
                j++;
            }

            string[] grupa = new string[100];
            string[] disciplina = new string[100];
            string path3 = "";
            int g1 = 1;
            string dis = "", logk = "", grh = "";

            for (int h = 1; h < j; h++)
            {
                path3 = path11 + @"\Данные\Журналы\" + nums[h];

                DirectoryInfo dir2 = new DirectoryInfo(path3);
                foreach (var item in dir2.GetFiles())
                {
                    int g = 1;
                    reader = new BinaryReader(File.Open(path3 + @"\" + item.Name, FileMode.Open));
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (g == 1)
                            dis = name;
                        if (g == 2)
                            grh = name;
                        if (g == 3)
                        {
                            logk = name;
                            if (logk == login)
                            {
                                grupa[g1] = grh;
                                disciplina[g1] = item.Name;
                                g1++;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        g++;
                    }
                    reader.Close();
                }
            }
            if (g1 == 1)
            {
                Console.WriteLine("Внимание! У Вас отсутвуют закреплённые за Вами дисциплины/группы! Обратитесь к администратору!");
                Thread.Sleep(6000);
                Console.Clear();
                MenuVibor lol = new MenuVibor();
                lol.Vibor();
            }
            reader.Close();
            Console.WriteLine();
            Console.WriteLine("Выберите группу и дисциплину:");
            string[] vi_dis = new string[100];
            string[] vi_gr = new string[100];
            int vi_dis1 = 1;
            int[] checker = new int[100];
            checker[0] = 0;
            for (int h = 1; h < g1; h++)
            {
                Console.WriteLine( "  " + vi_dis1 + ") " + grupa[h] + " " + Path.GetFileNameWithoutExtension(disciplina[h]));
                vi_dis[h] = Path.GetFileNameWithoutExtension(disciplina[h]);
                vi_gr[h] = grupa[h];
                vi_dis1++;
                checker[h] = checker[h - 1] + 1;
            }

            
            agg1: Console.Write("Введите номер: ");
            int vibor = Convert.ToInt32(Console.ReadLine());

            bool flag = false;
            for (int i1 = 0; i1 < vi_dis1; ++i1)
            {
                if (checker[i1] == vibor && vibor != 0)
                {
                    flag = true;
                }
            }
            if (flag == false)
            {
                Console.WriteLine("Error");
                Thread.Sleep(2000);
                goto agg1;
            }

            string group = vi_gr[vibor];
            string predmet = vi_dis[vibor];

            Console.Clear();
            Console.WriteLine("Студенты группы:");
            int[] checker3 = new int[100];
            student: string[] nums3 = new string[35];
            DirectoryInfo dir3 = new DirectoryInfo(path11 + @"\Данные\Журналы\" + group);
            j = 1;
            foreach (var item in dir3.GetDirectories())
            {
                nums[j] = item.Name;
                Console.WriteLine(j + ") " + nums[j]);
                checker3[j] = j;
                j++;
            }
            int j1 = 0;

            if (j == 1)
            {
                Console.WriteLine("У данной учебной группы нет студентов!");
                Thread.Sleep(2000);
                Console.Clear();
                goto start;
            }
            if (j > 1)
            {
                Console.Write("Введите номер студента: ");
                j1 = Convert.ToInt32(Console.ReadLine());
                if (nums[j1] == "" || nums[j1] == null)
                {
                    Console.WriteLine("Error!");
                    Thread.Sleep(2000);
                    goto student;
                }
                bool flag1 = false;
                for (int i1 = 0; i1 < j; ++i1)
                {
                    if (checker3[i1] == j1)
                    {
                        flag1 = true;
                    }
                }

                if (flag1 == false)
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    goto student;
                }
            }

            syt: Console.Clear();
            Console.WriteLine("Выберите:");
            Console.WriteLine("1. Просмотреть оценки студента по дисциплинe "+predmet);
            Console.WriteLine("2. Поставить новую оценку студенту по дисциплине " + predmet);
            Console.WriteLine("3. Изменить оценку студента по дисциплине " + predmet);
            Console.Write("Выбор: ");
            string vibor2 = Console.ReadLine();

            if (vibor2 == "1")
            {
                Console.Clear();
                Console.WriteLine("Оценки студента: " + nums[j1]);
                string path5 = path11 + @"\Данные\Журналы\" + group + @"\" + nums[j1];
                
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
                    reader = new BinaryReader(File.Open(path11 + @"\Данные\Журналы\" + group + @"\" + nums[j1] + @"\" + nums5[i], FileMode.Open));
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
                        if (h == 3 && otbr1 == predmet)
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
                            Thread.Sleep(10000);
                            Console.Clear();
                            goto start;
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
                    reader.Close();
                    Console.WriteLine("Оценок нет!");
                    Thread.Sleep(3000);
                    Console.Clear();
                    goto start;
                }
            }

            if (vibor2 == "2")
            {
            starty:
                Console.Clear();
                Console.Write("Введите оценку: ");
                string otchenka = Convert.ToString(Console.ReadLine());

                if (otchenka == "2" || otchenka == "3" || otchenka == "4" || otchenka == "5")
                {
                    goto lolik;
                }
                else
                {
                    Console.WriteLine("В нашей шараге используется 5-бальная система (от 2 до 5)!");
                    Thread.Sleep(2000);
                    goto starty;
                }

            lolik: Console.Write("Введите дату (если сегодня, то нажмите Enter): ");
                string data = Console.ReadLine();
                DateTime dt = DateTime.Now;
                if (data == "" || data == null)
                {
                    data = dt.ToShortDateString();
                }
                Console.WriteLine();
                Console.WriteLine("Вы поставили оценку " + otchenka + " по дисциплине " + predmet + " за " + data + " студенту " + nums[j1]);

                string path22 = path11 + @"\Данные\Журналы\" + group + @"\" + nums[j1] + @"\" + data + " " + predmet + ".dat";
                if (File.Exists(path22))
                {
                    File.Delete(path22);
                    BinaryWriter writer = new BinaryWriter(File.Open(path22, FileMode.OpenOrCreate));
                    writer.Write(predmet);
                    writer.Write(otchenka);
                    writer.Write(data);
                    writer.Close();
                    Thread.Sleep(4500);
                    Console.Clear();
                    goto start;
                }
                if (!File.Exists(path22))
                {
                    BinaryWriter writer = new BinaryWriter(File.Open(path22, FileMode.OpenOrCreate));
                    writer.Write(predmet);
                    writer.Write(otchenka);
                    writer.Write(data);
                    writer.Close();
                    Thread.Sleep(4500);
                    Console.Clear();
                    goto start;
                }
            }

            if (vibor2 == "3")
            {
                Console.Clear();
                Console.WriteLine("Оценки студента: " + nums[j1]);
                string path5 = path11 + @"\Данные\Журналы\" + group + @"\" + nums[j1];
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
                int[] checker1 = new int[100];
                int g2 = 0;
                hhj:
                reader = new BinaryReader(File.Open(path11 + @"\Данные\Журналы\" + group + @"\" + nums[j1] + @"\" + nums5[i], FileMode.Open));
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
                    if (h == 3 && otbr1 == predmet)
                    {
                        Console.WriteLine(i + ") По дисциплине " + otbr1 + " оценка " + otbr2 + " за " + otbr3);
                        checker1[g2] = i;
                        g2++;
                    }
                }

                if (i < j3)
                {
                    i++;
                    if (i >= j3)
                    {
                        reader.Close();
                        agg2: Console.Write("Введите номер оценки, котрый надо изменить: ");
                        int vibb = Convert.ToInt32(Console.ReadLine());

                        bool flag1 = false;
                        for (int i1 = 0; i1 < g2; ++i1)
                        {
                            if (checker1[i1] == vibb)
                            {
                                flag1 = true;
                            }
                        }

                        if (flag1 == false)
                        {
                            Console.WriteLine("Error");
                            Thread.Sleep(2000);
                            goto agg2;
                        }

                        System.IO.File.Delete(path11 + @"\Данные\Журналы\" + group + @"\" + nums[j1] + @"\" + nums5[vibb]);

                    starty:
                        Console.Clear();
                        Console.Write("Введите оценку: ");
                        string otchenka = Convert.ToString(Console.ReadLine());

                        if (otchenka == "2" || otchenka == "3" || otchenka == "4" || otchenka == "5")
                        {
                            goto lolik;
                        }
                        else
                        {
                            Console.WriteLine("В нашей шараге используется 5-бальная система (от 2 до 5)!");
                            Thread.Sleep(2000);
                            goto starty;
                        }

                    lolik: Console.Write("Введите дату (если сегодня, то нажмите Enter): ");
                        string data = Console.ReadLine();
                        DateTime dt = DateTime.Now;
                        if (data == "" || data == null)
                        {
                            data = dt.ToShortDateString();
                        }
                        Console.WriteLine();
                        Console.WriteLine("Вы поставили оценку " + otchenka + " по дисциплине " + predmet + " за " + data + " студенту " + nums[j1]);

                        string path23 = path11 + @"\Данные\Журналы\" + group + @"\" + nums[j1] + @"\" + data + " " + predmet + ".dat";
                        if (File.Exists(path23))
                        {
                            File.Delete(path23);
                            BinaryWriter writer = new BinaryWriter(File.Open(path23, FileMode.OpenOrCreate));
                            writer.Write(predmet);
                            writer.Write(otchenka);
                            writer.Write(data);
                            writer.Close();
                            Thread.Sleep(4500);
                            Console.Clear();
                            goto start;
                        }
                        if (!File.Exists(path23))
                        {
                            BinaryWriter writer = new BinaryWriter(File.Open(path23, FileMode.OpenOrCreate));
                            writer.Write(predmet);
                            writer.Write(otchenka);
                            writer.Write(data);
                            Thread.Sleep(4500);
                            writer.Close();
                            Console.Clear();
                            goto start;
                        }
                    }
                    else
                    {
                        reader.Close();
                        goto hhj;
                    }
                }
            }
            if (vibor2 != "3" || vibor2 != "2" || vibor2 != "1")
            {
                Console.WriteLine("Error!");
                Thread.Sleep(2500);
                Console.Clear();
                goto syt;
            }
            Console.ReadKey();
        }
    }
}
