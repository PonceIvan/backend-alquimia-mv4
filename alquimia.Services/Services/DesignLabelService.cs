using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using alquimia.Services.Services.Models;
using System.Drawing.Imaging;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using static System.Net.Mime.MediaTypeNames;

namespace alquimia.Services.Services
{
    public class DesignLabelService
    {
        //public static byte[] CrearPdfDesdeDesign(DesignDTO dto)
        //{
        //    var doc = new PdfDocument();
        //    var page = doc.AddPage();
        //    var gfx = XGraphics.FromPdfPage(page);
        //    var font = new XFont("Verdana", 12);

        //    int y = 40;
        //    gfx.DrawString("Diseño personalizado", new XFont("Verdana", 18, XFontStyle.Bold), XBrushes.Black, new XPoint(40, y));
        //    y += 40;

        //    gfx.DrawString($"Texto: {dto.Text}", font, XBrushes.Black, new XPoint(40, y));
        //    y += 20;
        //    gfx.DrawString($"Tipografía: {dto.Typography}", font, XBrushes.Black, new XPoint(40, y));
        //    y += 20;
        //    gfx.DrawString($"Color del texto: {dto.TextColor}", font, XBrushes.Black, new XPoint(40, y));
        //    y += 20;
        //    gfx.DrawString($"Forma de la etiqueta: {dto.Shape}", font, XBrushes.Black, new XPoint(40, y));
        //    y += 20;
        //    gfx.DrawString($"Color de la etiqueta: {dto.LabelColor}", font, XBrushes.Black, new XPoint(40, y));
        //    y += 20;
        //    gfx.DrawString($"Volumen: {dto.Volume} ml", font, XBrushes.Black, new XPoint(40, y));
        //    if (!string.IsNullOrEmpty(dto.Image))
        //    {
        //        try
        //        {
        //            byte[] imageBytes = Convert.FromBase64String(dto.Image);
        //            using (var ms = new MemoryStream(imageBytes))
        //            {
        //                XImage xImage = XImage.FromStream(ms);
        //                gfx.DrawImage(xImage, 40, y);
        //                y += xImage.PixelHeight / 2;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            gfx.DrawString($"Error al cargar la imagen: {ex.Message}", font, XBrushes.Red, new XPoint(40, y));
        //        }
        //    }

        //    using var stream = new MemoryStream();
        //    doc.Save(stream, false);
        //    return stream.ToArray(); 
        //}

    }
}
