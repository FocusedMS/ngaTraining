using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern
{
    public static class DocumentFactory
    {
        public static IDocument CreateDocument(string type)
        {
            switch (type.ToLower())
            {
                case "pdf":
                    return new PDFDocument();
                case "word":
                    return new WordDocument();
                default:
                    throw new ArgumentException("Invalid document type");
            }
        }
    }
}
