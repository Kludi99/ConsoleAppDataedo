namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class DataReader
    {
        IEnumerable<Database> Databases;
        IEnumerable<Table> Tables;
        IEnumerable<Column> Columns;
        public DataReader()
        {
            Databases = new List<Database>();
            Tables = new List<Table>();
            Columns = new List<Column>();
        }

        public void ImportAndPrintData(string fileToImport, bool printData = true)
        {
            try
            {
                var streamReader = new StreamReader(fileToImport);

                var importedLines = new List<string>();
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    if (!string.IsNullOrEmpty(line))
                        importedLines.Add(line);
                }

                for (int i = 0; i < importedLines.Count; i++)
                {
                    var importedLine = importedLines[i];
                    var values = importedLine.Split(';');
                    if (values.Length != 7)
                        continue;

                    var importedObject = CreateObject(values);
                    if (importedObject == null)
                        continue;

                    NormalizeData(importedObject);
                    if (importedObject is Database)
                    {
                        ((List<Database>)Databases).Add(importedObject as Database);
                    }
                    else if (importedObject is Table)
                    {
                        ((List<Table>)Tables).Add(importedObject as Table);
                    }
                    else if(importedObject is Column)
                        ((List<Column>)Columns).Add(importedObject as Column);

                }
                AssignChildren();
                if (printData)
                    PrintData();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in reading file {e}");
            }
            Console.ReadLine();
        }

        private ImportedObject CreateObject(string[] values)
        {
            if (values[0].ToUpper() == ObjectTypes.DATABASE.ToString())
            {
                var database = new Database
                {
                    Type = values[0],
                    Name = values[1]
                };
                return database;
            }
            else if (values[0].ToUpper() == ObjectTypes.TABLE.ToString())
            {
                var table = new Table
                {
                    Type = values[0],
                    Name = values[1],
                    Schema = values[2],
                    ParentName = values[3],
                    ParentType = values[4],
                    DataType = values[5],
                    IsNullable = values[6]
                };
                return table;
            }
            else if (values[0].ToUpper() == ObjectTypes.COLUMN.ToString())
            {
                var column = new Column
                {
                    Type = values[0],
                    Name = values[1],
                    Schema = values[2],
                    ParentName = values[3],
                    ParentType = values[4],
                    DataType = values[5],
                    IsNullable = values[6]
                };
                return column;
            }
            return null;
        }    

        private void NormalizeData(ImportedObject importedObject) 
        {
            // clear and correct imported data
            importedObject.Type = NormalizeProperty(importedObject.Type);
            importedObject.Name = NormalizeProperty(importedObject.Name);
            importedObject.Schema = NormalizeProperty(importedObject.Schema);
            importedObject.ParentName = NormalizeProperty(importedObject.ParentName);
            importedObject.ParentType = NormalizeProperty(importedObject.ParentType);

        }

        private string NormalizeProperty(string prop)
        {
            return prop?.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();
        }

        public void AssignChildren()
        {
            // assign number of children
            foreach (var database in Databases)
            {
                var tables = Tables.Where(x => x.ParentType == database.Type && x.ParentName == database.Name);
                database.Tables = tables.ToList();
                database.NumberOfChildren = database.Tables.Count;  
            }

            foreach (var table in Tables)
            {
                var columns = Columns.Where(x => x.ParentType == table.Type && x.ParentName == table.Name);
                table.Columns = columns.ToList();
                table.NumberOfChildren = table.Columns.Count;
            }
        }


        private void PrintData()
        {
            foreach (var database in Databases)
            {
                Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");

                foreach (var table in database.Tables)
                {
                    Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
                    foreach (var column in table.Columns)
                    {
                        Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                    }
                }
            }
        }
    }
}
