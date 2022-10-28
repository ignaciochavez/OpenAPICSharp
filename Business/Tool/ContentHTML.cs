using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tool
{
    public class ContentHTML
    {
        public string Path { get; set; }
        public HtmlDocument HtmlDocument { get; set; }

        public ContentHTML()
        {

        }

        public ContentHTML(string path)
        {
            Path = path;
            HtmlDocument = null;
        }

        public void LoadDocumentHTML(string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
                HtmlDocument = GetDocumentHTML(path);
            else
                HtmlDocument = GetDocumentHTML(GetDirectoryHtmlMessages());
        }

        private HtmlDocument GetDocumentHTML(string path)
        {
            HtmlDocument documentHTML = new HtmlDocument();
            documentHTML.Load(path);
            return documentHTML;
        }

        public string GetInnerTextById(string id)
        {
            if (HtmlDocument == null)
                LoadDocumentHTML(Path);

            return HtmlDocument.GetElementbyId(id).InnerText;
        }

        public bool IsLoadDocumentHTML()
        {
            try
            {
                if (HtmlDocument == null)
                    LoadDocumentHTML(Path);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetDirectoryHtmlMessages()
        {
            return Useful.GetApplicationDirectory() + @"Contents\Useful\Messages.html"; ;
        }
    }
}
