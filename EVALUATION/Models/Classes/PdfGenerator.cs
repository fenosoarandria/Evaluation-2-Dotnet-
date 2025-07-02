using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.IO.Image;

public class PdfGenerator
{

    public static byte[] GenererChampions(List<ClassementGeneralView> classement)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            PdfWriter writer = new PdfWriter(memoryStream);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            Color headerColor = new DeviceRgb(0, 51, 102);
            Color lightGray = new DeviceRgb(240, 240, 240);
           

        Image img = new Image(ImageDataFactory.Create("D:/ITU/S6/Mr Rojo/EVALUATION 2/EVALUATION/wwwroot/assets/img/champion.png"))
            .SetWidth(150) // Définit la largeur de l'image à 200 points
            .SetHeight(150)
            .SetHorizontalAlignment(HorizontalAlignment.RIGHT);
            document.Add(img);

        Image img2 = new Image(ImageDataFactory.Create("D:/ITU/S6/Mr Rojo/EVALUATION 2/EVALUATION/wwwroot/assets/img/champion.png"))
            .SetWidth(150) // Définit la largeur de l'image à 200 points
            .SetHeight(150)
            .SetMarginTop(-140)
            .SetHorizontalAlignment(HorizontalAlignment.LEFT);
            document.Add(img2);

           
            // Adding the invoice title
            document.Add(new Paragraph("RUNNING")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(boldFont)
                .SetMarginTop(-100)
                .SetFontSize(45)
                .SetMarginBottom(10));
                   // Adding the invoice title
            document.Add(new Paragraph("CHAMPIONS")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(boldFont)
                .SetMarginTop(20)
                .SetFontSize(50)
                .SetMarginBottom(10));
            document.Add(new Paragraph("TRACK & FIELD CERTIFICATE")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(boldFont)
                .SetFontSize(15)
                .SetMarginBottom(10));
            document.Add(new Paragraph("TEAM "+classement[0]?.Equipes?.Nom)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(boldFont)
                .SetFontSize(30)
                .SetMarginBottom(10));
            document.Add(new Paragraph(classement[0]?.Point.ToString() +"points")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(boldFont)
                .SetFontSize(20)
                .SetMarginBottom(10));
        // Ajout du texte en bas
        document.Add(new Paragraph("Congratulations on your team's performance!")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(font)
            .SetFontSize(20)
            .SetMarginTop(30)
            .SetMarginBottom(10));

        // Ajout de la signature
        document.Add(new Paragraph("Signature: ")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(font)
            .SetFontSize(10)
            .SetMarginTop(10)
            .SetMarginLeft(-150)
            .SetMarginBottom(10));
            // Ajout de la signature
        document.Add(new Paragraph($"Distance: {classement[0]?.Etapes?.Longueur} Km")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(font)
            .SetFontSize(10)
            .SetMarginTop(-25)
            .SetMarginLeft(100)
            .SetMarginBottom(10));
        document.Add(new Paragraph("Time: ")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(font)
            .SetFontSize(10)
            .SetMarginTop(-8)
            .SetMarginLeft(85)
            .SetMarginBottom(10));
        document.Add(new Paragraph("Pace: ")
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(font)
            .SetFontSize(10)
            .SetMarginTop(-5)
            .SetMarginLeft(90)
            .SetMarginBottom(10));
        document.Add(new Paragraph("Date: "+DateTime.Now.ToString("dd-MM-yyyy"))
            .SetTextAlignment(TextAlignment.CENTER)
            .SetFont(font)
            .SetFontSize(10)
            .SetMarginTop(-67)
            .SetMarginLeft(350)
            .SetMarginBottom(10));

        document.Close();

        return memoryStream.ToArray();
 
         
           
        }
    }

    private static Cell CreateHeaderCell(string content, Color backgroundColor, PdfFont font)
    {
        return new Cell()
            .Add(new Paragraph(content).SetFont(font).SetFontSize(12).SetFontColor(ColorConstants.WHITE))
            .SetBackgroundColor(backgroundColor)
            .SetTextAlignment(TextAlignment.CENTER);
    }

    private static Cell CreateCell(string content, PdfFont font)
    {
        return new Cell()
            .Add(new Paragraph(content).SetFont(font).SetFontSize(10))
            .SetTextAlignment(TextAlignment.CENTER);
    }

    public static string FormatNumberWithSpaces(double? number)
    {
        if (number == null)
            return string.Empty;
        return number.Value.ToString("N0", new System.Globalization.CultureInfo("fr-FR")).Replace(",", "\u00A0");
    }
}
