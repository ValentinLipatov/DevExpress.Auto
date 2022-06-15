using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace XML
{
    public class XML : Entity
    {
        public XML() : base("XML", "Сравнение XML")
        {

        }

        protected Field<string> SQLServerName1 { get; set; }

        protected Field<string> SQLServerName2 { get; set; }

        protected Field<string> DatabaseName1 { get; set; }

        protected Field<string> DatabaseName2 { get; set; }

        protected Field<string> TableName1 { get; set; }

        protected Field<string> TableName2 { get; set; }

        protected Field<string> IdColumnName1 { get; set; }

        protected Field<string> IdColumnName2 { get; set; }

        protected Field<string> XMLColumnName1 { get; set; }

        protected Field<string> XMLColumnName2 { get; set; }

        protected Field<uint?> Id1 { get; set; }

        protected Field<uint?> Id2 { get; set; }

        protected Field<string> OutputPath { get; set; }

        protected Field<string> OutputFileName { get; set; }

        protected Field<string> TempFileName1 { get; set; }

        protected Field<string> TempFileName2 { get; set; }

        protected Field<bool> OpenFile { get; set; }

        protected Field<bool> DeleteTempFiles { get; set; }

        protected override void CreateFields()
        {
            base.CreateFields();

            SQLServerName1 = AddField(new Field<string>(this, "SQLServerName1", "SQL сервер", isMandatory: true));
            SQLServerName2 = AddField(new Field<string>(this, "SQLServerName2", "SQL сервер", isMandatory: true));

            DatabaseName1 = AddField(new Field<string>(this, "DatabaseName1", "База данных", isMandatory: true));
            DatabaseName2 = AddField(new Field<string>(this, "DatabaseName2", "База данных", isMandatory: true));

            TableName1 = AddField(new Field<string>(this, "TableName1", "Название таблицы", isMandatory: true));
            TableName2 = AddField(new Field<string>(this, "TableName2", "Название таблицы", isMandatory: true));

            IdColumnName1 = AddField(new Field<string>(this, "IdColumnName1", "Название столбца Id", isMandatory: true));
            IdColumnName2 = AddField(new Field<string>(this, "IdColumnName2", "Название столбца Id", isMandatory: true));

            XMLColumnName1 = AddField(new Field<string>(this, "XMLColumnName1", "Название столбца XML", isMandatory: true));
            XMLColumnName2 = AddField(new Field<string>(this, "XMLColumnName2", "Название столбца XML", isMandatory: true));

            Id1 = AddField(new Field<uint?>(this, "Id1", "Значение Id", isMandatory: true));
            Id2 = AddField(new Field<uint?>(this, "Id2", "Значение Id", isMandatory: true));

            OutputPath = AddField(new Field<string>(this, "OutputPath", "Папка", controlType: typeof(FolderStringControl), isMandatory: true));

            OutputFileName = AddField(new Field<string>(this, "OutputFileName", "Название результирующего файла", controlType: typeof(HTMLStringControl), isMandatory: true));

            TempFileName1 = AddField(new Field<string>(this, "TempFileName1", "Название файла c XML A", controlType: typeof(XMLStringControl), isMandatory: true));
            TempFileName2 = AddField(new Field<string>(this, "TempFileName2", "Название файла c XML Б", controlType: typeof(XMLStringControl), isMandatory: true));

            OpenFile = AddField(new Field<bool>(this, "OpenFile", "Открыть результирующий файл после успешного сравнения"));

            DeleteTempFiles = AddField(new Field<bool>(this, "DeleteTempFiles", "Удалить файлы с XML после сравнения"));
        }

        protected override void CreateActions()
        {
            base.CreateActions();

            AddAction(new PredicateAction(this, "Compare", "Сравнить", () => Compare()));
        }

        protected override void CreateGroups()
        {
            base.CreateGroups();

            AddGroup(new Group("Group1", "Параметры А"));
            AddGroup(new Group("Group2", "Параметры Б"));
            AddGroup(new Group("Group3", "Параметры результатов"));
        }

        protected void Compare()
        {
            try
            {
                string connectionString1 = $"Data Source={SQLServerName1.Value};Initial Catalog={DatabaseName1.Value};Integrated Security=true";
                string query1 = $"SELECT [{XMLColumnName1.Value}] FROM [{TableName1.Value}] WHERE [{IdColumnName1.Value}] = {Id1.Value}";
                using SqlConnection connection1 = new SqlConnection(connectionString1);
                SqlCommand command1 = new SqlCommand(query1, connection1);

                string connectionString2 = $"Data Source={SQLServerName2.Value};Initial Catalog={DatabaseName2.Value};Integrated Security=true";
                string query2 = $"SELECT [{XMLColumnName2.Value}] FROM [{TableName2.Value}] WHERE [{IdColumnName2.Value}] = {Id2.Value}";
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
                    MessageBox.Show("Значение XML А не найдено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    MessageBox.Show("Значение XML Б не найдено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (OutputPath.Value.Last() != '\\')
                    OutputPath.Value += "\\";

                string fileName1 = $"{OutputPath.Value}\\{TempFileName1.Value}";
                File.WriteAllText(fileName1, xml1);

                string fileName2 = $"{OutputPath.Value}\\{TempFileName2.Value}";
                File.WriteAllText(fileName2, xml2);

                string resultFileName = $"{OutputPath.Value}\\{OutputFileName.Value}";

                var process = Process.Start($"XmlUtil.exe", $"\"{fileName1}\" \"{fileName2}\" \"{resultFileName}\"");
                process.WaitForExit();

                if (DeleteTempFiles.Value)
                {
                    File.Delete(fileName1);
                    File.Delete(fileName2);
                }

                if (OpenFile.Value)
                {
                    var openFileProcess = new Process();
                    openFileProcess.StartInfo = new ProcessStartInfo(resultFileName);
                    openFileProcess.StartInfo.UseShellExecute = true;
                    openFileProcess.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}