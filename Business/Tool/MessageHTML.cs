using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tool
{
    public class MessageHTML
    {
        public string Path { get; set; }
        public HtmlDocument Document { get; set; }
        public MessageHTML(string path)
        {
            Path = path;
            Document = null;
        }

        public void LoadDocumentHTML(string path)
        {
            Document = GetDocumentHTML(path);
        }

        private HtmlDocument GetDocumentHTML(string path)
        {
            HtmlDocument documentHTML = new HtmlDocument();
            documentHTML.Load(path);
            return documentHTML;
        }

        public string GetInnerTextById(string id)
        {
            if (Document == null)
                LoadDocumentHTML(Path);

            return Document.GetElementbyId(id).InnerText;
        }
    }
}
