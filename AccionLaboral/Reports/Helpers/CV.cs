using AccionLaboral.Models;
using Novacode;
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
        #region public static void CreateWordDocument(Client client)
        #endregion

        /*#region private static void AddImage(Field myMergeField, string fieldName, Application wordApp, byte[] Photo, Object oMissing)
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
            Bitmap newSignImage = (Bitmap)System.Drawing.Image.FromStream(ms);
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
        #endregion*/

        #region static void ReplaceTextWithImage(this DocX document, string value, Bitmap image, int count)
        /// <summary>
        /// Replaces a text with a image
        /// </summary>
        /// <param name="document">The docx document</param>
        /// <param name="value">The text to be replaced</param>
        /// <param name="image">The image to replace</param>
        /// <param name="count">How many times it should replace</param>
        public static void ReplaceTextWithImage(this DocX document, string value, Bitmap image, int count)
        {
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            var imageStream = memoryStream;
            try
            {
                var docImage = document.AddImage((Stream)imageStream);
                var docPicture = docImage.CreatePicture();
                docPicture.Height = 160;
                docPicture.Width = 160;

                var countReplace = 0;
                foreach (var paragraph in document.Paragraphs)
                {
                    var valuesIndex = paragraph.FindAll(value);
                    if (valuesIndex.Count > 0)
                    {
                        if (count > 0)
                        {
                            if (valuesIndex.Count > count)
                            {
                                valuesIndex.RemoveRange(count, valuesIndex.Count - count);
                            }
                        }
                        valuesIndex.Reverse();

                        foreach (var valueIndex in valuesIndex)
                        {
                            countReplace++;
                            paragraph.RemoveText(valueIndex, value.Length);
                            paragraph.InsertPicture(docPicture, valueIndex);
                            if (countReplace == count)
                            {
                                return;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (image != null)
                {
                    image.Dispose();
                }

                if (imageStream != null)
                {
                    imageStream.Dispose();
                }
            }
        }
        #endregion

        #region static void CreateCurriculum(this DocX document)
        public static void CreateCurriculum(this DocX document, Client client)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream(client.Photo);
            Bitmap newSignImage = (Bitmap)System.Drawing.Image.FromStream(ms);
            int imgHeight = 160;
            int imgWidth = 160;

            Size newSize = new Size(imgWidth, imgHeight);
            Bitmap sigImage = new Bitmap(newSignImage, newSize);
            ReplaceTextWithImage(document, "{Photo}", sigImage, 1);

            Formatting Bold = new Formatting();
            Bold.Bold = true;
            Formatting noBold = new Formatting();
            noBold.Bold = false;
            document.ReplaceText("{FirstName}", client.FirstName);
            document.ReplaceText("{LastName}", client.LastName);
            document.ReplaceText("{Age}", client.Age + " años de edad.");
            document.ReplaceText("{Address}", client.CompleteAddress);
            document.ReplaceText("{Cellphone}", client.Cellphone);
            document.ReplaceText("{Email}", client.Email);
            InsertAcademicEducations(document, client.AcademicEducations.ToList());
            InsertPrograms(document, client.KnownPrograms.ToList());
            InsertLanguages(document, client.Languages.ToList());
            InsertWorkExperiences(document, client.WorkExperiences.ToList());
            InsertWorkReferences(document, client.References.ToList());
            InsertPersonalReferences(document, client.References.ToList());
            InsertTrainings(document, client.AcademicEducations.ToList());
            document.ReplaceText("{Hobby}", !string.IsNullOrEmpty(client.Hobby) ? "Deportes Hobbies: {Hobby}" : "", false, System.Text.RegularExpressions.RegexOptions.None, Bold);
            document.ReplaceText("{Hobby}", !string.IsNullOrEmpty(client.Hobby) ? client.Hobby : "", false, System.Text.RegularExpressions.RegexOptions.None, noBold);
        }
        #endregion

        #region static void InsertAcademicEducations(List<AcademicEducation> educations)
        public static void InsertAcademicEducations(DocX document, List<AcademicEducation> educations)
        {
            if (educations.Count > 0)
            {
                bool first = true;
                Formatting bold = new Formatting();
                bold.Bold = true;
                Formatting noBold = new Formatting();
                noBold.Bold = false;
                foreach (AcademicEducation item in educations)
                {
                    if (item.EducationType.Name == "ACADEMICA")
                            document.ReplaceText("{AcademicEducations}",((item.Year!=null)?"Año: " + "{Year}" + "\t": "") 
                                                                      + (!(string.IsNullOrEmpty(item.TrainingName)) ? ("Titulo: " + "{TrainingName}") : "") + "\n" +
                                                                        ((item.City!=null) ? "Ciundad-País: " + "{City}" + "\n":"") + 
                                                                        (!(string.IsNullOrEmpty(item.InstitutionName))?"Universidad/Institución: " + "{InstitutionName}":"") 
                                                                        + "\n\n{AcademicEducations}", 
                                                  false, System.Text.RegularExpressions.RegexOptions.None, bold);

                            document.ReplaceText("{Year}", item.Year.ToString(), false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                            document.ReplaceText("{TrainingName}", item.TrainingName, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                            document.ReplaceText("{City}", item.City.Name + "-" + item.City.Country.Name, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                            document.ReplaceText("{InstitutionName}", item.InstitutionName, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                }
                document.ReplaceText("{AcademicEducations}", "");
            }
            else
            {
                document.ReplaceText("{AcademicEducations}", "Ninguna");
            }
        }
        #endregion

        #region static void InsertPrograms(DocX document, List<KnownProgram> educations)
        public static void InsertPrograms(DocX document, List<KnownProgram> programs)
        {
            if (programs.Count > 0)
            {
                bool first = true;
                Formatting bold = new Formatting();
                bold.Bold = true;
                Formatting noBold = new Formatting();
                noBold.Bold = false;
                document.ReplaceText("{Programs}", "Programas Manejados: {Programs}", false, System.Text.RegularExpressions.RegexOptions.None, bold);
                for (int i = 0; i < programs.Count - 1; i++)
                {
                    document.ReplaceText("{Programs}", programs[i].Name + ", {Programs}", false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                }
                document.ReplaceText("{Programs}", programs[programs.Count - 1].Name + ".", false, System.Text.RegularExpressions.RegexOptions.None, noBold);
            }
            else
            {
                document.ReplaceText("{Programs}", "");
            }
        }
        #endregion

        #region static void InsertLanguages(DocX document, List<KnownProgram> educations)
        public static void InsertLanguages(DocX document, List<KnownLanguage> languages)
        {
            if (languages.Count > 0)
            {
                bool first = true;
                Formatting bold = new Formatting();
                bold.Bold = true;
                bold.FontColor = Color.White;
                Formatting noBold = new Formatting();
                noBold.Bold = false;
                noBold.FontColor = Color.White;
                document.ReplaceText("{Languages}", "IDIOMAS\n\n{Languages}", false, System.Text.RegularExpressions.RegexOptions.None, bold);
                foreach (KnownLanguage language in languages)
                {
                    document.ReplaceText("{Languages}", language.Language.Name + ": " + language.LanguageLevel.Name + "\n{Languages}", false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                }
                document.ReplaceText("{Languages}", "");
            }
            else
            {
                document.ReplaceText("{Languages}", "");
            }
        }
        #endregion

        #region static void InsertWorkExperiences(List<WorkExperience> experiences)
        public static void InsertWorkExperiences(DocX document, List<WorkExperience> experiences)
        {
            if (experiences.Count > 0)
            {
                bool first = true;
                Formatting bold = new Formatting();
                bold.Bold = true;
                Formatting noBold = new Formatting();
                noBold.Bold = false;
                foreach (WorkExperience item in experiences)
                {
                        document.ReplaceText("{WorkExperiences}", ((!string.IsNullOrEmpty(item.CompanyName)) ? "Empresa: " + "{CompanyName}" + "\t" : "")
                                                                  + (!(string.IsNullOrEmpty(item.Charge)) ? ("Cargo Ocupado: " + "{Charge}") : "") + "\n" +
                                                                    ((item.StartDate != null && item.EndDate != null) ? "Fecha Inicio-Fecha Final: " + "{Date}" + "\n" : "") + ((item.City != null) ? "Ciundad-País: " + "{City}" + "\n" : "") +
                                                                    (!(string.IsNullOrEmpty(item.Achievements)) ? "Tareas o Logros Realizados: " + "{Achievements}" : "")
                                                                    + "\n\n{WorkExperiences}",
                                              false, System.Text.RegularExpressions.RegexOptions.None, bold);

                        document.ReplaceText("{CompanyName}", item.CompanyName, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                    document.ReplaceText("{Charge}", item.Charge, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                    document.ReplaceText("{Date}", item.StartDate.ToString("dd/MM/yyyy") + " - " + item.EndDate.ToString("dd/MM/yyyy"), false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                    document.ReplaceText("{City}", item.City.Name + "-" + item.City.Country.Name, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                    document.ReplaceText("{Achievements}", item.Achievements, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                }
                document.ReplaceText("{WorkExperiences}", "");
            }
            else
            {
                document.ReplaceText("{WorkExperiences}", "Ninguna");
            }
        }
        #endregion

        #region static void InsertWorkReferences(List<Reference> references)
        public static void InsertWorkReferences(DocX document, List<Reference> references)
        {
            if (references.Count > 0)
            {
                Formatting bold = new Formatting();
                bold.Bold = true;
                Formatting noBold = new Formatting();
                noBold.Bold = false;
                foreach (Reference item in references)
                {
                    if (item.ReferenceType.Name == "L")
                    {
                        document.ReplaceText("{WorkReferences}", item.FirstName + " " + item.LastName + "{WorkReferences}\n", false, System.Text.RegularExpressions.RegexOptions.None, bold);
                        document.ReplaceText("{WorkReferences}", ((!string.IsNullOrEmpty(item.CompanyName)) ? "Empresa: " + "{CompanyName}" + "\n" : "") +
                                                                 ((item.City != null) ? "Ciundad-País: " + "{City}" + "\n" : "") +
                                                                 (!(string.IsNullOrEmpty(item.Charge)) ? ("Cargo Ocupado: " + "{Charge}") : "") + "\t" + (!(string.IsNullOrEmpty(item.Cellphone)) ? "Celular: " + "{Cellphone}\n" : "") +
                                                                    (!(string.IsNullOrEmpty(item.Email)) ? "E-Mail: " + "{Email}" : "")
                                                                    + "\n\n{WorkReferences}",
                                              false, System.Text.RegularExpressions.RegexOptions.None, bold);

                        document.ReplaceText("{CompanyName}", item.CompanyName, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                        document.ReplaceText("{City}", item.City.Name + "-" + item.City.Country.Name, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                        document.ReplaceText("{Charge}", item.Charge, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                        document.ReplaceText("{Cellphone}", item.Cellphone, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                        document.ReplaceText("{Email}", item.Email, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                    }
                }
                document.ReplaceText("{WorkReferences}", "");
            }
            else
            {
                document.ReplaceText("{WorkReferences}", "Ninguna");
            }
        }
        #endregion

        #region static void InsertPersonalReferences(List<Reference> references)
        public static void InsertPersonalReferences(DocX document, List<Reference> references)
        {
            if (references.Count > 0)
            {
                Formatting bold = new Formatting();
                bold.Bold = true;
                Formatting noBold = new Formatting();
                noBold.Bold = false;
                foreach (Reference item in references)
                {
                    if (item.ReferenceType.Name == "P")
                    {
                        document.ReplaceText("{PersonalReferences}", item.FirstName + " " + item.LastName + "{PersonalReferences}\n", false, System.Text.RegularExpressions.RegexOptions.None, bold);
                        document.ReplaceText("{PersonalReferences}", ((!string.IsNullOrEmpty(item.Relationship)) ? "Parentesco: " + "{Relationship}" + "\n" : "") +
                                                                 ((item.City != null) ? "Ciundad-País: " + "{City}" + "\n" : "") +
                                                                 (!(string.IsNullOrEmpty(item.Charge)) ? ("Cargo Ocupado: " + "{Charge}") : "") + "\t" + (!(string.IsNullOrEmpty(item.Cellphone)) ? "Celular: " + "{Cellphone}\n" : "") +
                                                                    (!(string.IsNullOrEmpty(item.Email)) ? "E-Mail: " + "{Email}" : "")
                                                                    + "\n\n{PersonalReferences}",
                                              false, System.Text.RegularExpressions.RegexOptions.None, bold);

                        document.ReplaceText("{Relationship}", item.Relationship, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                        document.ReplaceText("{City}", item.City.Name + "-" + item.City.Country.Name, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                        document.ReplaceText("{Charge}", item.Charge, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                        document.ReplaceText("{Cellphone}", item.Cellphone, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                        document.ReplaceText("{Email}", item.Email, false, System.Text.RegularExpressions.RegexOptions.None, noBold);
                    }
                }
                document.ReplaceText("{PersonalReferences}", "");
            }
            else
            {
                document.ReplaceText("{PersonalReferences}", "Ninguna");
            }
        }
        #endregion

        #region static void InsertTrainings(DocX document, List<AcademicEducation> educations)
        public static void InsertTrainings(DocX document, List<AcademicEducation> educations)
        {
            if (educations.Count > 0)
            {
                bool first = true;
                Formatting bold = new Formatting();
                bold.Bold = true;
                Formatting noBold = new Formatting();
                noBold.Bold = false;
                string trainings = "";
                document.ReplaceText("{Trainings}", "{Trainings}", false, System.Text.RegularExpressions.RegexOptions.None, bold);
                for (int i = 0; i < educations.Count; i++)
                {
                    if (educations[i].EducationType.Name == "ADICIONAL")
                    {
                        trainings = educations[i].TrainingName + ",";
                    }
                }
                document.ReplaceText("{Trainings}", trainings.Remove(trainings.Length - 1), false, System.Text.RegularExpressions.RegexOptions.None, noBold);
            }
            else
            {
                document.ReplaceText("{Trainings}", "Ninguna");
            }
        }
        #endregion
    }
}