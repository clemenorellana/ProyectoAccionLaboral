using AccionLaboral.Helpers.Filters;
using AccionLaboral.Models;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AccionLaboral.Reports.Helpers
{
    public class Vacants
    {
        static HSSFWorkbook hssfworkbook;
        static string path = AppDomain.CurrentDomain.BaseDirectory;
        public static FileStream GenerateReport(string fileName, VacantReportFilter filters)
        {
            try
            {
                InitializeWorkbook();

                ISheet sheet1 = hssfworkbook.GetSheet("Vacantes");

                if (filters.DateFrom.Year > 1 && filters.DateTo.Year == 1)
                {
                    sheet1.GetRow(6).GetCell(3).SetCellValue(filters.DateFrom);
                }
                else if (filters.DateFrom.Year == 1 && filters.DateTo.Year > 1)
                {
                    sheet1.GetRow(6).GetCell(5).SetCellValue(filters.DateTo);
                }
                else if (filters.DateFrom.Year > 1 && filters.DateTo.Year > 1)
                {
                    sheet1.GetRow(6).GetCell(3).SetCellValue(filters.DateFrom);
                    sheet1.GetRow(6).GetCell(5).SetCellValue(filters.DateTo);
                }


                int rowStart = 9;
                int row;

                List<VacantFilter> vacantList = new List<VacantFilter>();
                vacantList = filters.Vacants;
                for (int i = 0; i < vacantList.Count; i++)
                {
                    row = rowStart + i;
                    VacantByCompany vacant = vacantList[i].VacantByCompany;
                    sheet1.GetRow(row).GetCell(1).SetCellValue(vacant.RequestDate);
                    sheet1.GetRow(row).GetCell(2).SetCellValue(vacant.Company.Name);
                    sheet1.GetRow(row).GetCell(3).SetCellValue(vacant.VacantName);
                    sheet1.GetRow(row).GetCell(4).SetCellValue(vacant.VacantLevel.Name);
                    if(!vacant.Active)
                        sheet1.GetRow(row).GetCell(5).SetCellValue(vacant.CoveredDate.ToShortDateString() + " -- " + vacant.CoveredTime);

                    List<VacantCovered> coveredList = vacantList[i].VacantCovered.Distinct().ToList();
                    int col = 6;
                    int x = 0;
                    foreach (var covered in coveredList) 
                    {
                        int column = col + x;
                        //if(row==9)
                            //sheet1.GetRow(row-1).GetCell(column).SetCellValue("Asesor " + covered.Employee.EmployeeAlias + " - " + covered.Employee.FirstName + " " + covered.Employee.LastName );
                        sheet1.GetRow(rowStart-1).GetCell(column).SetCellValue("Asesor " + covered.Employee.EmployeeAlias + " - " + covered.Employee.FirstName + " " + covered.Employee.LastName);
                        sheet1.GetRow(row).GetCell(column).SetCellValue(covered.NumberOfProfiles);
                        sheet1.SetColumnHidden(column, false);
                        //sheet1.AutoSizeColumn(column);
                        x++;
                    }
                    
                }

                ////Force excel to recalculate all the formula while open
                sheet1.ForceFormulaRecalculation = true;

            }
            catch (Exception e)
            {
                var error = e.Message;
            }

            return WriteToFile(fileName);
        }

        public static FileStream WriteToFile(string fileName)
        {
            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(path + "Reports/" + fileName, FileMode.Create);
            hssfworkbook.Write(file);

            file.Close();

            return file;
        }

        static void InitializeWorkbook()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 

            FileStream file = new FileStream(path + @"Reports\Templates\" + "VacantsReport.xls", FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Equipo Acción Laboral";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Vacantes";
            hssfworkbook.SummaryInformation = si;
        }
    }
}