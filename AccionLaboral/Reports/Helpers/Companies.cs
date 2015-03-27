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
    public class Companies
    {
        static HSSFWorkbook hssfworkbook;
        static string path = AppDomain.CurrentDomain.BaseDirectory;
        public static FileStream GenerateReport(string fileName, CompaniesFilters filters)
        {
            try
            {
                InitializeWorkbook();

                ISheet sheet1 = hssfworkbook.GetSheet("Mercadeo");

                if (filters.DateFrom.Year > 1 && filters.DateTo.Year == 1)
                {
                    sheet1.GetRow(2).GetCell(5).SetCellValue(filters.DateFrom);
                }
                else if (filters.DateFrom.Year == 1 && filters.DateTo.Year > 1)
                {
                    sheet1.GetRow(2).GetCell(8).SetCellValue(filters.DateTo);
                }
                else if (filters.DateFrom.Year > 1 && filters.DateTo.Year > 1)
                {
                    sheet1.GetRow(2).GetCell(5).SetCellValue(filters.DateFrom);
                    sheet1.GetRow(2).GetCell(8).SetCellValue(filters.DateTo);
                }


                int rowSart = 9;
                for (int i = 0; i < filters.Companies.Count; i++)
                {
                    int row = rowSart + i;
                    sheet1.GetRow(row).GetCell(0).SetCellValue(i + 1);
                    sheet1.GetRow(row).GetCell(1).SetCellValue(filters.Companies[i].DateCreated);
                    sheet1.GetRow(row).GetCell(2).SetCellValue(filters.Companies[i].Name);

                    if (filters.Companies[i].ContactsByCompany.Count > 0)
                    {
                        List<ContactByCompany> contacts = filters.Companies[i].ContactsByCompany.ToList();
                        sheet1.GetRow(row).GetCell(3).SetCellValue((contacts[0].Telephone != null)? contacts[0].Telephone : "");
                        sheet1.GetRow(row).GetCell(4).SetCellValue((contacts[0].Extension != null) ? contacts[0].Extension : "");
                        sheet1.GetRow(row).GetCell(5).SetCellValue((contacts[0].ContactName != null) ? contacts[0].ContactName : "");
                        sheet1.GetRow(row).GetCell(6).SetCellValue((contacts[0].ContactEmail != null) ? contacts[0].ContactEmail : "");
                        if (contacts.Count > 1)
                        {
                            sheet1.GetRow(row).GetCell(7).SetCellValue((contacts[1].ContactName != null) ? contacts[0].ContactName : "");
                            sheet1.GetRow(row).GetCell(8).SetCellValue((contacts[1].ContactEmail != null) ? contacts[0].ContactEmail : "");
                            string observations = ((contacts[0].Observations != null) ? contacts[0].Observations + ". " : "") + ((contacts[1].Observations != null) ? contacts[1].Observations : "");
                            sheet1.GetRow(row).GetCell(10).SetCellValue(observations);
                        }
                        else
                            sheet1.GetRow(row).GetCell(10).SetCellValue(((contacts[0].Observations != null) ? contacts[0].Observations : ""));
                    }
                    List<VacantByCompany> vacants = filters.Companies[i].VacantsByCompany.ToList();
                    int totalVacants = 0;
                    foreach (VacantByCompany vacant in vacants)
                    { 
                        if (filters.DateFrom.Year == 1 && filters.DateTo.Year == 1)
                        {
                            totalVacants += vacant.Quantity;
                        }
                        else if (filters.DateFrom.Year > 1 && filters.DateTo.Year == 1)
                        { 
                            if(vacant.RequestDate >= filters.DateFrom)
                                totalVacants += vacant.Quantity;
                        }
                        else if (filters.DateFrom.Year == 1 && filters.DateTo.Year > 1)
                        {
                            if (vacant.RequestDate <= filters.DateTo)
                                totalVacants += vacant.Quantity;
                        }
                        else if (filters.DateFrom.Year > 1 && filters.DateTo.Year > 1)
                        {
                            if (vacant.RequestDate >= filters.DateFrom && vacant.RequestDate <= filters.DateTo)
                                totalVacants += vacant.Quantity;
                        }
                    }
                    sheet1.GetRow(row).GetCell(9).SetCellValue(totalVacants);

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

            FileStream file = new FileStream(path + @"Reports\Templates\" + "CompaniesReport.xls", FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Equipo Acción Laboral";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Mercadeo";
            hssfworkbook.SummaryInformation = si;
        }

    }
}