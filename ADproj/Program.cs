using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections.Generic;
using System.Threading;

namespace ADproj
{

    public class Program
    {  
        public static void Main(string[] args)
        {
            while (true)
            {
                if (!System.IO.Directory.Exists(@"D:\university\AD entezari\ADproj\Create"))
                    System.IO.Directory.CreateDirectory(@"D:\university\AD entezari\ADproj\Create");

                Console.WriteLine("Enter number of horizontal pixels that should be removed : ");
                int horizontalremove = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter number of vertical pixels that should be removed : ");
                int verticalremove = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter your image name : ");
                string name = Console.ReadLine();

                if (System.IO.Directory.Exists(@"D:\university\AD entezari\ADproj\Create\" + name))
                    System.IO.Directory.Delete(@"D:\university\AD entezari\ADproj\Create\" + name, true);

                System.IO.Directory.CreateDirectory(@"D:\university\AD entezari\ADproj\Create\" + name);

                var myBitmap = new Bitmap(@"D:\university\AD entezari\ADproj\image\" + name + ".jpg");

                horizontal horizontal = new horizontal();
                horizontal.horizontalRemove(horizontalremove, myBitmap, name);

                vertical vertical = new vertical();
                vertical.VerticalRemove(verticalremove, name);

                Console.WriteLine("All done\n");

                Thread.Sleep(1000);
                Console.WriteLine("If you wanna exit press 1,else press ant other keys");
                if (int.Parse(Console.ReadLine()) == 2)
                    break;
                Console.Clear();
            }
        }
    }
}
