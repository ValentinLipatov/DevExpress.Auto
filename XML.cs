using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace XML
{
    [Control("Сравнение XML")]
    public class XML : BusinessObject
    {
        [ValidationRuleRequired]
        [Control("SQL сервер")]
        protected string SQLServerName1 { get; set; }

        [ValidationRuleRequired]
        [Control("SQL сервер")]
        protected string SQLServerName2 { get; set; }

        [ValidationRuleRequired]
        [Control("База данных")]
        protected string DatabaseName1 { get; set; }

        [ValidationRuleRequired]
        [Control("База данных")]
        protected string DatabaseName2 { get; set; }

        [ValidationRuleRequired]
        [Control("Название таблицы")]
        protected string TableName1 { get; set; }

        [ValidationRuleRequired]
        [Control("Название таблицы")]
        protected string TableName2 { get; set; }

        [ValidationRuleRequired]
        [Control("Название столбца Id")]
        public string IdColumnName1 { get; set; }

        [ValidationRuleRequired]
        [Control("Название столбца Id")]
        protected string IdColumnName2 { get; set; }

        [ValidationRuleRequired]
        [Control("Название столбца XML")]
        protected string XMLColumnName1 { get; set; }

        [ValidationRuleRequired]
        [Control("Название столбца XML")]
        protected string XMLColumnName2 { get; set; }

        [ValidationRuleRequired]
        [Control("Значение Id")]
        protected int? Id1 { get; set; }

        [ValidationRuleRequired]
        [Control("Значение Id")]
        public int? Id2 { get; set; }

        [ValidationRuleRequired]
        [Control("Папка", ControlType = typeof(FolderTextControl))]
        public string OutputPath { get; set; }

        [ValidationRuleRequired]
        [Control("Название результирующего файла", ControlType = typeof(HTMLTextControl))]
        public string OutputFileName { get; set; }

        [ValidationRuleRequired]
        [Control("Название файла c XML A", ControlType = typeof(XMLTextControl))]
        public string TempFileName1 { get; set; }

        [ValidationRuleRequired]
        [Control("Название файла c XML Б", ControlType = typeof(XMLTextControl))]
        public string TempFileName2 { get; set; }

        [Control("Открыть результирующий файл после успешного сравнения")]
        public bool OpenFile { get; set; }

        [Control("Удалить файлы с XML после сравнения")]
        public bool DeleteTempFiles { get; set; }

        [Control("Сравнить")]
        public void Compare()
        {
            try
            {
                if (string.IsNullOrEmpty(Path.GetExtension(OutputFileName)))
                    OutputFileName += ".html";

                string connectionString1 = $"Data Source={SQLServerName1};Initial Catalog={DatabaseName1};Integrated Security=true";
                string query1 = $"SELECT [{XMLColumnName1}] FROM [{TableName1}] WHERE [{IdColumnName1}] = {Id1}";
                using SqlConnection connection1 = new SqlConnection(connectionString1);
                SqlCommand command1 = new SqlCommand(query1, connection1);

                string connectionString2 = $"Data Source={SQLServerName2};Initial Catalog={DatabaseName2};Integrated Security=true";
                string query2 = $"SELECT [{XMLColumnName2}] FROM [{TableName2}] WHERE [{IdColumnName2}] = {Id2}";
                using SqlConnection connection2 = new SqlConnection(connectionString2);
                SqlCommand command2 = new SqlCommand(query2, connection2);

                connection1.Open();
                SqlDataReader reader1 = command1.ExecuteReader();
                string xml1 = null;
                while (reader1.Read())
                {
                    xml1 = reader1[0].ToString();
                    break;
                }
                reader1.Close();
                connection1.Close();

                if (string.IsNullOrEmpty(xml1))
                {
                    XtraMessageBox.Show(Form, "Значение XML А не найдено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                connection2.Open();
                SqlDataReader reader2 = command2.ExecuteReader();
                string xml2 = null;
                while (reader2.Read())
                {
                    xml2 = reader2[0].ToString();
                    break;
                }
                reader2.Close();
                connection2.Close();

                if (string.IsNullOrEmpty(xml2))
                {
                    XtraMessageBox.Show(Form, "Значение XML А не найдено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (OutputPath.Last() != '\\')
                    OutputPath += "\\";

                string fileName1 = $"{OutputPath}\\{TempFileName1}";
                File.WriteAllText(fileName1, xml1);

                string fileName2 = $"{OutputPath}\\{TempFileName2}";
                File.WriteAllText(fileName2, xml2);

                string resultFileName = $"{OutputPath}\\{OutputFileName}";

                var process = Process.Start($"XmlUtil.exe", $"\"{fileName1}\" \"{fileName2}\" \"{resultFileName}\"");
                process.WaitForExit();

                if (DeleteTempFiles)
                {
                    File.Delete(fileName1);
                    File.Delete(fileName2);
                }

                if (OpenFile)
                {
                    var openFileProcess = new Process();
                    openFileProcess.StartInfo = new ProcessStartInfo(resultFileName);
                    openFileProcess.StartInfo.UseShellExecute = true;
                    openFileProcess.Start();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(Form, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override void CreateLayouts()
        {
            Form.AddGroup("Group1", "Параметры А");
            Form.AddGroup("Group2", "Параметры Б");
            Form.AddGroup("Group3", "Параметры результатов");
        }
    }
}