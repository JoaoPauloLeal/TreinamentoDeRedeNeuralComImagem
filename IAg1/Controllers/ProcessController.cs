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
            List<float> blist = new List<float>();
            List<float> pesos = new List<float>();

            return View();
        }
        public ActionResult Checar()
        {
            #region TestarImagem

            List<float> blist = new List<float>();
            List<float> pesos = new List<float>();
            try
            {
            string path = Path.Combine(Server.MapPath("~/App_Data"), Path.GetFileName(NomeImagem));
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