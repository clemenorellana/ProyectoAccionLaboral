using AccionLaboral.Helpers.Filters;
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
    public class Clients
    {

        static HSSFWorkbook hssfworkbook;
        static string path = AppDomain.CurrentDomain.BaseDirectory;
        public static FileStream GenerateReport(string fileName, ClientsFilter filters)
        {
            InitializeWorkbook();

            ISheet sheet1 = hssfworkbook.GetSheet("Clientes");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            sheet1.GetRow(2).GetCell(2).SetCellValue(filters.DateFrom);
            sheet1.GetRow(2).GetCell(4).SetCellValue(filters.DateTo);
            for (int i = 0; i < filters.Clients.Count; i++)
            {
                DateTime enrollDate = filters.Clients[i].EnrollDate ?? DateTime.Now;
                sheet1.GetRow(i + 5).GetCell(0).SetCellValue(filters.Clients[i].ClientId);
                sheet1.GetRow(i + 5).GetCell(1).SetCellValue(filters.Clients[i].FirstName);
                sheet1.GetRow(i + 5).GetCell(2).SetCellValue(filters.Clients[i].LastName);
                sheet1.GetRow(i + 5).GetCell(3).SetCellValue(filters.Clients[i].Email);
                sheet1.GetRow(i + 5).GetCell(4).SetCellValue(filters.Clients[i].CompleteAddress);
                sheet1.GetRow(i + 5).GetCell(5).SetCellValue(filters.Clients[i].Cellphone);
                sheet1.GetRow(i + 5).GetCell(6).SetCellValue(filters.Clients[i].State.Name);
                sheet1.GetRow(i + 5).GetCell(7).SetCellValue(enrollDate.ToString("dd/MM/yyyy"));
            }

            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            return WriteToFile(fileName);

           
        }

        public static FileStream GenerateClientsTracking(string fileName, ClientsFilter filters)
        {
            InitializeWorkbookTracking();

            ISheet sheet1 = hssfworkbook.GetSheet("Clientes");
            //create cell on rows, since rows do already exist,it's not necessary to create rows again.
            if ((filters.DateFrom != null))
            sheet1.GetRow(2).GetCell(2).SetCellValue(filters.DateFrom);
            sheet1.GetRow(2).GetCell(5).SetCellValue(filters.DateTo);
            for (int i = 0; i < filters.Clients.Count; i++)
            {
                DateTime enrollDate = filters.Clients[i].EnrollDate ?? DateTime.Now;
                
                sheet1.GetRow(i + 5).GetCell(0).SetCellValue(filters.Clients[i].ClientId);
                sheet1.GetRow(i + 5).GetCell(1).SetCellValue(filters.Clients[i].FirstName);
                sheet1.GetRow(i + 5).GetCell(2).SetCellValue(filters.Clients[i].LastName);
                sheet1.GetRow(i + 5).GetCell(3).SetCellValue(filters.Clients[i].Age);
                sheet1.GetRow(i + 5).GetCell(4).SetCellValue(filters.Clients[i].Email);
                sheet1.GetRow(i + 5).GetCell(5).SetCellValue((filters.Clients[i].Trackings.ToList().Count>0)?("Seguimiento de " + filters.Clients[i].Trackings.ToList()[0].TrackingType.Name):"");
                sheet1.GetRow(i + 5).GetCell(6).SetCellValue(filters.Clients[i].CompleteAddress);
                sheet1.GetRow(i + 5).GetCell(7).SetCellValue(filters.Clients[i].Cellphone);
                sheet1.GetRow(i + 5).GetCell(8).SetCellValue(filters.Clients[i].State.Name);
                sheet1.GetRow(i + 5).GetCell(9).SetCellValue(enrollDate.ToString("dd/MM/yyyy"));
            }

            //Force excel to recalculate all the formula while open
            sheet1.ForceFormulaRecalculation = true;

            return WriteToFile(fileName);
        }

        public static FileStream WriteToFile(string fileName)
        {
            //Write the stream data of workbook to the root directory
            FileStream file = new FileStream(path + "Reports/"+fileName, FileMode.Create);
            hssfworkbook.Write(file);

            file.Close();

            return file;
        }

        static void InitializeWorkbook()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 

            FileStream file = new FileStream(path + @"Reports\Templates\" + "Clients.xls", FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Equipo Acción Laboral";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Clientes";
            hssfworkbook.SummaryInformation = si;
        }

        static void InitializeWorkbookTracking()
        {
            //read the template via FileStream, it is suggested to use FileAccess.Read to prevent file lock.
            //book1.xls is an Excel-2007-generated file, so some new unknown BIFF records are added. 

            FileStream file = new FileStream(path + @"Reports\Templates\" + "ClientsTracking.xls", FileMode.Open, FileAccess.Read);

            hssfworkbook = new HSSFWorkbook(file);

            //create a entry of DocumentSummaryInformation
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "Acción Laboral";
            hssfworkbook.DocumentSummaryInformation = dsi;

            //create a entry of SummaryInformation
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "Seguimiento de Clientes";
            hssfworkbook.SummaryInformation = si;
        }

    }
}