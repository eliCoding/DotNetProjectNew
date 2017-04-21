using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PointOfSaleManagementSys
{

    //FlowDocument doc = new FlowDocument();
    class PrintInvoice 
    { 
        FlowDocument doc = new FlowDocument();
        public PrintInvoice()
        {

        }

        public void PrintInv(string path)
        {
            doc.Blocks.Clear();
            using (StreamReader sr = File.OpenText(path))
            {
                string[] s = File.ReadAllLines(path);
             
                for (int i = 0; i < s.Length; i++)
                {
                    Paragraph p = new Paragraph(new Run(s[i]));
                    doc.Blocks.Add(p);
                }
                 fdViewer.Document = doc;
                
                //p.FontSize = 36;
                //p = new Paragraph(new Run("The ultimate programming greeting!"));
                //p.FontSize = 14;
                //p.FontStyle = FontStyles.Italic;
                //p.TextAlignment = TextAlignment.Left;
                //p.Foreground = Brushes.Gray;
                //doc.Blocks.Add(p);
               
            }

            PrintDialog pd = new PrintDialog();
            //if (pd.ShowDialog() != true) return;
            //doc.PageHeight = pd.PrintableAreaHeight;
            //doc.PageWidth = pd.PrintableAreaWidth;
            //IDocumentPaginatorSource idocument = doc as IDocumentPaginatorSource;
            //pd.PrintDocument(idocument.DocumentPaginator, "Printing Flow Document...");

        }
    
}
}
