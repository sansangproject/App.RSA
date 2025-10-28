using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SANSANG.Class
{
    public class clsReport
    {
        public clsFunction Function = new clsFunction();

        public void Print(DataTable Value, string Dataset, string Report, string Param = "")
        {
            string Path = Function.GetPath("App.Report");
            ReportParameter parameter = new ReportParameter("pConditions", Param);

            ReportDataSource rds = new ReportDataSource();
            rds.Name = Dataset;
            rds.Value = Value;

            LocalReport LocalReport = new LocalReport();
            LocalReport.ReportPath = Path + Report;
            LocalReport.DisplayName = "12354";
            LocalReport.EnableExternalImages = true;

            LocalReport.DataSources.Clear();
            LocalReport.DataSources.Add(rds);
            LocalReport.SetParameters(parameter);

            PrintToPrinter(LocalReport);
        }
        private void PrintToPrinter(LocalReport Report)
        {
            PageSettings Page = new PageSettings();
            Page.PaperSize = Report.GetDefaultPageSettings().PaperSize;
            Page.Landscape = Report.GetDefaultPageSettings().IsLandscape;
            Page.Margins = Report.GetDefaultPageSettings().Margins;
            Print(Report, Page);
        }

        public void Print(LocalReport Reports, PageSettings Settings)
        {
            string deviceInfo =
                $@"<DeviceInfo>
                    <OutputFormat>EMF</OutputFormat>
                    <PageWidth>{Settings.PaperSize.Width * 100}in</PageWidth>
                    <PageHeight>{Settings.PaperSize.Height * 100}in</PageHeight>
                    <MarginTop>{Settings.Margins.Top * 100}in</MarginTop>
                    <MarginLeft>{Settings.Margins.Left * 100}in</MarginLeft>
                    <MarginRight>{Settings.Margins.Right * 100}in</MarginRight>
                    <MarginBottom>{Settings.Margins.Bottom * 100}in</MarginBottom>
                </DeviceInfo>";

            Warning[] warnings;
            var streams = new List<Stream>();
            var pageIndex = 0;

            Reports.Render("Image", deviceInfo, (name, fileNameExtension, encoding, mimeType, willSeek) =>
            {
                MemoryStream stream = new MemoryStream();
                streams.Add(stream);
                return stream;
            }, out warnings);

            foreach (Stream stream in streams)
            {
                stream.Position = 0;
            }

            if (streams == null || streams.Count == 0)
            {
                throw new Exception("No stream to print.");
            }

            using (PrintDocument printDocument = new PrintDocument())
            {
                printDocument.DefaultPageSettings = Settings;

                if (!printDocument.PrinterSettings.IsValid)
                {
                    throw new Exception("Can't find the default printer.");
                }
                else
                {
                    printDocument.PrintPage += (sender, e) =>
                    {
                        Metafile pageImage = new Metafile(streams[pageIndex]);
                        Rectangle adjustedRect = new Rectangle(e.PageBounds.Left - (int)e.PageSettings.HardMarginX, e.PageBounds.Top - (int)e.PageSettings.HardMarginY, e.PageBounds.Width, e.PageBounds.Height);
                        e.Graphics.FillRectangle(Brushes.White, adjustedRect);
                        e.Graphics.DrawImage(pageImage, adjustedRect);
                        pageIndex++;
                        e.HasMorePages = (pageIndex < streams.Count);
                        e.Graphics.DrawRectangle(Pens.Red, adjustedRect);
                    };

                    printDocument.EndPrint += (Sender, e) =>
                    {
                        if (streams != null)
                        {
                            foreach (Stream stream in streams)
                            {
                                stream.Close();
                            }
                            streams = null;
                        }
                    };

                    printDocument.Print();
                }
            }
        }
    }
}