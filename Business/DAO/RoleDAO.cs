﻿using Business.DTO;
using DataSource.Comic;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight.Drawing;
using Business.Tool;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Business.Interface;

namespace Business.DAO
{
    public class RoleDAO : IRole
    {

        public RoleDAO()
        {

        }

        public Entity.Role Select(int id)
        {
            Entity.Role role = null;
            var entity = ModelComic.ComicEntities.Role.FirstOrDefault(o => o.Id == id);

            if (entity != null)
                role = new Entity.Role(entity.Id, entity.Name);

            return role;
        }

        public bool ExistByName(string name)
        {
            bool exist = ModelComic.ComicEntities.Role.Any(o => o.Name == name);
            return exist;
        }

        public bool Insert(string name)
        {
            Role role = new Role();
            role.Name = name;
            int insert = ModelComic.ComicEntities.SaveChanges();
            return (insert > 0);
        }

        public bool Update(Entity.Role role)
        {
            int isUpdate = 0;
            Role entity = ModelComic.ComicEntities.Role.FirstOrDefault(o => o.Id == role.Id);
            if (entity != null)
            {
                entity.Name = role.Name;
                isUpdate = ModelComic.ComicEntities.SaveChanges();
            }
            return (isUpdate > 0);
        }

        public bool Delete(int id)
        {
            int isDelete = 0;
            Role entity = ModelComic.ComicEntities.Role.FirstOrDefault(o => o.Id == id);
            if (entity != null)
            {
                ModelComic.ComicEntities.Role.Remove(entity);
                isDelete = ModelComic.ComicEntities.SaveChanges();
            }
            return (isDelete > 0);
        }

        public List<Entity.Role> List()
        {
            List<Entity.Role> list = new List<Entity.Role>();
            List<Role> entities = ModelComic.ComicEntities.Role.ToList();
            foreach (var item in entities)
            {
                Entity.Role role = new Entity.Role(item.Id, item.Name);
                list.Add(role);
            }
            return list;
        }

        public List<Entity.Role> ListPaginated(ListPaginatedDTO listPaginatedDTO)
        {
            List<Entity.Role> list = new List<Entity.Role>();
            List<Role> entities = ModelComic.ComicEntities.Role.OrderByDescending(o => o.Id).Skip((listPaginatedDTO.PageSize * (listPaginatedDTO.PageIndex - 1))).Take(listPaginatedDTO.PageSize).ToList();
            foreach (var item in entities)
            {
                Entity.Role role = new Entity.Role(item.Id, item.Name);
                list.Add(role);
            }
            return list;
        }

        public long TotalRecords()
        {
            long totalRecords = ModelComic.ComicEntities.Role.LongCount();
            return totalRecords;
        }

        public bool ExistByNameAndNotSameEntity(Entity.Role role)
        {
            bool exist = ModelComic.ComicEntities.Role.Any(o => o.Id != role.Id && o.Name == role.Name);
            return exist;
        }

        public FileDTO Excel()
        {
            FileDTO fileDTO = null;
            MemoryStream memoryStream = null;
            SLDocument sLDocument = null;
            try
            {
                memoryStream = new MemoryStream();
                sLDocument = Useful.GetSpreadsheetLightBase();

                SLStyle sLStyleHeaderTable = Useful.GetSpreadsheetLightStyleCellTableHeader(sLDocument);
                sLDocument.SetCellValue("E8", "Id");
                sLDocument.SetCellStyle("E8", sLStyleHeaderTable);
                sLDocument.SetCellValue("F8", "Name");
                sLDocument.SetCellStyle("F8", sLStyleHeaderTable);

                SLStyle sLStyleId = Useful.GetSpreadsheetLightStyleCellIdTable(sLDocument);
                sLStyleId.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBody = Useful.GetSpreadsheetLightStyleCellTableBody(sLDocument);

                SLStyle sLStyleIdDegrade = Useful.GetSpreadsheetLightStyleCellIdTableDegrade(sLDocument);
                sLStyleIdDegrade.Alignment.Horizontal = HorizontalAlignmentValues.Left;

                SLStyle sLStyleBodyDegrade = Useful.GetSpreadsheetLightStyleCellTableBodyDegrade(sLDocument);

                sLDocument.SetColumnWidth("F9", 25.00);

                int index = 9;
                var roles = List();
                foreach (var item in roles)
                {
                    if ((index % 2) == 0)
                    {
                        sLDocument.SetCellValue($"E{index}", item.Id);
                        sLDocument.SetCellStyle($"E{index}", sLStyleId);
                        sLDocument.SetCellValue($"F{index}", item.Name);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBody);
                    }
                    else
                    {
                        sLDocument.SetCellValue($"E{index}", item.Id);
                        sLDocument.SetCellStyle($"E{index}", sLStyleIdDegrade);
                        sLDocument.SetCellValue($"F{index}", item.Name);
                        sLDocument.SetCellStyle($"F{index}", sLStyleBodyDegrade);
                    }                   
                    index++;
                }

                sLDocument.SaveAs(memoryStream);
                fileDTO = new FileDTO("Role.xlsx", memoryStream.ToArray());
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
            return fileDTO;
        }

