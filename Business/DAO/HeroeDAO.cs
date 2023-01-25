using Business.DTO;
using Business.Entity;
using Business.Interface;
using Business.Tool;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace Business.DAO
{
    public class HeroeDAO : IHeroe
    {
        List<Heroe> heroes = new List<Heroe>();

        public HeroeDAO()
        {
            heroes.Add(new Heroe()
            {
                Id = 1,
                Name = "Aquaman",
                Home = "DC",
                Appearance = new DateTimeOffset(new DateTime(1941, 11, 1)),
                Description = "El poder más reconocido de Aquaman es la capacidad telepática para comunicarse con la vida marina, la cual puede convocar a grandes distancias.",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\aquaman.png")                
                
            });
            heroes.Add(new Heroe()
            {
                Id = 2,
                Name = "Batman",
                Home = "DC",
                Appearance = new DateTimeOffset(new DateTime(1939, 5, 1)),
                Description = "Los rasgos principales de Batman se resumen en «destreza física, habilidades deductivas y obsesión». La mayor parte de las características básicas de los cómics han variado por las diferentes interpretaciones que le han dado al personaje.",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\batman.png")
                
            });
            heroes.Add(new Heroe()
            {
                Id = 3,
                Name = "Daredevil",
                Home = "Marvel",
                Appearance = new DateTimeOffset(new DateTime(1964, 1, 1)),
                Description = "Al haber perdido la vista, los cuatro sentidos restantes de Daredevil fueron aumentados por la radiación a niveles superhumanos, en el accidente que tuvo cuando era niño. A pesar de su ceguera, puede \"ver\" a través de un \"sexto sentido\" que le sirve como un radar similar al de los murciélagos.",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\daredevil.png")
            });
            heroes.Add(new Heroe()
            {
                Id = 4,
                Name = "Hulk",
                Home = "Marvel",
                Appearance = new DateTimeOffset(new DateTime(1962, 5, 1)),
                Description = "Su principal poder es su capacidad de aumentar su fuerza hasta niveles prácticamente ilimitados a la vez que aumenta su furia. Dependiendo de qué personalidad de Hulk esté al mando en ese momento (el Hulk Banner es el más débil, pero lo compensa con su inteligencia).",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\hulk.png")                                
            });
            heroes.Add(new Heroe()
            {
                Id = 5,
                Name = "Linterna Verde",
                Home = "DC",
                Appearance = new DateTimeOffset(new DateTime(1940, 6, 1)),
                Description = "Poseedor del anillo de poder que posee la capacidad de crear manifestaciones de luz sólida mediante la utilización del pensamiento. Es alimentado por la Llama Verde (revisada por escritores posteriores como un poder místico llamado Starheart), una llama mágica contenida en dentro de un orbe (el orbe era en realidad un meteorito verde de metal que cayó a la Tierra, el cual encontró un fabricante de lámparas llamado Chang).",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\linterna-verde.png")
            });
            heroes.Add(new Heroe()
            {
                Id = 6,
                Name = "Spider-Man",
                Home = "Marvel",
                Appearance = new DateTimeOffset(new DateTime(1962, 8, 1)),
                Description = "Tras ser mordido por una araña radiactiva, obtuvo los siguientes poderes sobrehumanos, una gran fuerza, agilidad, poder trepar por paredes. La fuerza de Spider-Man le permite levantar 10 toneladas o más. Gracias a esta gran fuerza Spider-Man puede realizar saltos íncreibles. Un \"sentido arácnido\", que le permite saber si un peligro se cierne sobre él, antes de que suceda. En ocasiones este puede llevar a Spider-Man al origen del peligro.",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\spiderman.png")
            });
            heroes.Add(new Heroe()
            {
                Id = 7,
                Name = "Wolverine",
                Home = "Marvel",
                Appearance = new DateTimeOffset(new DateTime(1974, 11, 1)),
                Description = "En el universo ficticio de Marvel, Wolverine posee poderes regenerativos que pueden curar cualquier herida, por mortal que ésta sea, además ese mismo poder hace que sea inmune a cualquier enfermedad existente en la Tierra y algunas extraterrestres . Posee también una fuerza sobrehumana, que si bien no se compara con la de otros superhéroes como Hulk, sí sobrepasa la de cualquier humano.",
                ImgBase64String = Useful.GetPngToBase64String($"{Useful.GetApplicationDirectory()}Contents\\IMG\\wolverine.png")
            });
        }

        public Heroe Select(int id)
        {
            Heroe heroe = heroes.FirstOrDefault(o => o.Id == id);
            return heroe;
        }

        public bool ExistByName(string name)
        {
            bool exist = heroes.Any(o => o.Name == name);
            return exist;
        }

        public Heroe Insert(HeroeInsertDTO heroeInsertDTO)
        {
            Heroe heroe = new Heroe()
            {
                Id = heroes.Last().Id + 1,
                Name = heroeInsertDTO.Name,
                Home = heroeInsertDTO.Home,
                Appearance = heroeInsertDTO.Appearance,
                Description = heroeInsertDTO.Description,
                ImgBase64String = heroeInsertDTO.ImgBase64String
            };
            heroes.Add(heroe);
            return heroe;
        }

        public bool Update(Heroe heroe)
        {
            Heroe heroeExist = heroes.FirstOrDefault(o => o.Id == heroe.Id);
            if (heroeExist != null)
            {
                Heroe entity = new Heroe()
                {
                    Id = heroeExist.Id,
                    Name = heroe.Name,
                    Home = heroe.Home,
                    Appearance = heroe.Appearance,
                    Description = heroe.Description,
                    ImgBase64String = heroe.ImgBase64String
                };
                heroes.Remove(heroeExist);
                heroes.Add(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            Heroe heroe = heroes.FirstOrDefault(o => o.Id == id);
            if (heroe != null)
            {
                heroes.Remove(heroe);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Heroe> List(HeroeListDTO heroeListDTO)
        {
            List<Heroe> listHeroe = heroes.OrderByDescending(o => o.Id).Skip((heroeListDTO.PageSize * (heroeListDTO.PageIndex - 1))).Take(heroeListDTO.PageSize).ToList();
            return listHeroe;
        }

        public long TotalRecords()
        {
            long totalRecords = heroes.LongCount();
            return totalRecords;
        }

        public bool ExistByNameAndNotSameEntity(HeroeExistByNameAndNotSameEntityDTO heroeExistByNameAndNotSameEntityDTO)
        {
            Heroe heroe = heroes.FirstOrDefault(o => o.Id != heroeExistByNameAndNotSameEntityDTO.Id && o.Name == heroeExistByNameAndNotSameEntityDTO.Name);
            if (heroe == null)
                return false;
            else
                return true;
        }

        public HeroeExcelDTO Excel()
        {
            HeroeExcelDTO heroeExcelDTO = null;
            MemoryStream memoryStream = null;
            SLDocument sLDocument = null;
            try
            {
                memoryStream = new MemoryStream();
                sLDocument = new SLDocument();
                sLDocument.RenameWorksheet(SLDocument.DefaultFirstSheetName, "Heroe");
                sLDocument.SetCellValue("B3", "Heroe");

                SLStyle sLStyleTitle = sLDocument.CreateStyle();
                sLStyleTitle.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
                sLStyleTitle.Font.Bold = true;
                sLStyleTitle.SetLeftBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleTitle.SetTopBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleTitle.SetRightBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleTitle.SetBottomBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLDocument.MergeWorksheetCells("B3", "D3", sLStyleTitle);
                sLDocument.MergeWorksheetCells("B4", "D4");                

                SLPicture sLPicture = new SLPicture(Useful.GetImagePath());
                sLPicture.SetPosition(4, 1);
                sLDocument.InsertPicture(sLPicture);

                SLStyle sLStyleHeader = sLDocument.CreateStyle();
                sLStyleHeader.SetHorizontalAlignment(HorizontalAlignmentValues.Center);
                sLStyleHeader.Font.Bold = true;
                sLStyleHeader.SetWrapText(true);
                sLStyleHeader.SetLeftBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleHeader.SetTopBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleHeader.SetRightBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleHeader.SetBottomBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                System.Drawing.Color backgroundColor = System.Drawing.ColorTranslator.FromHtml("#A9D08E");
                sLStyleHeader.SetPatternFill(PatternValues.Solid, backgroundColor, backgroundColor);
                
                sLDocument.SetCellValue("F3", "Id");
                sLDocument.SetCellStyle("F3", sLStyleHeader);
                sLDocument.SetCellValue("G3", "Name");
                sLDocument.SetCellStyle("G3", sLStyleHeader);
                sLDocument.SetCellValue("H3", "Home");
                sLDocument.SetCellStyle("H3", sLStyleHeader);
                sLDocument.SetCellValue("I3", "Appearance");
                sLDocument.SetCellStyle("I3", sLStyleHeader);
                sLDocument.SetColumnWidth("I3", 12.00);
                sLDocument.SetCellValue("J3", "Description");
                sLDocument.SetCellStyle("J3", sLStyleHeader);
                sLDocument.SetColumnWidth("J3", 12.00);

                SLStyle sLStyleBody = sLDocument.CreateStyle();
                sLStyleBody.SetHorizontalAlignment(HorizontalAlignmentValues.Left);
                sLStyleBody.SetLeftBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleBody.SetTopBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleBody.SetRightBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                sLStyleBody.SetBottomBorder(BorderStyleValues.Thin, SLThemeColorIndexValues.Dark1Color);
                int index = 4;
                foreach (var item in heroes)
                {
                    sLDocument.SetCellValue($"F{index}", item.Id);
                    sLDocument.SetCellStyle($"F{index}", sLStyleBody);
                    sLDocument.SetCellValue($"G{index}", item.Name);
                    sLDocument.SetCellStyle($"G{index}", sLStyleBody);
                    sLDocument.SetCellValue($"H{index}", item.Home);
                    sLDocument.SetCellStyle($"H{index}", sLStyleBody);
                    sLDocument.SetCellValue($"I{index}", item.Appearance.ToString("yyyy-MM-dd"));
                    sLDocument.SetCellStyle($"I{index}", sLStyleBody);
                    sLDocument.SetCellValue($"J{index}", item.Description);
                    sLDocument.SetCellStyle($"J{index}", sLStyleBody);
                    index++;
                }

                sLDocument.SaveAs(memoryStream);
                heroeExcelDTO = new HeroeExcelDTO()
                {
                    FileName = "Heroes.xlsx",
                    FileContent = memoryStream.ToArray()
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Dispose();
                if (sLDocument != null)
                    sLDocument.Dispose();
            }
            return heroeExcelDTO;
        }

        public HeroePDFDTO PDF()
        {
            HeroePDFDTO heroePDFDTO = null;
            MemoryStream memoryStream = null;
            Document documentPDF = null;
            PdfWriter pdfWriter = null;
            System.Drawing.Image contentImage = null;
            try
            {
                memoryStream = new MemoryStream();
                documentPDF = new Document(PageSize.A3, 25, 25, 25, 25);
                pdfWriter = PdfWriter.GetInstance(documentPDF, memoryStream);

                documentPDF.Open();
                documentPDF.AddAuthor("Ignacio Chávez");

                PdfContentByte pdfContentByte = pdfWriter.DirectContent;

                PdfPTable pdfTableTitle = new PdfPTable(1);
                pdfTableTitle.TotalWidth = 140;

                iTextSharp.text.Font fontHeader = new iTextSharp.text.Font(FontFactory.GetFont("Calibri").BaseFont, 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                PdfPCell pdfPCellTitle = new PdfPCell(new Phrase("Heroe", fontHeader));
                pdfPCellTitle.BorderWidth = 1;
                pdfPCellTitle.BorderColor = BaseColor.BLACK;
                pdfPCellTitle.BackgroundColor = BaseColor.WHITE;
                pdfPCellTitle.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                pdfTableTitle.AddCell(pdfPCellTitle);
                pdfTableTitle.WriteSelectedRows(0, -1, documentPDF.LeftMargin, documentPDF.Top - 20, pdfContentByte);

                contentImage = System.Drawing.Image.FromFile(Useful.GetImagePath());
                Image image = Image.GetInstance(contentImage, System.Drawing.Imaging.ImageFormat.Png);
                image.ScaleToFit(140, 237);
                image.Alignment = Image.UNDERLYING;
                image.SetAbsolutePosition(documentPDF.LeftMargin, documentPDF.Top - 185);

                documentPDF.Add(image);

                int index = 1;
                int size = GetPageSizeMaximun();
                int length = heroes.Count() / size;
                for (int i = 0; i <= length; i++)
                {
                    List<Heroe> heroesByPage = heroes.OrderBy(o => o.Id).Skip(size * (index - 1)).Take(size).ToList();

                    if (heroesByPage.Count() == 0)
                        break;

                    PdfPTable pdfTable = new PdfPTable(6);
                    pdfTable.TotalWidth = 583;

                    int widthId = 48;
                    int widthName = 47;
                    int widthHome = 62;
                    int widthAppearence = 63;
                    int widthDescription = 106;
                    int widthImgBase64String = 254;

                    pdfTable.SetWidths(new int[] { widthId, widthName, widthHome, widthAppearence, widthDescription, widthImgBase64String });

                    PdfPCell pdfPCellId = new PdfPCell(new Phrase("Id", fontHeader));
                    pdfPCellId.BorderWidth = 1;
                    pdfPCellId.BorderColor = BaseColor.BLACK;
                    pdfPCellId.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellId.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell pdfPCellName = new PdfPCell(new Phrase("Name", fontHeader));
                    pdfPCellName.BorderWidth = 1;
                    pdfPCellName.BorderColor = BaseColor.BLACK;
                    pdfPCellName.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellName.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell pdfPCellHome = new PdfPCell(new Phrase("Home", fontHeader));
                    pdfPCellHome.BorderWidth = 1;
                    pdfPCellHome.BorderColor = BaseColor.BLACK;
                    pdfPCellHome.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellHome.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell pdfPCellAppearance = new PdfPCell(new Phrase("Appearance", fontHeader));
                    pdfPCellAppearance.BorderWidth = 1;
                    pdfPCellAppearance.BorderColor = BaseColor.BLACK;
                    pdfPCellAppearance.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellAppearance.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell pdfPCellDescription = new PdfPCell(new Phrase("Description", fontHeader));
                    pdfPCellDescription.BorderWidth = 1;
                    pdfPCellDescription.BorderColor = BaseColor.BLACK;
                    pdfPCellDescription.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellDescription.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    PdfPCell pdfPCellImgBase64String = new PdfPCell(new Phrase("ImgBase64String", fontHeader));
                    pdfPCellImgBase64String.BorderWidth = 1;
                    pdfPCellImgBase64String.BorderColor = BaseColor.BLACK;
                    pdfPCellImgBase64String.BackgroundColor = new BaseColor(GetBackgroundColorHeaders());
                    pdfPCellImgBase64String.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                    pdfTable.AddCell(pdfPCellId);
                    pdfTable.AddCell(pdfPCellName);
                    pdfTable.AddCell(pdfPCellHome);
                    pdfTable.AddCell(pdfPCellAppearance);
                    pdfTable.AddCell(pdfPCellDescription);
                    pdfTable.AddCell(pdfPCellImgBase64String);

                    iTextSharp.text.Font fontBody = new iTextSharp.text.Font(FontFactory.GetFont("Calibri").BaseFont, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                    foreach (var item in heroesByPage)
                    {
                        pdfPCellId = new PdfPCell(new Phrase(item.Id.ToString(), fontBody));
                        pdfPCellId.BorderWidth = 1;
                        pdfPCellId.BorderColor = BaseColor.BLACK;
                        pdfPCellId.BackgroundColor = BaseColor.WHITE;

                        pdfPCellName = new PdfPCell(new Phrase(item.Name, fontBody));
                        pdfPCellName.BorderWidth = 1;
                        pdfPCellName.BorderColor = BaseColor.BLACK;
                        pdfPCellName.BackgroundColor = BaseColor.WHITE;

                        pdfPCellHome = new PdfPCell(new Phrase(item.Home, fontBody));
                        pdfPCellHome.BorderWidth = 1;
                        pdfPCellHome.BorderColor = BaseColor.BLACK;
                        pdfPCellHome.BackgroundColor = BaseColor.WHITE;

                        pdfPCellAppearance = new PdfPCell(new Phrase(item.Appearance.ToString("yyyy-MM-dd"), fontBody));
                        pdfPCellAppearance.BorderWidth = 1;
                        pdfPCellAppearance.BorderColor = BaseColor.BLACK;
                        pdfPCellAppearance.BackgroundColor = BaseColor.WHITE;

                        pdfPCellDescription = new PdfPCell(new Phrase(item.Description, fontBody));
                        pdfPCellDescription.BorderWidth = 1;
                        pdfPCellDescription.BorderColor = BaseColor.BLACK;
                        pdfPCellDescription.BackgroundColor = BaseColor.WHITE;

                        byte[] imgBase64String = Convert.FromBase64String(Useful.ReplaceConventionImageFromBase64String(item.ImgBase64String));
                        image = Image.GetInstance(imgBase64String);
                        image.ScalePercent(50f, 50f);

                        pdfPCellImgBase64String = new PdfPCell(image);
                        pdfPCellImgBase64String.BorderWidth = 1;
                        pdfPCellImgBase64String.BorderColor = BaseColor.BLACK;
                        pdfPCellImgBase64String.BackgroundColor = BaseColor.WHITE;

                        pdfTable.AddCell(pdfPCellId);
                        pdfTable.AddCell(pdfPCellName);
                        pdfTable.AddCell(pdfPCellHome);
                        pdfTable.AddCell(pdfPCellAppearance);
                        pdfTable.AddCell(pdfPCellDescription);
                        pdfTable.AddCell(pdfPCellImgBase64String);
                    }

                    pdfTable.WriteSelectedRows(0, -1, 210, documentPDF.Top - 20, pdfContentByte);

                    documentPDF.NewPage();

                    index++;
                }                
                
                documentPDF.Close();

                heroePDFDTO = new HeroePDFDTO()
                {
                    FileName = "Heroes.pdf",
                    FileContent = memoryStream.ToArray()
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Dispose();
                if (documentPDF != null)
                {
                    if (documentPDF.IsOpen())
                        documentPDF.Close();

                    documentPDF.Dispose();
                }
                if (pdfWriter != null)
                    pdfWriter.Dispose();
                if (contentImage != null)
                    contentImage.Dispose();
            }
            return heroePDFDTO;
        }

        private int GetPageSizeMaximun()
        {
            return Convert.ToInt32(Useful.GetAppSettings("PDFHeroeSizeMaximunOfRecords"));
        }

        private System.Drawing.Color GetBackgroundColorHeaders()
        {
            return System.Drawing.ColorTranslator.FromHtml("#A9D08E");
        }
    }
}
