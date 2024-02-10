using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.IO.Font.Constants;

namespace SMPT.Api.Services
{
    public class PdfService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PdfService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public PdfDocument GenerateEnglishCertificate(string studentName, string directorName, string grade)
        {
            string inputFile = "C:\\Apps\\Blazor\\Documents\\Templates\\English\\Template.pdf";
            string outputFile = "C:\\Apps\\Blazor\\Documents\\Tests\\AlumnoX.pdf";

            PdfReader reader = new(inputFile);
            PdfWriter writer = new(outputFile);
            PdfDocument pdf = new(reader, writer);

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            Document doc = new(pdf);

            doc.Add(BiuldParagraph(studentName, font, 14, 563));
            doc.Add(BiuldParagraph(directorName, font, 14, (float)606.4));

            doc.Close();

            return doc.GetPdfDocument();
        }

        private Paragraph BiuldParagraph(string text, PdfFont font, int fontSize, float y)
        {
            var p = new Paragraph(text);

            p.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            p.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
            p.SetRelativePosition(0, y, 0, 0);
            p.SetFont(font);
            p.SetFontSize(fontSize);

            return p;
        }
    }
}
