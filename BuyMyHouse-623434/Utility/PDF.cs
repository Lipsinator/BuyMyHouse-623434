using Domain.DBModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace Utility
{
    public class PDF
    {
        public static byte[] CreatePDF(BuyerInfo buyerInfo, float ammountToBorrow)
        {
            Document doc = new Document(PageSize.A4, 50, 50, 50, 50);

            using (MemoryStream output = new MemoryStream())
            {
                PdfWriter wri = PdfWriter.GetInstance(doc, output);
                doc.Open();

                Paragraph header = new Paragraph("Your Mortgage Application for BuyMyHouse") { Alignment = Element.ALIGN_CENTER };
                Paragraph paragraph = new Paragraph($"Dear mr/ mrs {buyerInfo.LastName},");
                Phrase phrase = new Phrase($"The maximum mortgage you can claim is: \u20AC {ammountToBorrow.ToString()}.");

                doc.Add(header);
                doc.Add(paragraph);
                doc.Add(phrase);

                doc.Close();
                return output.ToArray();
            }
        }
    }
}
