using System;
using System.Collections.Generic;
using System.Drawing;
using System.Collections.Generic;
using System.Threading;

namespace ADproj
{
    public class PixelVertical
    {
        public Tuple<int, int> father;
        public int r, g, b;
        public double energy;
    }
    public class vertical
    {
        public void VerticalRemove(int verticalremove, string name)
        {
            var myBitmap = new Bitmap(@"D:\university\AD entezari\ADproj\image\" + name + @"result\horizontalfinalImage.jpg");

            int width = myBitmap.Width;
            int height = myBitmap.Height;
            double hold = double.MaxValue;
            int holdindx = 0;


            List<Tuple<int, int>> tuples = new List<Tuple<int, int>>();

            Pixel[,] pixel = new Pixel[width, height];
            double[,] arr = new double[width, height];

            double energy = 0;

            Color color = Color.Red;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color pixelColor = myBitmap.GetPixel(i, j);
                    pixel[i, j] = new Pixel
                    {
                        r = int.Parse(pixelColor.R.ToString()),
                        g = int.Parse(pixelColor.G.ToString()),
                        b = int.Parse(pixelColor.B.ToString())
                    };
                }
            }

            for (int counter = 0; counter < verticalremove; counter++)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height - 1; j++)
                    {
                        if ((i == 0 || i == width - 1) && (j == 0 || j == height - 1))
                        {
                            energy = 0;
                        }
                        else
                        {
                            if (i == 0 || i == width - 1)
                            {
                                energy = Math.Pow(pixel[i, j - 1].r - pixel[i, j + 1].r, 2) +
                                    Math.Pow(pixel[i, j - 1].g - pixel[i, j + 1].g, 2) +
                                    Math.Pow(pixel[i, j - 1].b - pixel[i, j + 1].b, 2);
                            }
                            else
                            {
                                if (j == 0 || j == height - 1)
                                {
                                    energy = Math.Pow(pixel[i - 1, j].r - pixel[i + 1, j].r, 2) +
                                         Math.Pow(pixel[i - 1, j].g - pixel[i + 1, j].g, 2) +
                                         Math.Pow(pixel[i - 1, j].b - pixel[i + 1, j].b, 2);
                                }
                                else
                                {
                                    energy = Math.Pow(pixel[i - 1, j].r - pixel[i + 1, j].r, 2) +
                                         Math.Pow(pixel[i - 1, j].g - pixel[i + 1, j].g, 2) +
                                         Math.Pow(pixel[i - 1, j].b - pixel[i + 1, j].b, 2);
                                    energy += Math.Pow(pixel[i, j - 1].r - pixel[i, j + 1].r, 2) +
                                        Math.Pow(pixel[i, j - 1].g - pixel[i, j + 1].g, 2) +
                                        Math.Pow(pixel[i, j - 1].b - pixel[i, j + 1].b, 2);
                                }
                            }
                        }
                        pixel[i, j].energy = Math.Sqrt(energy);
                    }

                }
                for (int i = 0; i < width; i++)
                    arr[i, 0] = pixel[i, 0].energy;
                for (int j = 1; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        if (i == 0)
                        {
                            if (arr[i + 1, j - 1] < arr[i, j - 1])
                            {
                                pixel[i, j].father = Tuple.Create(i + 1, j - 1);
                                arr[i, j] = pixel[i, j].energy + arr[i + 1, j - 1];
                            }
                            else
                            {
                                pixel[i, j].father = Tuple.Create(i, j - 1);
                                arr[i, j] = pixel[i, j].energy + arr[i, j - 1];
                            }
                        }
                        else
                        {
                            if (i == width - 1)
                            {
                                if (arr[i - 1, j - 1] < arr[i, j - 1])
                                {
                                    pixel[i, j].father = Tuple.Create(i - 1, j - 1);
                                    arr[i, j] = pixel[i, j].energy + arr[i - 1, j - 1];
                                }
                                else
                                {
                                    pixel[i, j].father = Tuple.Create(i, j - 1);
                                    arr[i, j] = pixel[i, j].energy + arr[i, j - 1];
                                }
                            }
                            else
                            {
                                if (arr[i - 1, j - 1] < Math.Min(arr[i, j - 1], arr[i + 1, j - 1]))
                                {
                                    pixel[i, j].father = Tuple.Create(i - 1, j - 1);
                                    arr[i, j] = pixel[i, j].energy + arr[i - 1, j - 1];
                                }
                                else
                                {
                                    if (arr[i, j - 1] < Math.Min(arr[i - 1, j - 1], arr[i + 1, j - 1]))
                                    {
                                        pixel[i, j].father = Tuple.Create(i, j - 1);
                                        arr[i, j] = pixel[i, j].energy + arr[i, j - 1];
                                    }
                                    else
                                    {
                                        pixel[i, j].father = Tuple.Create(i + 1, j - 1);
                                        arr[i, j] = pixel[i, j].energy + arr[i + 1, j - 1];
                                    }
                                }
                            }
                        }
                    }
                }
                
                double[] holdarr = new double[width];
                for (int i = 0; i < width; i++)
                {
                    holdarr[i] = arr[i, height - 1] ;
                }
                Array.Sort(holdarr);
                double max = 0;
                hold = holdarr[0];

                for (int i = 0; i < width; i++)
                {
                    if(hold == arr[i, height - 1])
                        holdindx = i;
                }

                for(int i = 0; i < width; i++)
                {
                    for(int j = 0; j < height; j++)
                    {
                        if (pixel[i, j].energy > max)
                            max = pixel[i, j].energy;
                    }
                }

                tuples.Add(Tuple.Create(holdindx, height - 1));
                for (int i = 0; i < height - 1; i++)
                {
                    tuples.Add(Tuple.Create(pixel[tuples[tuples.Count - 1].Item1, tuples[tuples.Count - 1].Item2].father.Item1,
                        pixel[tuples[tuples.Count - 1].Item1, tuples[tuples.Count - 1].Item2].father.Item2));
                }

                color = Color.Red;
                for (int i = 0; i < height; i++)
                    myBitmap.SetPixel(tuples[i].Item1, tuples[i].Item2, color);
                Bitmap saveBitmap = new Bitmap(width, height);

                for(int i = 0; i < width; i++)
                {
                    for(int j = 0; j < height; j++)
                    {
                        color = Color.FromArgb((int)(pixel[i, j].energy * 255.9 / max ), (int)(pixel[i, j].energy * 255.9 / max), (int)(pixel[i, j].energy * 255.9 / max));
                        saveBitmap.SetPixel(i, j, color);
                    }
                }
                color = Color.Red;
                for (int i = 0; i < height; i++)
                    saveBitmap.SetPixel(tuples[i].Item1, tuples[i].Item2, color);

                saveBitmap.Save(@"D:\university\AD entezari\ADproj\Create\" + name + @"\verticalWithRed" + counter + ".jpg");

                int test = 0;
                int holdx = 0, holdy = 0;
                for (int i = 0; i < height; i++)
                {
                    holdx = tuples[i].Item1;
                    holdy = tuples[i].Item2;

                    for (int j = holdx; j < width - 1; j++)
                    {

                        pixel[j, holdy].b = pixel[j + 1, holdy].b;
                        pixel[j, holdy].r = pixel[j + 1, holdy].r;
                        pixel[j, holdy].g = pixel[j + 1, holdy].g;
                        pixel[j, holdy].father = pixel[j + 1, holdy].father;
                        pixel[j, holdy].energy = pixel[j + 1, holdy].energy;


                        color = Color.FromArgb(pixel[j, holdy].r, pixel[j, holdy].g, pixel[j, holdy].b);
                        myBitmap.SetPixel(j, holdy, color);
                    }

                }

                width--;
                /*myBitmap.Save(@"D:\university\AD entezari\ADproj\Create\verticalnewImage" + counter + ".png");*/
                tuples = new List<Tuple<int, int>>();
                if (counter == 0)
                    Console.WriteLine("One vertical pixel has been removed");
                else
                    Console.WriteLine(counter + 1 + " vertical pixels has been removed");
            }
            Bitmap finalBitmap = new Bitmap(width, height);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                    finalBitmap.SetPixel(i, j, myBitmap.GetPixel(i, j));
            }
            Console.WriteLine("vertical Done");
            finalBitmap.Save(@"D:\university\AD entezari\ADproj\image\" + name + @"result\" + "finalImage.jpg");
        }
    }
}




