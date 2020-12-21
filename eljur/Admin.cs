using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices.ComTypes;

namespace eljur
{
    class Admin
    {
        static strelki str = new strelki();
        static public int ViewMenu(string[] menuText)
        {
            Console.Clear();
            foreach (var text in menuText)
                Console.Write(text);
            int chek = str.CheckerMenu(menuText.Length);
            Console.Clear();
            return chek;
        }

        public void Admin_panel()
        {
            Console.Clear();
            string[] menuText = { "Выберите\n", "  1)Добавить группу\n", "  2)Зачислить студента в группу\n", "  3)Добавить преподавателя\n", "  4)Закрепить группу и преподавателя за дисциплину\n", "  5)Удалить преподавателя или студента\n", "  6)Редактировать данные студентов или преподавателей\n", "  7)Просмотри и редактирование журнала учебной группы\n", "  8)Выход\n" };
            Authorization auth = new Authorization();
            while (true)
            {

                int chek = ViewMenu(menuText);

                switch (chek)
                {
                    case 1:
                        AddGroup();
                        break;
                    case 2:
                        AddStudent();
                        break;
                    case 3:
                        AddPrepod();
                        break;
                    case 4:
                        AddSubject();
                        break;
                    case 5:
                        Delete();
                        break;
                    case 6:
                        Redact();
                        break;
                    case 7:
                        Journal();
                        break;
                    case 8:
                        MenuVibor lol = new MenuVibor();
                        lol.Vibor();
                        break;
                }
            }


        }

