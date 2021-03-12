using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Drawing;

namespace MK.Utilities.Reporting
{
    public static class Excel
    {
        public static void Export(string filename, string[,] data)
        {
            try
            {
                Application app;
                Workbook workbook;
                Worksheet worksheet;

                app = new Application();
                workbook = app.Workbooks.Add(System.Reflection.Missing.Value);
                worksheet = workbook.Sheets["Sheet1"];

                // Data dump
                for (int row = 0; row < data.GetLength(0); row++)
                {
                    for (int col = 0; col < data.GetLength(1); col++)
                    {
                        // Put the value in the cell
                        worksheet.Cells[row + 1, col + 1] = data[row, col];

                        // Alternate the row coloring
                        if (row % 2 == 0)
                            worksheet.Cells[row + 1, col + 1].Interior.Color = Color.FromArgb(225, 225, 225);
                    }
                }

                // Bold the column headers
                worksheet.Cells[1, 1].EntireRow.Font.Bold = true;
                worksheet.Cells[1, 1].EntireRow.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                // Autofit the columns
                worksheet.Columns.AutoFit();

                // Center the Headers
                //worksheet.PageSetup.CenterHeader = "List of Teams";

                // Add Gridlines for the data region
                worksheet.UsedRange.Borders.LineStyle = XlLineStyle.xlContinuous;


                // Save the file
                app.DisplayAlerts = false;
                worksheet.SaveAs(filename);

                // Automatically generate the pdf
                workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, filename.Substring(0, filename.Length - 5));

                // Clean Up
                workbook.Close();
                app.Quit();

                // Release the memory for each Excel object
                Marshal.ReleaseComObject(worksheet);
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(app);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
