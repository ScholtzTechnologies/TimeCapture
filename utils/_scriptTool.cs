using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeCapture.utils
{
    public class _scriptTool
    {
        public void RunInScripts()
        {
            string scriptsFolder = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\DB", "Scripts"));

            Dictionary<string, string> dSQLFiles = CollectSQLFromFiles(scriptsFolder);
            List<string> lErrors = new List<string>();
            StringBuilder sbError = new StringBuilder();

            if (dSQLFiles != null)
            {
                foreach (var sql in dSQLFiles)
                {
                    try
                    {
                        new DB.Access().ExecuteNonQuery(sql.Value);
                    }
                    catch
                    {
                        lErrors.Add(sql.Key);
                    }
                }

                if (lErrors.Count > 0)
                {
                    sbError.Append("Please see below tables that contained errors while generating:");
                    foreach (var error in lErrors)
                    {
                        sbError.AppendLine(error);
                    }
                    MessageBox.Show(sbError.ToString());
                }
            }
        }

        private Dictionary<string, string> CollectSQLFromFiles(string folderPath)
        {
            Dictionary<string, string> dSQLFiles = new Dictionary<string, string>();
            try
            {
                string[] sqlFiles = Directory.GetFiles(folderPath, "*.sql");

                foreach (string filePath in sqlFiles)
                {
                    string sqlContent = ReadFileContent(filePath);

                    dSQLFiles.Add(Path.GetFileName(filePath), sqlContent);
                    Console.WriteLine($"SQL from file '{Path.GetFileName(filePath)}':\n{sqlContent}\n");
                }
                return dSQLFiles;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when generating tables: {Environment.NewLine} {ex.Message}");
                return null;
            }
        }

        private string ReadFileContent(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file '{filePath}': {ex.Message}");
                return string.Empty;
            }
        }
    }
}