        public void AddGroup()
        {
            string path11 = Directory.GetCurrentDirectory();
        again: Console.Clear();
            Console.Write("Введите название группы: ");
            string gr = Console.ReadLine();
            if (gr == "" || gr == null || gr == " " || gr == "  " || gr.Length<=1)
            {
                Console.WriteLine("Error");
                Thread.Sleep(2000);
                goto again;
            }
            if (System.IO.Directory.Exists(path11+@"\Данные\Журналы\" + gr))
            {
                Console.WriteLine("Данная учебная группа уже существует, повторите ввод");
                Thread.Sleep(2000);
                goto again;
            }
            else
            {
                var directory = new DirectoryInfo(path11 + @"\Данные\Журналы\");
                directory.CreateSubdirectory(gr);
                Console.WriteLine("Группа добавлена!");
                Thread.Sleep(2000);
                Admin_panel();
            }
        }

        public void AddStudent()
        {
            string path11 = Directory.GetCurrentDirectory();
            string[] nums = new string[100];
            int j = 1;
            again:
            Console.WriteLine("Группы: ");
            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Журналы\");
            foreach (var item in dir.GetDirectories())
            {
                nums[j] = item.Name;
                Console.WriteLine(" "+nums[j]);
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Нет групп!");
                Thread.Sleep(2000);
                Admin_panel();
            }

            Console.WriteLine();
            Console.Write("Введите группу, куда будет зачислен студент: ");
            string gr = Console.ReadLine();
            if (!System.IO.Directory.Exists(path11 + @"\Данные\Журналы\" + gr))
            {
                Console.WriteLine("Данная учебная группа не существет");
                Thread.Sleep(2000);
                Console.Clear();
                goto again;
            }


        st: string fam = "", ima = "", otch = " ";
            Console.Write("Введите фамилию студента: ");
            fam = Console.ReadLine();
            Console.Write("Введите имя студента: ");
            ima = Console.ReadLine();
            Console.Write("Введите отчество студента: ");
            otch = Console.ReadLine();
            if (fam == "" || ima == "" || !Regex.IsMatch(fam, @"^[а-яА-Я]+$") || !Regex.IsMatch(ima, @"^[а-яА-Я]+$") || Regex.IsMatch(fam, @"^[a-zA-Z]+$") || Regex.IsMatch(ima, @"^[a-zA-Z]+$") || fam.Contains(" ") || ima.Contains(" "))
            {
                Console.WriteLine("Вы некорректно ввели данные!");
                Thread.Sleep(2000);
                Console.Clear();
                goto st;
            }

            string st_login = "", st_pas = "";

        r1: Console.Write("Введите логин студенту: ");
            st_login = Console.ReadLine();
            if (st_login == "" || st_login == " " || st_login == null || Regex.IsMatch(st_login, @"^[а-яА-Я]+$") || st_login.Contains(" ") || st_login.Length<2)
            {
                Console.WriteLine("Error!");
                Thread.Sleep(2000);
                goto r1;
            }

            DirectoryInfo dir2 = new DirectoryInfo(path11 + @"\Данные\Студенты");
            foreach (var item in dir2.GetFiles())
            {
                if (st_login+".dat" == item.Name)
                {
                    Console.WriteLine("Логин уже существует!!");
                    goto r1;
                }
            }
            dir2 = new DirectoryInfo(path11 +@"\Данные\Преподаватели");
            foreach (var item in dir2.GetFiles())
            {
                if (st_login + ".dat" == item.Name)
                {
                    Console.WriteLine("Логин уже существует!!");
                    goto r1;
                }
            }


        r2: Console.Write("Введите пароль студенту: ");
            st_pas = Console.ReadLine();
            if (st_pas == "" || st_pas == " " || st_pas == null || Regex.IsMatch(st_pas, @"^[а-яА-Я]+$") || st_pas.Contains(" ") || st_pas.Length < 5)
            {
                Console.WriteLine("Error! (Длина пароля должны быть не меньше 5)");
                Thread.Sleep(2000);
                goto r2;
            }

            BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Студенты\" + st_login + ".dat", FileMode.OpenOrCreate));
            writer.Write(gr);
            writer.Write(fam);
            writer.Write(ima);
            writer.Write(otch);
            writer.Write(st_login);
            writer.Write(st_pas);
            writer.Close();
            Directory.CreateDirectory(path11 + @"\Данные\Журналы\" + gr + @"\" + fam + " " + ima+" "+otch);

            Console.WriteLine("Данные записаны!");
            Thread.Sleep(2000);
        }

        public void AddPrepod()
        {
            string path11 = Directory.GetCurrentDirectory();
        st: string fam = "", ima = "", otch = " ";
            Console.Write("Введите фамилию преподавателя: ");
            fam = Console.ReadLine();
            Console.Write("Введите имя преподавателя: ");
            ima = Console.ReadLine();
            Console.Write("Введите отчество преподавателя: ");
            otch = Console.ReadLine();
            if (fam == "" || ima == "" || !Regex.IsMatch(fam, @"^[а-яА-Я]+$") || !Regex.IsMatch(ima, @"^[а-яА-Я]+$") || Regex.IsMatch(fam, @"^[a-zA-Z]+$") || Regex.IsMatch(ima, @"^[a-zA-Z]+$") || fam.Contains(" ") || ima.Contains(" "))
            {
                Console.WriteLine("Вы не ввели фамилию или имя!");
                Thread.Sleep(2000);
                Console.Clear();
                goto st;
            }

            string st_login = "", st_pas = "";

        r1: Console.Write("Введите логин преподавателя: ");
            st_login = Console.ReadLine();
            if (st_login == "" || st_login == " " || st_login == null || Regex.IsMatch(st_login, @"^[а-яА-Я]+$") || st_login.Contains(" ") || st_login.Length < 2)
            {
                Console.WriteLine("Error!");
                Thread.Sleep(2000);
                goto r1;
            }

            DirectoryInfo dir2 = new DirectoryInfo(path11 + @"\Данные\Преподаватели");
            foreach (var item in dir2.GetFiles())
            {
                if (st_login + ".dat" == item.Name)
                {
                    Console.WriteLine("Логин уже существует!!");
                    goto r1;
                }
            }
            dir2 = new DirectoryInfo(path11 + @"\Данные\Студенты");
            foreach (var item in dir2.GetFiles())
            {
                if (st_login + ".dat" == item.Name)
                {
                    Console.WriteLine("Логин уже существует!!");
                    goto r1;
                }
            }


        r2: Console.Write("Введите пароль преподавателя: ");
            st_pas = Console.ReadLine();
            if (st_pas == "" || st_pas == " " || st_pas == null || Regex.IsMatch(st_pas, @"^[а-яА-Я]+$") || st_pas.Contains(" ") || st_pas.Length < 5)
            {
                Console.WriteLine("Error!");
                Thread.Sleep(2000);
                goto r2;
            }

            BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Преподаватели\" + st_login + ".dat", FileMode.OpenOrCreate));
            writer.Write(fam);
            writer.Write(ima);
            writer.Write(otch);
            writer.Write(st_login);
            writer.Write(st_pas);
            writer.Close();

            Console.WriteLine("Данные записаны!");
            Thread.Sleep(2000);
        }

        public void AddSubject()
        {
            string path11 = Directory.GetCurrentDirectory();
        r1: Console.Write("Введите наименование предмета: ");
            string name = Console.ReadLine();

            string[] nums = new string[100];
            int j = 1;
            again:
            Console.WriteLine("Группы: ");
            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Журналы");
            foreach (var item in dir.GetDirectories())
            {
                nums[j] = item.Name;
                Console.WriteLine(" " + nums[j]);
                j++;
            }
            if (j == 1)
            {
                Console.WriteLine("Нет групп!");
                Thread.Sleep(2000);
                Admin_panel();
            }

            Console.Write("Введите группу, за которой закреплён предмет: ");
            string gr = Console.ReadLine();

            if (!System.IO.Directory.Exists(path11 + @"\Данные\Журналы\" + gr))
            {
                Console.WriteLine("Данная учебная группа не существет");
                Thread.Sleep(2000);
                Console.Clear();
                goto again;
            }
            DirectoryInfo dir2 = new DirectoryInfo(path11 + @"\Данные\Журналы\" + gr);
            foreach (var item in dir2.GetFiles())
            {
                if (name + ".dat" == item.Name)
                {
                    Console.WriteLine("Данный предмет у данной учебной группы существует уже!");
                    Thread.Sleep(1500);
                    Console.Clear();
                    goto r1;
                }
            }

        again1:  Console.WriteLine("Преподаватели: ");
            string[] nums1 = new string[100];
            int j1 = 1;
            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Преподаватели");
            foreach (var item in dir1.GetFiles())
            {
                nums[j1] = item.Name;
                Console.WriteLine(" " + Path.GetFileNameWithoutExtension(nums[j1]));
                j1++;
            }

            if (j1 == 1)
            {
                Console.WriteLine("Преподавателей нет!");
                Thread.Sleep(2000);
                Console.Clear();
                Admin_panel();
            }

            Console.Write("Введите логин преподавателя, за которым закреплён предмет: ");
            string rpe = Console.ReadLine();

            if (!System.IO.File.Exists(path11 + @"\Данные\Преподаватели\" + rpe+".dat"))
            {
                Console.WriteLine("Данный логин не существует");
                Thread.Sleep(2000);
                Console.Clear();
                goto again1;
            }

            BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Журналы\" + gr+@"\"+ name + ".dat", FileMode.OpenOrCreate));
            writer.Write(name);
            writer.Write(gr);
            writer.Write(rpe);
            writer.Close();
            Console.WriteLine("Данные записаны!");
            Thread.Sleep(2000);
        }

        public void Delete()
        {
            string path11 = Directory.GetCurrentDirectory();
            Console.Clear();
            Console.WriteLine("Выберите, кого жалете удалить: ");
            Console.WriteLine("1. Преподавателя");
            Console.WriteLine("2. Студента");
            Console.Write("Выбор: ");
            string vib = Console.ReadLine();

            if (vib=="1")
            {
                stt: Console.WriteLine("Логины: ");
                string[] dirs = Directory.GetFiles(path11 + @"\Данные\Преподаватели");
                int i = 1;
                foreach (string s in dirs)
                {
                    Console.WriteLine(" " + System.IO.Path.GetFileNameWithoutExtension(s));
                    i++;
                }
                if (i == 1)
                {
                    Console.WriteLine("Преподавателей нет!");
                    Thread.Sleep(1500);
                    Admin_panel();
                }
                Console.Write("Введите логин преподавателя, который желаете удалить: ");
                string del_login = Console.ReadLine();
                
                if (!System.IO.File.Exists(path11 + @"\Данные\Преподаватели\" + del_login + ".dat"))
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto stt;
                }
                else
                {
                    System.IO.File.Delete(path11 + @"\Данные\Преподаватели\" + del_login + ".dat");
                    if (File.Exists(path11 + @"\Данные\Преподаватели\ДР\" + del_login + ".dat"))
                        File.Delete(path11 + @"\Данные\Преподаватели\ДР\" + del_login + ".dat");
                    Console.WriteLine("Преподаватель удалён!");
                    Thread.Sleep(2000);
                }
            }
            if (vib == "2")
            {
            stt: Console.WriteLine("Логины: ");
                string[] dirs = Directory.GetFiles(path11 + @"\Данные\Студенты");
                int i = 1;
                foreach (string s in dirs)
                {                   
                    Console.WriteLine(" " + System.IO.Path.GetFileNameWithoutExtension(s));
                    i++;
                }
                if (i == 1)
                {
                    Console.WriteLine("Студентов нет!");
                    Thread.Sleep(1500);
                    Admin_panel();
                }
                Console.Write("Введите логин студента, который желаете удалить: ");
                string del_login = Console.ReadLine();

                if (!System.IO.File.Exists(path11 + @"\Данные\Студенты\" + del_login + ".dat"))
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto stt;
                }
                else
                {
                    string ima = "", gr = "", fam = "", otch = "";
                    i = 0;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Студенты\" + del_login+".dat", FileMode.Open));
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
                        i++;
                    }
                    reader.Close();
                    Directory.Delete(path11 + @"\Данные\Журналы\" + gr + @"\" + fam + " " + ima + " " + otch);
                    System.IO.File.Delete(path11 + @"\Данные\Студенты\" + del_login + ".dat");
                    if (File.Exists(path11 + @"\Данные\Студенты\ДР\" + del_login + ".dat"))
                        File.Delete(path11 + @"\Данные\Студенты\ДР\" + del_login + ".dat");
                    Console.WriteLine("Студент удалён!");
                    Thread.Sleep(2000);
                }
            }
        }

        public void Redact()
        {
            string path11 = Directory.GetCurrentDirectory();
            Console.Clear();
            Console.WriteLine("Выберите, кого жалете изменить: ");
            Console.WriteLine("1. Преподавателя");
            Console.WriteLine("2. Студента");
            Console.Write("Выбор: ");
            string vib = Console.ReadLine();

            if (vib == "1")
            {
                stt: Console.WriteLine("Логины: ");
                string[] dirs = Directory.GetFiles(path11 + @"\Данные\Преподаватели");
                int i = 1;
                foreach (string s in dirs)
                {
                    Console.WriteLine(" " + System.IO.Path.GetFileNameWithoutExtension(s));
                    i++;
                }
                if (i == 1)
                {
                    Console.WriteLine("Преподавателей нет!");
                    Thread.Sleep(1500);
                    Admin_panel();
                }
                Console.Write("Введите логин преподавателя, который желаете изменить: ");
                string red_login = Console.ReadLine();

                if (!System.IO.File.Exists(path11 + @"\Данные\Преподаватели\" + red_login + ".dat"))
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto stt;
                }
                else
                {
                    Console.Clear();
                    st: string fam = "", ima = "", otch = " ";
                    Console.Write("Введите фамилию преподавателя: ");
                    fam = Console.ReadLine();
                    Console.Write("Введите имя преподавателя: ");
                    ima = Console.ReadLine();
                    Console.Write("Введите отчество преподавателя: ");
                    otch = Console.ReadLine();
                    if (fam == "" || ima == "" || !Regex.IsMatch(fam, @"^[а-яА-Я]+$") || !Regex.IsMatch(ima, @"^[а-яА-Я]+$") || Regex.IsMatch(fam, @"^[a-zA-Z]+$") || Regex.IsMatch(ima, @"^[a-zA-Z]+$") || fam.Contains(" ") || ima.Contains(" "))
                    {
                        Console.WriteLine("Вы не ввели фамилию или имя!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        goto st;
                    }

                    string st_login = "", st_pas = "";

                r1: Console.Write("Введите логин преподавателя: ");
                    st_login = Console.ReadLine();
                    if (st_login == "" || st_login == " " || st_login == null || Regex.IsMatch(st_login, @"^[а-яА-Я]+$") || st_login.Contains(" ") || st_login.Length < 2)
                    {
                        Console.WriteLine("Error!");
                        Thread.Sleep(2000);
                        goto r1;
                    }

                    DirectoryInfo dir2 = new DirectoryInfo(path11 + @"\Данные\Преподаватели");
                    foreach (var item in dir2.GetFiles())
                    {
                        if (st_login + ".dat" == item.Name)
                        {
                            Console.WriteLine("Логин уже существует!!");
                            goto r1;
                        }
                    }
                    dir2 = new DirectoryInfo(path11 + @"\Данные\Студенты");
                    foreach (var item in dir2.GetFiles())
                    {
                        if (st_login + ".dat" == item.Name)
                        {
                            Console.WriteLine("Логин уже существует!!");
                            goto r1;
                        }
                    }


                r2: Console.Write("Введите пароль преподавателя: ");
                    st_pas = Console.ReadLine();
                    if (st_pas == "" || st_pas == " " || st_pas == null || Regex.IsMatch(st_pas, @"^[а-яА-Я]+$") || st_pas.Contains(" ") || st_pas.Length < 5)
                    {
                        Console.WriteLine("Error!");
                        Thread.Sleep(2000);
                        goto r2;
                    }

                    System.IO.File.Delete(path11 + @"\Данные\Преподаватели\" + red_login + ".dat");
                    if (File.Exists(path11 + @"\Данные\Преподаватели\ДР\"+red_login+".dat"))
                        File.Delete(path11 + @"\Данные\Преподаватели\ДР\" + red_login + ".dat");


                    BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Преподаватели\" + st_login + ".dat", FileMode.OpenOrCreate));
                    writer.Write(fam);
                    writer.Write(ima);
                    writer.Write(otch);
                    writer.Write(st_login);
                    writer.Write(st_pas);
                    writer.Close();
                }
            }

            if (vib == "2")
            {
            stt:
                Console.Clear();
                Console.WriteLine("Логины: ");
                string[] dirs = Directory.GetFiles(path11 + @"\Данные\Студенты");
                int i = 1;
                foreach (string s in dirs)
                {
                    Console.WriteLine(" " + System.IO.Path.GetFileNameWithoutExtension(s));
                    i++;
                }
                if (i == 1)
                {
                    Console.WriteLine("Студентов нет!");
                    Thread.Sleep(1500);
                    Admin_panel();
                }
                Console.Write("Введите логин студента, который желаете изменить: ");
                string red_login = Console.ReadLine();
                if (!System.IO.File.Exists(path11 + @"\Данные\Студенты\" + red_login + ".dat"))
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto stt;
                }
                else
                {
                    Console.Clear();

                again:
                    Console.WriteLine("Группы: ");
                    string[] dirs1 = Directory.GetDirectories(path11 + @"\Данные\Журналы");
                    foreach (string s in dirs1)
                    {
                        int i1 = 1;
                        Console.WriteLine(" " + s);
                        i1++;
                    }

                    Console.WriteLine();
                    Console.Write("Введите группу, куда будет зачислен студент: ");
                    string gr = Console.ReadLine();
                    if (!System.IO.Directory.Exists(path11 + @"\Данные\Журналы\" + gr))
                    {
                        Console.WriteLine("Данная учебная группа не существет");
                        Thread.Sleep(2000);
                        Console.Clear();
                        goto again;
                    }
                st: string fam = "", ima = "", otch = " ";
                    Console.Write("Введите фамилию студента: ");
                    fam = Console.ReadLine();
                    Console.Write("Введите имя студента: ");
                    ima = Console.ReadLine();
                    Console.Write("Введите отчество студента: ");
                    otch = Console.ReadLine();
                    if (fam == "" || ima == "" || !Regex.IsMatch(fam, @"^[а-яА-Я]+$") || !Regex.IsMatch(ima, @"^[а-яА-Я]+$") || Regex.IsMatch(fam, @"^[a-zA-Z]+$") || Regex.IsMatch(ima, @"^[a-zA-Z]+$") || fam.Contains(" ") || ima.Contains(" "))
                    {
                        Console.WriteLine("Вы не ввели фамилию или имя!");
                        Thread.Sleep(2000);
                        Console.Clear();
                        goto st;
                    }

                    string st_login = "", st_pas = "";

                r1: Console.Write("Введите логин студента: ");
                    st_login = Console.ReadLine();
                    if (st_login == "" || st_login == " " || st_login == null || Regex.IsMatch(st_login, @"^[а-яА-Я]+$") || st_login.Contains(" ") || st_login.Length < 2)
                    {
                        Console.WriteLine("Error!");
                        Thread.Sleep(2000);
                        goto r1;
                    }

                    DirectoryInfo dir2 = new DirectoryInfo(path11 + @"\Данные\Студенты");
                    foreach (var item in dir2.GetFiles())
                    {
                        if (st_login + ".dat" == item.Name)
                        {
                            Console.WriteLine("Логин уже существует!!");
                            goto r1;
                        }
                    }
                    dir2 = new DirectoryInfo(path11 + @"\Данные\Преподаватели");
                    foreach (var item in dir2.GetFiles())
                    {
                        if (st_login + ".dat" == item.Name)
                        {
                            Console.WriteLine("Логин уже существует!!");
                            goto r1;
                        }
                    }


                r2: Console.Write("Введите пароль студента: ");
                    st_pas = Console.ReadLine();
                    if (st_pas == "" || st_pas == " " || st_pas == null || Regex.IsMatch(st_pas, @"^[а-яА-Я]+$") || st_pas.Contains(" ") || st_pas.Length < 5)
                    {
                        Console.WriteLine("Error!");
                        Thread.Sleep(2000);
                        goto r2;
                    }
                    if (st_pas.Contains(" "))
                    {
                        Console.WriteLine("Error!");
                        Thread.Sleep(2000);
                        goto r2;
                    }

                    string ima1 = "", gr1 = "", fam1 = "", otch1 = "";
                    i = 0;
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Студенты\" + red_login+ ".dat", FileMode.Open));
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        if (i == 0)
                            gr1 = name;
                        if (i == 1)
                            fam1 = name;
                        if (i == 2)
                            ima1 = name;
                        if (i == 3)
                            otch1 = name;
                        i++;
                    }
                    reader.Close();

                    Directory.Delete(path11 + @"\Данные\Журналы\" + gr1 + @"\" + fam1 + " " + ima1 + " " + otch1);
                    System.IO.File.Delete(path11 + @"\Данные\Студенты\" + red_login + ".dat");
                    if (File.Exists(path11 + @"\Данные\Студенты\ДР\" + red_login + ".dat"))
                        System.IO.File.Delete(path11 + @"\Данные\Студенты\ДР\" + red_login + ".dat");

                    Directory.CreateDirectory(path11 + @"\Данные\Журналы\" + gr + @"\" + fam + " " + ima + " " + otch);
                    BinaryWriter writer = new BinaryWriter(File.Open(path11 + @"\Данные\Студенты\" + st_login + ".dat", FileMode.OpenOrCreate));
                    writer.Write(gr);
                    writer.Write(fam);
                    writer.Write(ima);
                    writer.Write(otch);
                    writer.Write(st_login);
                    writer.Write(st_pas);
                    writer.Close();
                }
            }
        }

        public void Journal()
        {
            string path11 = Directory.GetCurrentDirectory();
        again:
            Console.WriteLine("Группы: ");
            DirectoryInfo dirs1 = new DirectoryInfo(path11 + @"\Данные\Журналы\");
            int j = 1;
            string[] nums3 = new string[100];
            foreach (var item in dirs1.GetDirectories())
            {
                nums3[j] = item.Name;
                Console.WriteLine(" " + nums3[j]);
                j++;
            }
            Console.WriteLine();
            string gr = "";
            if (j == 1)
            {
                Console.WriteLine("Нет групп!");
                Thread.Sleep(2000);
                Admin_panel();
            }
            if (j > 1)
            {
                Console.Write("Введите группу, журнал которой надо отобразить: ");
                gr = Console.ReadLine();
                if (!System.IO.Directory.Exists(path11 + @"\Данные\Журналы\" + gr))
                {
                    Console.WriteLine("Данная учебная группа не существет");
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto again;
                }
            }

            Console.Clear();
            Console.WriteLine("Студенты группы:");
            int[] checker3 = new int[100];
            student: string[] nums = new string[35];
            DirectoryInfo dir = new DirectoryInfo(path11 + @"\Данные\Журналы\"+gr);
            j = 1;
            foreach (var item in dir.GetDirectories())
            {
                nums[j] = item.Name;
                Console.WriteLine(j+") "+nums[j]);
                checker3[j] = j;
                j++;
            }
            int j1 = 0;
            if (j==1)
            {
                Console.WriteLine("У данной учебной группы нет студентов!");
                Thread.Sleep(2000);
                Admin_panel();
            }
            if (j > 1)
            {
                Console.Write("Введите номер студента: ");
                j1 = Convert.ToInt32(Console.ReadLine());
                if (nums[j1] == "" || nums[j1] == null)
                {
                    Console.WriteLine("Error!");
                    goto student;
                }
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

            /*----*/
            stud_dis: Console.Clear();
            string[] nums2 = new string[35];
            Console.WriteLine("Выберите дициплину:");
            j = 1;

            DirectoryInfo dir1 = new DirectoryInfo(path11 + @"\Данные\Журналы\" + gr);
            foreach (var item in dir1.GetFiles())
            {
                nums2[j] = item.Name;
                Console.WriteLine(j + ") " + Path.GetFileNameWithoutExtension(nums2[j]));
                checker3[j] = j;
                j++;
            }
            if (j==1)
            {
                Console.WriteLine("У данной учебной группы нет дисциплин!");
                Thread.Sleep(2000);
                Admin_panel();
            }
            int j2 = 0;
            if (j > 1)
            {
                Console.Write("Выбор: ");
                j2 = Convert.ToInt32(Console.ReadLine());

                flag1 = false;
                for (int i1 = 0; i1 < j; ++i1)
                {
                    if (checker3[i1] == j2 && checker3[i1] != 0)
                    {
                        flag1 = true;
                    }
                }

                if (flag1 == false)
                {
                    Console.WriteLine("Error");
                    Thread.Sleep(2000);
                    goto stud_dis;
                }
            }

        /*-------*/
        syt:
            Console.Clear();
            Console.WriteLine("Выберите:");
            Console.WriteLine("1. Просмотреть оценки студента по всем дисциплинам");
            Console.WriteLine("2. Поставить новую оценку студенту по дисциплине "+ Path.GetFileNameWithoutExtension(nums2[j2]));
            Console.WriteLine("3. Изменить оценку студента");
            Console.Write("Выбор: ");
            string vibor2 = Console.ReadLine();

            if (vibor2 == "1")
            {
                Console.Clear();
                Console.WriteLine("Оценки студента: " + nums[j1]);
                string path5 = path11 + @"\Данные\Журналы\" + gr + @"\" + nums[j1];
                int j3 = 0;
                string[] nums5 = new string[100];
                DirectoryInfo dir5 = new DirectoryInfo(path5);

                foreach (var item in dir5.GetFiles())
                {
                    nums5[j3] = item.Name;
                    j3++;
                }

                string otbr1 = "", otbr2 = "", otbr3 = "";

                int i = 0;
                if (j3 != 0)
                {
                hhj:
                    BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Журналы\" + gr + @"\" + nums[j1] + @"\" + nums5[i], FileMode.Open));
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
                            Thread.Sleep(10000);
                            Console.Clear();
                            Admin_panel();
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
                    Thread.Sleep(3000);
                    Console.Clear();
                    Admin_panel();
                }
            }

            if (vibor2=="2")
            {
                starty:
                Console.Clear();
                Console.Write("Введите оценку: ");
                string otchenka = Convert.ToString(Console.ReadLine());
                
                if (otchenka == "2"  || otchenka == "3" || otchenka == "4" || otchenka == "5")
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
                Console.WriteLine("Вы поставили оценку " + otchenka + " по дисциплине " + Path.GetFileNameWithoutExtension(nums2[j2]) + " за " + data+" студенту "+ nums[j1]);

                string path2 = path11 + @"\Данные\Журналы\"+gr+@"\"+nums[j1]+@"\"+data+" "+ Path.GetFileNameWithoutExtension(nums2[j2])+".dat";
                if (File.Exists(path2))
                {
                    File.Delete(path2);
                    BinaryWriter writer = new BinaryWriter(File.Open(path2, FileMode.OpenOrCreate));
                    writer.Write(Path.GetFileNameWithoutExtension(nums2[j2]));
                    writer.Write(otchenka);
                    writer.Write(data);
                    writer.Close();
                    Thread.Sleep(4500);
                    Admin_panel();
                }
                if (!File.Exists(path2))
                {
                    BinaryWriter writer = new BinaryWriter(File.Open(path2, FileMode.OpenOrCreate));
                    writer.Write(Path.GetFileNameWithoutExtension(nums2[j2]));
                    writer.Write(otchenka);
                    writer.Write(data);
                    writer.Close();
                    Thread.Sleep(4500);
                    Admin_panel();
                }
            }

            if (vibor2 == "3")
            {
                Console.Clear();
                Console.WriteLine("Оценки студента: " + nums[j1]);
                string path5 = path11 + @"\Данные\Журналы\" + gr + @"\" + nums[j1];
                int j3 = 0;
                string[] nums5 = new string[100];
                DirectoryInfo dir5 = new DirectoryInfo(path5);

                foreach (var item in dir5.GetFiles())
                {
                    nums5[j3] = item.Name;
                    j3++;
                }

                string otbr1 = "", otbr2 = "", otbr3 = "";

                int i = 0; 
                int[] checker1 = new int[100];
                int g2 = 0;
                hhj:
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\Данные\Журналы\" + gr + @"\" + nums[j1] + @"\" + nums5[i], FileMode.Open));
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
                        Console.WriteLine(i+") По дисциплине " + otbr1 + " оценка " + otbr2 + " за " + otbr3);
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

                        flag1 = false;
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

                        System.IO.File.Delete(path11 + @"\Данные\Журналы\" + gr + @"\" + nums[j1]+@"\"+nums5[vibb]);

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
                        Console.WriteLine("Вы поставили оценку " + otchenka + " по дисциплине " + Path.GetFileNameWithoutExtension(nums2[j2]) + " за " + data + " студенту " + nums[j1]);

                        string path2 = path11 + @"\Данные\Журналы\" + gr + @"\" + nums[j1] + @"\" + data + " " + Path.GetFileNameWithoutExtension(nums2[j2]) + ".dat";
                        if (File.Exists(path2))
                        {
                            File.Delete(path2);
                            BinaryWriter writer = new BinaryWriter(File.Open(path2, FileMode.OpenOrCreate));
                            writer.Write(Path.GetFileNameWithoutExtension(nums2[j2]));
                            writer.Write(otchenka);
                            writer.Write(data);
                            writer.Close();
                            Thread.Sleep(4500);
                            Admin_panel();
                        }
                        if (!File.Exists(path2))
                        {
                            BinaryWriter writer = new BinaryWriter(File.Open(path2, FileMode.OpenOrCreate));
                            writer.Write(Path.GetFileNameWithoutExtension(nums2[j2]));
                            writer.Write(otchenka);
                            writer.Write(data);
                            writer.Close();
                            Thread.Sleep(4500);
                            Admin_panel();
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
        }
    }
}
