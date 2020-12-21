using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace eljur
{
    class Program
    {
        static void Main(string[] args)
        {
            string path11 = Directory.GetCurrentDirectory();
            if (!Directory.Exists(path11 + @"\Данные"))
                Directory.CreateDirectory(path11 + @"\Данные");
            if (!Directory.Exists(path11 + @"\Данные\Журналы"))
                Directory.CreateDirectory(path11 + @"\Данные\Журналы");
            if (!Directory.Exists(path11 + @"\Данные\Преподаватели"))
                Directory.CreateDirectory(path11 + @"\Данные\Преподаватели");
            if (!Directory.Exists(path11 + @"\Данные\Преподаватели\ДР"))
                Directory.CreateDirectory(path11 + @"\Данные\Преподаватели\ДР");
            if (!Directory.Exists(path11 + @"\Данные\Студенты"))
                Directory.CreateDirectory(path11 + @"\Данные\Студенты");
            if (!Directory.Exists(path11 + @"\Данные\Студенты\ДР"))
                Directory.CreateDirectory(path11 + @"\Данные\Студенты\ДР");
            MenuVibor lol = new MenuVibor();
            lol.Vibor();
        }
    }
}
