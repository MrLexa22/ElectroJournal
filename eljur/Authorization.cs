using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Reflection.Emit;
using System.Net.Sockets;
using System.Security.Principal;

namespace eljur
{
    public class Authorization
    {
        string path11 = Directory.GetCurrentDirectory();
        public string login;
        public void Lolkin()
        {
            again: Console.Clear();
            Console.Write("Введите логин: ");
            login = Console.ReadLine();

            if (login != "admin")
            {
                student stud = new student();
                stud.Authentication(login);
            }

            if (login == "admin")
            {
                string name = "";
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\admin_pas.dat", FileMode.Open));
                while (reader.PeekChar() > -1)
                {
                    name = reader.ReadString();
                }
                Console.Write("Введите пароль: ");
                string parol = Console.ReadLine();
                if (parol == name)
                {
                    Console.WriteLine("Вы вошли как Администратор!");
                    reader.Close();
                    Admin adm = new Admin();
                    adm.Admin_panel();
                }
                if (parol != name)
                {
                    Console.WriteLine("Вы ввели неверный пароль Администратора, повторите ввод");
                    reader.Close();
                    Thread.Sleep(2000);
                    goto again;
                }
            }
        }

        /*public void Loging()
        {
            //again:
            /*Console.Clear();
            Console.Write("Введите логин: ");
            string login = Console.ReadLine();
            
            if (login != "admin")
            {
                //return login;
                student stud = new student();
                stud.Authentication();
            }*/

            /*if (login == "admin")
            {
                string name = "";
                BinaryReader reader = new BinaryReader(File.Open(path11 + @"\admin_pas.dat", FileMode.Open));
                while (reader.PeekChar() > -1)
                {
                    name = reader.ReadString();
                }
                Console.Write("Введите пароль: ");
                string parol = Console.ReadLine();
                if (parol == name)
                {
                    Console.WriteLine("Вы вошли как Администратор!");
                    reader.Close();
                    Admin adm = new Admin();
                    adm.Admin_panel();
                }
                if (parol != name)
                {
                    Console.WriteLine("Вы ввели неверный пароль Администратора, повторите ввод");
                    reader.Close();
                    Thread.Sleep(2000);
                    goto again;
                }
            }*/
    }
}
