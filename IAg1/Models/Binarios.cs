using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace IAg1.Models
{
    public class Binarios
    {
        #region BListFinal
        public static float[,] matriz1 = new float[100, 100];
        public static float[,] matriz2 = new float[100, 100];
        #endregion
        public static float[,] pesos = new float[100, 100];
        public static float v = 1;
        public static float soma;
        public static float w0 = 0;
        
        public static void testando()
        {
            #region ListasBinarias
            List<float> preto = new List<float>();
            List<float> branco = new List<float>();
            #endregion

            #region Fotos
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\temp\doenca1\t1.png";
                Bitmap image = new Bitmap(path, true);

            
            float largura = image.Height / 100;
            float altura = image.Width / 100;

            float grayScale = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int cc = 0; cc < 100; cc++)
                {
                for (int x = (i*(int)altura); x < ((i+1)*(int)altura); x++)
                {
                    for (int y = (cc * (int)largura); y < ((cc+1)*(int)largura); y++)
                    {
                        Color originalColor = image.GetPixel(x, y);//pega a cor original
                            grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                            if (grayScale >= 0 && grayScale < 170)
                            {
                                preto.Add(0);
                            }
                            if (grayScale > 170)
                            {
                                branco.Add(1);
                            }
                            
                    }
                       
                }
                    if (preto.Count() > branco.Count())
                    {
                        matriz1[cc, i] = 0;
                    }
                    if (branco.Count() > preto.Count())
                    {
                        matriz1[cc, i] = 1;
                    }
                    preto.Clear();
                    branco.Clear();

                }
            }
            #endregion
        }
        public static void FotoArray()
        {
            #region ListasBinarias
            List<float> preto = new List<float>();
            List<float> branco = new List<float>();
            #endregion

            #region Fotos
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\temp\doenca1\n1.png";
            Bitmap image = new Bitmap(path, true);


            float largura = image.Height / 100;
            float altura = image.Width / 100;

            float grayScale = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int cc = 0; cc < 100; cc++)
                {
                    for (int x = (i * (int)altura); x < ((i + 1) * (int)altura); x++)
                    {
                        for (int y = (cc * (int)largura); y < ((cc + 1) * (int)largura); y++)
                        {
                            Color originalColor = image.GetPixel(x, y);//pega a cor original
                            grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                            if (grayScale >= 0 && grayScale < 170)
                            {
                                preto.Add(0);
                            }
                            if (grayScale > 170)
                            {
                                branco.Add(1);
                            }

                        }

                    }
                    if (preto.Count() > branco.Count())
                    {
                        matriz1[cc, i] = 0;
                    }
                    if (branco.Count() > preto.Count())
                    {
                        matriz1[cc, i] = 1;
                    }
                    preto.Clear();
                    branco.Clear();

                }
            }
            #endregion
            
                    #region FotoD1
                    preto.Clear();
                    branco.Clear();

                    altura = 0;
                    largura = 0;
                    string path11 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\temp\doenca2\d1.png";
                    Bitmap image11 = new Bitmap(path11, true);

                    altura = image11.Width / 100;
                    largura = image11.Height / 100;

            grayScale = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int cc = 0; cc < 100; cc++)
                {
                    for (int x = (i * (int)altura); x < ((i + 1) * (int)altura); x++)
                    {
                        for (int y = (cc * (int)largura); y < ((cc + 1) * (int)largura); y++)
                        {
                            Color originalColor = image11.GetPixel(x, y);//pega a cor original
                            grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                            if (grayScale >= 0 && grayScale < 170)
                            {
                                preto.Add(0);
                            }
                            if (grayScale > 170)
                            {
                                branco.Add(1);
                            }

                        }

                    }
                    if (preto.Count() > branco.Count())
                    {
                        matriz2[cc, i] = 0;
                    }
                    if (branco.Count() > preto.Count())
                    {
                        matriz2[cc, i] = 1;
                    }
                    preto.Clear();
                    branco.Clear();

                }
            }

            #endregion

            #region AtribuicaoAsListas


            #endregion

            //novo.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\temp\newn1.png");

            #region Pesos
            for (int i = 0; i < 100; i++)
            {
                for (int x = 0; x < 100; x++)
                {
                    pesos[i, x] = 0;
                }
            }
            #endregion

        }
        public static void ExecutarAprendizado()
        {
            #region SaidasEsperadas1
            float y1 = 1;
            #endregion
            #region SaidasEsperadas2
            float y6 = 0;
            #endregion
            float y = 0;
            int cont = 0;
            soma = 0;
            //REGION TREINAMENTO
            while (cont != 2)
            {
                cont = 0;
                #region X=1
                for (int i = 0; i < 100; i++)
                {
                    for (int x = 0; x < 100; x++)
                    {
                        soma += (matriz1[i,x] * pesos[i,x]);
                    }
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
                    for (int i = 0; i < 100; i++)
                    {
                        for (int x = 0; x < 100; x++)
                        {
                            pesos[i,x] = pesos[i,x] + 1 * (y1 - y) * matriz1[i,x];
                        }
                    }
                    w0 = w0 + 1 * (y1 - y) * v;
                }
                else
                {
                    cont++;
                }
                #endregion
                #region X=6
                for (int i = 0; i < 100; i++)
                {
                    for (int x = 0; x < 100; x++)
                    {
                        soma += (matriz2[i, x] * pesos[i, x]);
                    }
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
                if (y != y6)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        for (int x = 0; x < 100; x++)
                        {
                            pesos[i, x] = pesos[i, x] + 1 * (y6 - y) * matriz2[i, x];
                        }
                    }
                    w0 = w0 + 1 * (y6 - y) * v;
                }
                else
                {
                    cont++;
                }
                #endregion
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
        public static float Testar(float[,] ListaBinaria)
        {
            float y = 0;
            float soma = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int x = 0; x < 100; x++)
                {
                    soma += (ListaBinaria[i,x] * pesos[i,x]);
                }
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