using AccionLaboral.Models;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AccionLaboral.Reports.Helpers
{
    public static class CV
    {
        public static void CreateWordDocument(Client client, ref string path)
        {
            path = " Prueba: ";
            Object oMissing = System.Reflection.Missing.Value;
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            string wanted_path = System.Web.HttpContext.Current.Server.MapPath(@"~/Reports");
            /*string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(
System.IO.Path.GetDirectoryName(
      System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase))).Replace("file:\\", "");*/
            //Object oTemplatePath = wanted_path + "\\AccionLaboral\\Reports\\CVTemplate.dotx";
            Object oTemplatePath = wanted_path +  "/CVTemplate.dotx";

         

            Application wordApp = new Application();
            Document wordDoc = new Document();
            path += oTemplatePath.ToString();
            wordDoc = wordApp.Documents.Add(ref oTemplatePath, ref oMissing, ref oMissing, ref oMissing);

            /*List<string> campos = new List<string>();
            campos.Add("uno");
            campos.Add("dos");
            campos.Add("tres");
            campos.Add("cuatro");
            campos.Add("cinco");*/

            foreach (Microsoft.Office.Interop.Word.Shape shape in wordApp.ActiveDocument.Shapes)
            {
                if (shape.TextFrame.HasText < 0)
                {
                    Range r = shape.TextFrame.TextRange;
                    var ftext = r.Text;
                    ftext = ftext.Replace("\r", "");
                    ftext = ftext.Replace("«", "");
                    ftext = ftext.Replace("»", "");

                    if(ftext=="Photo"){
                        //r.Text
                        //AddImage(r, ftext, wordApp, client.Photo, oMissing);
                    }
                    
                }
            }

            foreach (Field myMergeField in wordDoc.Fields)
            {
                Range rngFieldCode = myMergeField.Code;
                String fieldText = rngFieldCode.Text;

                // ONLY GETTING THE MAILMERGE FIELDS

                if (fieldText.StartsWith(" MERGEFIELD"))
                {
                    Int32 endMerge = fieldText.IndexOf("\\");
                    Int32 fieldNameLength = fieldText.Length - endMerge;
                    String fieldName = fieldText.Substring(11, endMerge - 11);
                    fieldName = fieldName.Trim();

                    if (fieldName == "FirstName")
                    {
                        myMergeField.Select();
                        wordApp.Selection.TypeText(client.FirstName);
                    }
                    else if (fieldName == "LastName")
                    {
                        myMergeField.Select();
                        wordApp.Selection.TypeText(client.LastName);
                    }
                    else if (fieldName == "Age")
                    {
                        myMergeField.Select();
                        wordApp.Selection.TypeText(client.Age.ToString());
                    }
                    else if (fieldName == "CompleteAddress")
                    {
                        myMergeField.Select();
                        wordApp.Selection.TypeText(client.CompleteAddress);
                    }
                    else if (fieldName == "City")
                    {
                        myMergeField.Select();
                        wordApp.Selection.TypeText(client.City.Name);
                    }
                    else if (fieldName == "Cellphone")
                    {
                        myMergeField.Select();
                        wordApp.Selection.TypeText(client.Cellphone);
                    }
                    else if (fieldName == "Email")
                    {
                        myMergeField.Select();
                        wordApp.Selection.TypeText(client.Email);
                    }
                    else if (fieldName == "Languages")
                    {
                        myMergeField.Select();
                        bool first = true;
                        foreach (KnownLanguage item in client.Languages.ToList())
                        {
                            /*if (first)
                            {*/
                                wordApp.Selection.TypeText(item.Language.Name + ": " + item.LanguageLevel.Name +"\n");
                                /*first = false;
                            }
                            else
                                wordApp.Selection.InsertAfter(item + "\n");*/
                        }
                    }
                    else if (fieldName == "AcademicEducations")
                    {
                        myMergeField.Select();
                        bool first = true;
                        foreach (AcademicEducation item in client.AcademicEducations.ToList())
                        {
                            if (item.EducationType.Name == "ACADEMICA")
                                if (first)
                                {
                                wordApp.Selection.TypeText("Año: " + item.Year + "\t" + "Titulo: " + item.TrainingName + "\n" +
                                                            "Ciundad-País: " + item.City.Name + item.City.Country.Name + "\t" + "Universidad/Institución: " + item.InstitutionName + "\n");
                            first = false;
                        }
                        else
                            wordApp.Selection.InsertAfter(item + "\n");
                        }
                    }
                    else if (fieldName == "Programs")
                    {
                        myMergeField.Select();
                        bool first = true;
                        foreach (KnownProgram item in client.KnownPrograms.ToList())
                        {
                                if (first)
                                {
                                    wordApp.Selection.TypeText(item.Name + ",");
                            first = false;
                        }
                        else
                            wordApp.Selection.InsertAfter(item.Name + ",");
                        }
                        wordApp.Selection.Text = wordApp.Selection.Text.Remove(wordApp.Selection.Text.Length - 1, 1) + "\n";
                    }
                    else if (fieldName == "WorkExperiencies")
                    {
                        myMergeField.Select();
                        bool first = true;
                        foreach (WorkExperience item in client.WorkExperiences.ToList())
                        {
                            string content = "Empresa: " + item.CompanyName + ", " + "Sector " + item.CompanyArea + "\n" +
                                                           "Cargo Ocupado: " + item.Charge + "\n"
                                                           + "Fecha Inicio-Fecha Final: " + item.StartDate.ToString("dd/MM/yyyy") + " - " + item.EndDate.ToString("dd/MM/yyyy") + "\t" + "Ciudad-País: " + item.City.Name + " - " + item.City.Country.Name + "\n" +
                                                           "Tareas o Logros Realizados: " + item.Achievements + "\n";
                            if (first)
                            {
                                wordApp.Selection.TypeText(content);
                                first = false;
                            }
                            else
                                wordApp.Selection.InsertAfter(content);
                        }
                    }
                    else if (fieldName == "WorkReferences")
                    {
                        myMergeField.Select();
                        bool first = true;
                        foreach (Reference item in client.References.ToList())
                        {
                            if (item.ReferenceType.Name == "L")
                            {
                                string content = "Nombre: " + item.FirstName + " " + item.LastName + "\n" +
                                                 "Empresa: " + item.CompanyName + "\t" + "Ocupación: " + item.Charge + "\n" +
                                                 "Ciudad-País: " + item.City.Name + " - " + item.City.Country.Name + "\n" +
                                                 "Teléfono: " + item.Cellphone + "\t" + "Email: " + item.Email + "\n";
                                if (first)
                                {
                                    wordApp.Selection.TypeText(content);
                                    first = false;
                                }
                                else
                                    wordApp.Selection.InsertAfter(content);
                            }
                        }

                    }
                    else if (fieldName == "PersonalReferences")
                    {
                        myMergeField.Select();
                        bool first = true;
                        foreach (Reference item in client.References.ToList())
                        {
                            if (item.ReferenceType.Name == "P")
                            {
                                string content = "Nombre: " + item.FirstName + " " + item.LastName + "\n" +
                                                 "Parentesco: " + item.Relationship + "\t" + "Ocupación: " +item.Charge + "\n" +
                                                 "Ciudad-País: " + item.City.Name + " - " + item.City.Country.Name + "\n" +
                                                 "Teléfono: " + item.Cellphone + "\t" + "Email: " + item.Email + "\n";
                                if (first)
                                {
                                    wordApp.Selection.TypeText(content);
                                    first = false;
                                }
                                else
                                    wordApp.Selection.InsertAfter(content);
                            }
                        }

                    }
                    else if (fieldName == "Trainings")
                    {
                        myMergeField.Select();
                        bool first = true;
                        foreach (AcademicEducation item in client.AcademicEducations.ToList())
                        {
                            if (item.EducationType.Name == "ADICIONAL")
                                if (first)
                                {
                                    wordApp.Selection.TypeText(item.TrainingName + ",");
                            first = false;
                        }
                        else
                                    wordApp.Selection.InsertAfter(item.TrainingName + ",");
                        }
                        wordApp.Selection.Text = wordApp.Selection.Text.Remove(wordApp.Selection.Text.Length - 1, 1) + "\n";
                    

                    }
                    else if (fieldName.Contains("Hobby"))
                    {
                        myMergeField.Select();
                        wordApp.Selection.TypeText(client.Hobby);
                    }
                    else if (fieldName.Contains("Photo"))
                    {
                        AddImage(myMergeField, fieldName, wordApp, client.Photo, oMissing);
                    }

                }

            }


            /////////

            string dummyFileName = "Guardar Curriculum";


            FileDialog dialog = wordApp.get_FileDialog(MsoFileDialogType.msoFileDialogSaveAs);
            dialog.Title = dummyFileName;
            if (dialog.Show() != 0)
            {
                dialog.Execute();
            }

            //wordDoc.SaveAs("myfile.doc");

            wordApp.Application.Quit();
        }

        private static void AddImage(Field myMergeField, string fieldName, Application wordApp, byte[] Photo, Object oMissing)
        {
            myMergeField.Select();
            wordApp.Selection.TypeText(" ");
            int toCharPos = fieldName.IndexOf("(");
            Byte[] byteImage = Photo;
            int imgSizePosStart = fieldName.IndexOf("(", toCharPos + 1);
            //int imgSizePosEnd = fieldName.IndexOf(")", imgSizePosStart);
            string imageDims = fieldName;
            char[] sepChar = { ',' };
            string[] imgDims = imageDims.Split(sepChar);
            System.IO.MemoryStream ms = new System.IO.MemoryStream(byteImage);
            Bitmap newSignImage = (Bitmap)Image.FromStream(ms);
            int imgHeight = 221;
            int imgWidth = 270;
            if (imgDims.Length > 1)
            {
                imgWidth = Convert.ToInt32(imgDims[0]);
                imgHeight = Convert.ToInt32(imgDims[1]);
            }
            Size newSize = new Size(imgWidth, imgHeight);
            Bitmap sigImage = new Bitmap(newSignImage, newSize);
            string imgTempFileName = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".jpg";
            sigImage.Save(imgTempFileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            sigImage.Dispose();          
            wordApp.Selection.InlineShapes.AddPicture(imgTempFileName, ref oMissing, ref oMissing, ref oMissing);
            System.IO.File.Delete(imgTempFileName);
        }
    }
}