        public FileDTO PDF()
        {
            FileDTO fileDTO = null;
            MemoryStream memoryStream = null;
            PdfWriter pdfWriter = null;
            try
            {
                memoryStream = new MemoryStream();
                Document documentPDF = new Document(PageSize.A3, 25, 25, 25, 25);
                pdfWriter = PdfWriter.GetInstance(documentPDF, memoryStream);

                documentPDF.Open();
                documentPDF.AddAuthor("Ignacio Chávez");

                PdfContentByte pdfContentByte = pdfWriter.DirectContent;

                PdfPTable pdfPTableHeaderOne = Useful.GetiTextSharpTableHeaderOne();
                documentPDF.Add(pdfPTableHeaderOne);
                
                PdfPTable pdfPTableHeaderTwo = Useful.GetiTextSharpTableHeaderTwo();
                documentPDF.Add(pdfPTableHeaderTwo);

                iTextSharp.text.Image imageLogo = Useful.GetiTextSharpImageLogo();
                imageLogo.SetAbsolutePosition(documentPDF.LeftMargin + 40, documentPDF.Top - 55);
                documentPDF.Add(imageLogo);

                PdfPTable pdfPTableTitle = Useful.GetiTextSharpTitle();
                pdfPTableTitle.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 41, documentPDF.Top - 50, pdfContentByte);

                PdfPTable pdfPTableDateTime = Useful.GetiTextSharpDateTime();
                pdfPTableDateTime.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 640, documentPDF.Top - 5, pdfContentByte);

                PdfPTable pdfPTablePageNumber = Useful.GetiTextSharpTablePageNumber(1);
                pdfPTablePageNumber.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 760, documentPDF.Bottom + 20, pdfContentByte);

                documentPDF.Add(new Phrase("\n"));

                PdfPTable pdfPTableDescription = Useful.GetiTextSharpTableDescription("Role", TotalRecords());
                documentPDF.Add(pdfPTableDescription);

                documentPDF.Add(new Phrase("\n"));
                
                int index = 1;
                int size = GetPageSizeMaximun();
                long length = TotalRecords() / size;
                for (int i = 0; i <= length; i++)
                {
                    List<DataSource.Comic.Role> heroesByPage = ModelComic.ComicEntities.Role.OrderBy(o => o.Id).Skip(size * (index - 1)).Take(size).ToList();

                    if (heroesByPage.Count() == 0)
                        break;
                    
                    if (index != 1)
                    {
                        documentPDF.Add(pdfPTableHeaderOne);
                        documentPDF.Add(pdfPTableHeaderTwo);
                        documentPDF.Add(imageLogo);                        
                        pdfPTableTitle.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 41, documentPDF.Top - 50, pdfContentByte);                        
                        pdfPTableDateTime.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 640, documentPDF.Top - 5, pdfContentByte);

                        pdfPTablePageNumber = Useful.GetiTextSharpTablePageNumber(index);
                        pdfPTablePageNumber.WriteSelectedRows(0, -1, documentPDF.LeftMargin + 760, documentPDF.Bottom + 20, pdfContentByte);

                        documentPDF.Add(new Phrase("\n\n"));
                    }                    

                    PdfPTable pdfPTable = new PdfPTable(2);
                    pdfPTable.HorizontalAlignment = 1;
                    
                    PdfPCell pdfPCellId = Useful.GetiTextSharpCellTableHeader("Id");
                    PdfPCell pdfPCellName = Useful.GetiTextSharpCellTableHeader("Name");

                    pdfPTable.AddCell(pdfPCellId);
                    pdfPTable.AddCell(pdfPCellName);
                    
                    int count = 0;
                    foreach (var item in heroesByPage)
                    {
                        if ((count % 2) == 0)
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBodyDegrade(item.Id.ToString());
                            pdfPCellName = Useful.GetiTextSharpCellTableBodyDegrade(item.Name);                            
                        }
                        else
                        {
                            pdfPCellId = Useful.GetiTextSharpCellIdTableBody(item.Id.ToString());
                            pdfPCellName = Useful.GetiTextSharpCellTableBody(item.Name);
                        }
                        

                        pdfPTable.AddCell(pdfPCellId);
                        pdfPTable.AddCell(pdfPCellName);

                        count++;
                    }
                    

                    documentPDF.Add(pdfPTable);
                    documentPDF.NewPage();

                    index++;
                }

                documentPDF.Dispose();
                documentPDF.Close();

                fileDTO = new FileDTO("Role.pdf", memoryStream.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (memoryStream != null)
                    memoryStream.Dispose();
                if (pdfWriter != null)
                    pdfWriter.Dispose();
            }
            return fileDTO;
        }

        private int GetPageSizeMaximun()
        {
            return Convert.ToInt32(Useful.GetAppSettings("PDFRoleSizeMaximunOfRecords"));
        }

        private System.Drawing.Color GetBackgroundColorHeaders()
        {
            return System.Drawing.ColorTranslator.FromHtml("#A9D08E");
        }
    }
}
