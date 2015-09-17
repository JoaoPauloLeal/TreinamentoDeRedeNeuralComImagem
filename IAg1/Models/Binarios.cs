using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace IAg1.Models
{
    public class Binarios
    {
        public static List<float> blistfinal1 = new List<float>();
        public static List<float> blistfinal2 = new List<float>();
        public static List<float> pesos = new List<float>();
        
        public static float v = 1;
        public static float soma;
        public static float w0 = 0;
        

        public static void FotoArray()
        {
            List<float> blist = new List<float>();
            List<float> blist2 = new List<float>();


            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\temp\n1.png";
                Bitmap image = new Bitmap(path, true);
                Bitmap novo = new Bitmap(image.Width, image.Height);
            
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        Color originalColor = image.GetPixel(x, y);//pega a cor original
                        int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                        if (grayScale <= 110 && grayScale > 0)
                        {
                            
                            blist.Add(0);
                        }
                        if (grayScale >= 110 && grayScale < 256)
                        {
                            blist.Add(1);
                        }
                        Color CorEmEscalaDeCinza = Color.FromArgb(grayScale, grayScale, grayScale);
                        novo.SetPixel(x, y, CorEmEscalaDeCinza);
                    }
                }

            string path2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\temp\n2.png";
            Bitmap image2 = new Bitmap(path2, true);
            Bitmap novo2 = new Bitmap(image2.Width, image2.Height);

            for (int x = 0; x < image2.Width; x++)
            {
                for (int y = 0; y < image2.Height; y++)
                {
                    Color originalColor = image2.GetPixel(x, y);//pega a cor original
                    int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                    if (grayScale < 110 && grayScale > 0)
                    {

                        blist2.Add(0);
                    }
                    if (grayScale > 110 && grayScale < 256)
                    {
                        blist2.Add(1);
                    }
                    Color CorEmEscalaDeCinza = Color.FromArgb(grayScale, grayScale, grayScale);
                    novo.SetPixel(x, y, CorEmEscalaDeCinza);
                }
            }


            //novo.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\temp\newn1.png");
            
            
                blistfinal1 = blist;
            
                blistfinal2 = blist2;
            
            for (int x = 0; x < 10000; x++)
            {
                pesos.Add(0);
            }
            
        }
        public static void ExecutarAprendizado()
        {
            float y1 = 1;
            float y2 = 0;
            float y = 0;
            int cont = 0;
            soma = 0;
            //REGION TREINAMENTO
            while (cont != 2)
            {
                
                cont = 0;
                //#region X=1
                for (int x = 0; x < 10000; x++)
                {
                    soma += (blistfinal1[x] * pesos[x]);
                }
                soma += (v * w0);

                if (soma <= 0)
                {
                    y = 0;
                }
                if (soma > 0)
                {
                    y = 1;
                }
                if (y != y1)
                {

                    for (int x = 0; x < 10000; x++)
                    {
                        pesos[x] = pesos[x] + 1 * (y1 - y) * blistfinal1[x];
                    }
                    w0 = w0 + 1 * (y1 - y) * v;
                }
                else
                {
                    cont++;
                }
                //#end region X=1
                //#region X=2
                for (int x = 0; x < 10000; x++)
                {
                    soma += (blistfinal2[x] * pesos[x]);
                }
                soma += (v * w0);

                if (soma <= 0)
                {
                    y = 0;
                }
                if (soma > 0)
                {
                    y = 1;
                }
                if (y != y2)
                {
                    for (int x = 0; x < 10000; x++)
                    {
                        pesos[x] = pesos[x] + 1 * (y2 - y) * blistfinal2[x];
                    }
                    w0 = w0 + 1 * (y2 - y) * v;
                }
                else
                {
                    cont++;
                }
                //#end region X=2
            }
            //END REGION TREINAMENTO
        }
        public static List<float> FotoRetorna()
        {
            List<float> blist = new List<float>();
            List<float> pesos = new List<float>();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\temp\teste2.png";
            Bitmap image = new Bitmap(path, true);
            Bitmap novo = new Bitmap(image.Width, image.Height);

            Console.WriteLine(image.PixelFormat);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    /*Color clr = image.GetPixel(x, y);
                    Color newclr = Color.FromArgb(0, clr.B, 0);
                    image.SetPixel(x, y, newclr);*/
                    Color originalColor = image.GetPixel(x, y);//pega a cor original
                    int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                    if (grayScale < 110 && grayScale > -1)
                    {
                        blist.Add(0);
                    }
                    if (grayScale > 110 && grayScale < 256)
                    {
                        blist.Add(1);
                    }
                    Color CorEmEscalaDeCinza = Color.FromArgb(grayScale, grayScale, grayScale);
                    novo.SetPixel(x, y, CorEmEscalaDeCinza);
                    pesos.Add(0);
                }
            }
            novo.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\temp\newn1.png");
            return blist;
        }
        public static float Testar(List<float> ListaBinaria)
        {
            float y = 0;
            float soma = 0;

            for (int x = 0; x < 10000; x++)
            {
                soma += (ListaBinaria[x] * pesos[x]);
            }
            soma += (v * w0);

            if (soma <= 0)
            {
                y = 0;
            }
            if (soma > 0)
            {
                y = 1;
            }
            return y;
        }
    }
}