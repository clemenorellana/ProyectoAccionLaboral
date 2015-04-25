using AccionLaboral.Models;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccionLaboral.Helpers.Lucene
{
    public static class GoLucene
    {
        // properties
        public static string _luceneDir =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "lucene_index");
        private static FSDirectory _directoryTemp;
        private static FSDirectory _directory
        {
            get
            {
                if (_directoryTemp == null) _directoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));
                if (IndexWriter.IsLocked(_directoryTemp)) IndexWriter.Unlock(_directoryTemp);
                var lockFilePath = Path.Combine(_luceneDir, "write.lock");
                if (File.Exists(lockFilePath)) File.Delete(lockFilePath);
                return _directoryTemp;
            }
        }

        // search methods
        public static IEnumerable<Client> GetAllIndexRecords()
        {
            // validate search index
            if (!System.IO.Directory.EnumerateFiles(_luceneDir).Any()) return new List<Client>();

            // set up lucene searcher
            var searcher = new IndexSearcher(_directory, false);
            var reader = IndexReader.Open(_directory, false);
            var docs = new List<Document>();
            var term = reader.TermDocs();
            // v 2.9.4: use 'hit.Doc()'
            // v 3.0.3: use 'hit.Doc'
            while (term.Next()) docs.Add(searcher.Doc(term.Doc));
            reader.Dispose();
            searcher.Dispose();
            return _mapLuceneToDataList(docs);
        }
        public static IEnumerable<Client> Search(string input, string[] fieldsName = null)
        {
            fieldsName = fieldsName ?? new string[0];
            if (string.IsNullOrEmpty(input)) return new List<Client>();
            
            var terms = input.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim() + "*");
            input = string.Join(" ", terms);

            return _search(input, fieldsName);
        }
        public static IEnumerable<Client> SearchDefault(string input, string [] fieldsName = null)
        {
            fieldsName = fieldsName ?? new string[0];
            return string.IsNullOrEmpty(input) ? new List<Client>() : _search(input, fieldsName);
        }

        // main search method
        private static IEnumerable<Client> _search(string searchQuery, string[] searchFields=null)
        {
            searchFields = searchFields ?? new string[0];
            // validation
            if (string.IsNullOrEmpty(searchQuery.Replace("*", "").Replace("?", ""))) return new List<Client>();

            // set up lucene searcher
            using (var searcher = new IndexSearcher(_directory, false))
            {
                var hits_limit = 1000;
                var analyzer = new StandardAnalyzer(global::Lucene.Net.Util.Version.LUCENE_30);

                // search by single field
                if (searchFields.Length == 1)
                {
                    var parser = new QueryParser(global::Lucene.Net.Util.Version.LUCENE_30, searchFields[0], analyzer);
                    var query = parseQuery(searchQuery, parser);
                    var hits = searcher.Search(query, hits_limit).ScoreDocs;
                    var results = _mapLuceneToDataList(hits, searcher);
                    analyzer.Close();
                    searcher.Dispose();
                    return results;
                }
                // search by multiple fields (ordered by RELEVANCE)
                else if (searchFields.Length > 1)
                {
                    var parser = new MultiFieldQueryParser
                        (global::Lucene.Net.Util.Version.LUCENE_30, searchFields, analyzer);

                    var query = parseQuery(searchQuery, parser);
                    var hits = searcher.Search(query, null, hits_limit, Sort.INDEXORDER).ScoreDocs;
                    var results = _mapLuceneToDataList(hits, searcher);
                    analyzer.Close();
                    searcher.Dispose();
                    return results;
                }
                else
                {
                    var parser = new MultiFieldQueryParser
                        (global::Lucene.Net.Util.Version.LUCENE_30, new[] { "ClientId", "CorrelativeCode", "IdentityNumber",
                                                                             "IdentityNumber","FirstName","LastName","Age",
                                                                             "Gender","Email","Hobby","Cellphone","HomePhone",
                                                                             "CurrentStudies","WageAspiration","DesiredEmployment"
                                                                             ,"CompaniesWithPreviouslyRequested"}, analyzer);

                    var query = parseQuery(searchQuery, parser);
                    var hits = searcher.Search(query, null, hits_limit, Sort.INDEXORDER).ScoreDocs;
                    var results = _mapLuceneToDataList(hits, searcher);
                    analyzer.Close();
                    searcher.Dispose();
                    return results;
                }
            }
        }
        private static Query parseQuery(string searchQuery, QueryParser parser)
        {
            Query query;
            try
            {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException)
            {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim()));
            }
            return query;
        }

        // map Lucene search index to data
        private static IEnumerable<Client> _mapLuceneToDataList(IEnumerable<Document> hits)
        {
            return hits.Select(_mapLuceneDocumentToData).ToList();
        }
        private static IEnumerable<Client> _mapLuceneToDataList(IEnumerable<ScoreDoc> hits, IndexSearcher searcher)
        {
            return hits.Select(hit => _mapLuceneDocumentToData(searcher.Doc(hit.Doc))).ToList();
        }
        private static Client _mapLuceneDocumentToData(Document doc)
        {
            Client client = new Client();
            try
            {
                    client.ClientId = Convert.ToInt32(doc.Get("ClientId"));
                    client.Photo = (!string.IsNullOrEmpty(doc.Get("Photo"))) ? System.Text.Encoding.ASCII.GetBytes(doc.Get("Photo")): null;
                    client.CorrelativeCode = doc.Get("CorrelativeCode");
                    client.IdentityNumber = doc.Get("IdentityNumber");
                    client.FirstName = doc.Get("FirstName");
                    client.LastName = doc.Get("LastName");
                    client.StateId = Convert.ToInt32(doc.Get("StateId"));
                    client.Age = Convert.ToInt32(doc.Get("Age"));
                    client.Gender = doc.Get("Gender");
                    client.Email = doc.Get("Email");
                    client.Hobby = doc.Get("Hobby");
                    client.Cellphone = doc.Get("Cellphone");
                    client.HomePhone = doc.Get("HomePhone");
                    client.CurrentStudies = doc.Get("CurrentStudies");
                    client.WageAspiration = Convert.ToDouble(doc.Get("WageAspiration"));
                    client.DesiredEmployment = doc.Get("DesiredEmployment");
                    client.CompaniesWithPreviouslyRequested = doc.Get("CompaniesWithPreviouslyRequested");
                #region Llenar datos de cliente en una sola linea
                /*client = new Client
                {
                    ClientId = Convert.ToInt32(doc.Get("ClientId")),
                    Photo = System.Text.Encoding.ASCII.GetBytes(doc.Get("Photo")),
                    CorrelativeCode = doc.Get("CorrelativeCode"),
                    IdentityNumber = doc.Get("IdentityNumber"),
                    FirstName = doc.Get("FirstName"),
                    LastName = doc.Get("LastName"),
                    StateId = Convert.ToInt32(doc.Get("StateId")),
                    Age = Convert.ToInt32(doc.Get("Age")),
                    Gender = doc.Get("Gender"),
                    Email = doc.Get("Email"),
                    Hobby = doc.Get("Hobby"),
                    Cellphone = doc.Get("Cellphone"),
                    HomePhone = doc.Get("HomePhone"),
                    CurrentStudies = doc.Get("CurrentStudies"),
                    WageAspiration = Convert.ToDouble(doc.Get("WageAspiration")),
                    DesiredEmployment = doc.Get("DesiredEmployment"),
                    CompaniesWithPreviouslyRequested = doc.Get("CompaniesWithPreviouslyRequested")

                };*/
                #endregion
            }
            catch (Exception)
            {
                
                throw;
            }
            return client;
        }

        // add/update/clear search index data 
        public static void AddUpdateLuceneIndex(Client sampleData)
        {
            
                AddUpdateLuceneIndex(new List<Client> { sampleData });
            
        }
        public static void AddUpdateLuceneIndex(IEnumerable<Client> sampleDatas)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(global::Lucene.Net.Util.Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // add data to lucene search index (replaces older entries if any)
                foreach (var sampleData in sampleDatas) _addToLuceneIndex(sampleData, writer);

                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }
        public static void ClearLuceneIndexRecord(int record_id)
        {
            // init lucene
            var analyzer = new StandardAnalyzer(global::Lucene.Net.Util.Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                // remove older index entry
                var searchQuery = new TermQuery(new Term("ClientId", record_id.ToString()));
                writer.DeleteDocuments(searchQuery);

                // close handles
                analyzer.Close();
                writer.Dispose();
            }
        }
        public static bool ClearLuceneIndex()
        {
            try
            {
                var analyzer = new StandardAnalyzer(global::Lucene.Net.Util.Version.LUCENE_30);
                using (var writer = new IndexWriter(_directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                {
                    // remove older index entries
                    writer.DeleteAll();

                    // close handles
                    analyzer.Close();
                    writer.Dispose();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public static void Optimize()
        {
            var analyzer = new StandardAnalyzer(global::Lucene.Net.Util.Version.LUCENE_30);
            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                analyzer.Close();
                writer.Optimize();
                writer.Dispose();
            }
        }
        private static void _addToLuceneIndex(Client sampleData, IndexWriter writer)
        {
            // remove older index entry
            var searchQuery = new TermQuery(new Term("ClientId", sampleData.ClientId.ToString()));
            writer.DeleteDocuments(searchQuery);

            // add new index entry
            var doc = new Document();

            // add lucene fields mapped to db fields
            doc.Add(new Field("ClientId", sampleData.ClientId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            if (sampleData.Photo!=null)
                doc.Add(new Field("Photo", Encoding.ASCII.GetString(sampleData.Photo, 0, sampleData.Photo.Length), Field.Store.YES, Field.Index.NOT_ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.CorrelativeCode))
            doc.Add(new Field("CorrelativeCode", sampleData.CorrelativeCode, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.IdentityNumber))
            doc.Add(new Field("IdentityNumber", sampleData.IdentityNumber, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.FirstName))
            doc.Add(new Field("FirstName", sampleData.FirstName, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.LastName))
            doc.Add(new Field("LastName", sampleData.LastName, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.Age.ToString()))
            doc.Add(new Field("Age", sampleData.Age.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.Gender))
            doc.Add(new Field("Gender", sampleData.Gender, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.Email))
            doc.Add(new Field("Email", sampleData.Email, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.CompleteAddress))
                doc.Add(new Field("CompleteAddress", sampleData.CompleteAddress, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.Hobby))
            doc.Add(new Field("Hobby", sampleData.Hobby, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.Cellphone))
            doc.Add(new Field("Cellphone", sampleData.Cellphone, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.HomePhone))
            doc.Add(new Field("HomePhone", sampleData.HomePhone, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.CurrentStudies))
            doc.Add(new Field("CurrentStudies", sampleData.CurrentStudies, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.WageAspiration.ToString()))
            doc.Add(new Field("WageAspiration", sampleData.WageAspiration.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.DesiredEmployment))
            doc.Add(new Field("DesiredEmployment", sampleData.DesiredEmployment, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.CompaniesWithPreviouslyRequested))
            doc.Add(new Field("CompaniesWithPreviouslyRequested", sampleData.CompaniesWithPreviouslyRequested, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(sampleData.RejectionDescription))
            doc.Add(new Field("RejectionDescription", sampleData.RejectionDescription, Field.Store.YES, Field.Index.ANALYZED));
            if (sampleData.City!=null)
            doc.Add(new Field("City", sampleData.City.Name, Field.Store.YES, Field.Index.ANALYZED));
            if(sampleData.State!=null)
                doc.Add(new Field("State", sampleData.State.Name.ToString(), Field.Store.YES, Field.Index.ANALYZED));
            if (sampleData.Employee!=null)
            doc.Add(new Field("Employee", sampleData.Employee.EmployeeAlias, Field.Store.YES, Field.Index.ANALYZED));
            if (sampleData.Company!=null)
            doc.Add(new Field("Company", sampleData.Company.Name, Field.Store.YES, Field.Index.ANALYZED));

            string programs = "";
            if (sampleData.KnownPrograms!=null)
            foreach (KnownProgram program in sampleData.KnownPrograms.ToList())
            {
                programs += program.Name + ",";
            }

            string languages = "";
            if (sampleData.Languages != null)
            foreach (KnownLanguage language in sampleData.Languages.ToList())
            {
                languages += language.Language.Name + " , " + language.Percentage + "%";
            }

            if (!string.IsNullOrEmpty(programs))
                doc.Add(new Field("Programs", programs, Field.Store.YES, Field.Index.ANALYZED));
            if (!string.IsNullOrEmpty(languages))
                doc.Add(new Field("Languages", languages, Field.Store.YES, Field.Index.ANALYZED));

            string educations = "";

            string careers = "";
            if (sampleData.AcademicEducations != null)
            {
                foreach (AcademicEducation education in sampleData.AcademicEducations)
                {
                    if (education.AcademicLevel!=null)
                    educations += education.AcademicLevel.Name + " , ";
                    if (education.Career != null)
                        careers += education.Career.Name + " , ";

                }
                if (!string.IsNullOrEmpty(languages))
                    doc.Add(new Field("Educations", educations, Field.Store.YES, Field.Index.ANALYZED));
                if (!string.IsNullOrEmpty(languages))
                    doc.Add(new Field("Careers", careers, Field.Store.YES, Field.Index.ANALYZED));

                sampleData.WorkExperiences = sampleData.WorkExperiences ?? null;
                int yearsExperience = (sampleData.WorkExperiences == null) ? 0 : GetYearsExperience(sampleData.WorkExperiences);
                if(yearsExperience>0)
                    doc.Add(new Field("Experience", yearsExperience.ToString() + " años", Field.Store.YES, Field.Index.ANALYZED));
            }

            // add entry to index
            writer.AddDocument(doc);
          
        }

        public static Int32 GetYearsExperience(ICollection<WorkExperience> workExperiences)
        {
            int yearsExperience = 0;
            foreach (WorkExperience experience in workExperiences)
                yearsExperience += GetYears(experience.StartDate) - GetYears(experience.EndDate);

            return yearsExperience;
        }

        public static Int32 GetYears(this DateTime dateOf)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOf.Year * 100 + dateOf.Month) * 100 + dateOf.Day;

            return (a - b) / 10000;
        }

    }
}