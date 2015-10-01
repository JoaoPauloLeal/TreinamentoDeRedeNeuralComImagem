using IAg1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IAg1.Controllers
{
    public class ProcessController : Controller
    {
        public static string NomeImagem;
        public ActionResult Upload()
        {
            Binarios.FotoArray();
            Binarios.ExecutarAprendizado();
            /*List<float> blist = new List<float>();
            List<float> pesos = new List<float>();*/

            return View();
        }
        public ActionResult Checar()
        {
            #region TestarImagem

            float[,] blist = new float[100, 100];
            List<float> p = new List<float>();
            List<float> b = new List<float>();
            try
            {
            string path = Path.Combine(Server.MapPath("~/App_Data"), Path.GetFileName(NomeImagem));
            Bitmap image = new Bitmap(path, true);
            Bitmap novo = new Bitmap(image.Width, image.Height);
            float altura = image.Width / 100;
            float largura = image.Height / 100;

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
                                    p.Add(0);
                                }
                                if (grayScale > 170)
                                {
                                    b.Add(1);
                                }

                            }

                        }
                        if (p.Count() > b.Count())
                        {
                            blist[cc, i] = 0;
                        }
                        if (b.Count() > p.Count())
                        {
                            blist[cc, i] = 1;
                        }
                        p.Clear();
                        b.Clear();

                    }
                }

                #endregion
                #region Teste-0e1-
                float retorno = Binarios.Testar(blist);
            if (retorno == 1)
            {
                ViewBag.Doenca = 1;
            }
            if (retorno == 0)
            {
                ViewBag.Doenca = 0;
            }
            #endregion
            }
            catch (Exception)
            {

                ViewBag.Doenca = -1;
            }
            return View();
        }
        [HttpPost]
        public FilePathResult Image()
        {
            string filename = Request.Url.AbsolutePath.Replace("/Upload/image", "");
            string contentType = "";
            var filePath = new FileInfo(Server.MapPath("~/App_Data") + filename);
            
            var index = filename.LastIndexOf(".") + 1;
            var extension = filename.Substring(index).ToUpperInvariant();

            // Fix for IE not handling jpg image types
            contentType = string.Compare(extension, "JPG") == 0 ? "image/jpeg" : string.Format("image/{0}", extension);

            return File(filePath.FullName, contentType);
        }
        [HttpPost]
        public ContentResult UploadFiles()
        {
            var r = new List<UploadFilesResult>();

            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;

                string savedFileName = Path.Combine(Server.MapPath("~/App_Data"), Path.GetFileName(hpf.FileName));
                hpf.SaveAs(savedFileName);

                r.Add(new UploadFilesResult()
                {
                    Name = hpf.FileName,
                    Length = hpf.ContentLength,
                    Type = hpf.ContentType
                });
            }
            NomeImagem = r[0].Name;
            return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", r[0].Length) + "\"}", "application/json");
        }
    }
}