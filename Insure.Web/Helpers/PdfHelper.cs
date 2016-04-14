using SelectPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Insure.Web.Helpers
{
    public static class PdfHelper
    {
        public static string ConversionUrl { get; set; }
        public static ActionResult Convert(FormCollection collection)
        {
            HtmlToPdf converter = new HtmlToPdf();

            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
            converter.Options.WebPageWidth = 1202;
            converter.Options.WebPageHeight = 0;
            PdfDocument doc = converter.ConvertUrl(ConversionUrl);

            byte[] pdf = doc.Save();
            doc.Close();

            FileResult fileResult = new FileContentResult(pdf, "application/pdf");
            fileResult.FileDownloadName = "Document.pdf";
            return fileResult;
        }
    }
}