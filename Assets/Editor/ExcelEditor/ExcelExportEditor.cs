using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace MuqDice
{
    using Editor;
    using MongoDB.Bson.IO;

    public class ExcelExportEditor
    {
        static string excelPath = "./Excel/";
        static string exportPath = "./Assets/Bundles/Data/";

        [MenuItem("Tools/导出配置")]
        private static void ExportAll()
        {
            AssetDatabase.Refresh();
            Debug.Log("导出成功");
        }

        public static void writeData(string fileName, object obj)
        {
            FileStream txt = new FileStream(exportPath + fileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(txt);
            sw.Write(MongoHelper.ToJson(obj, new JsonWriterSettings() { OutputMode = JsonOutputMode.Strict }));
            sw.Close();
            txt.Close();
        }

        static List<T> Export<T>(string fileName, string tableName)
        {
            List<T> result = new List<T>();
            IExcelDataReader reader;
            FileStream file = new FileStream(excelPath + fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            reader = ExcelReaderFactory.CreateReader(file);
            try
            {
                foreach (System.Data.DataTable sheet in reader.AsDataSet().Tables)
                {
                    if (sheet.TableName != tableName) continue;
                    int cellCount = sheet.Columns.Count;
                    for (int i = 2; i < sheet.Rows.Count; i++)
                    {
                        string Id = GetCellString(sheet, i, 0);
                        if (string.IsNullOrEmpty(Id) || Id.StartsWith("#"))
                        {
                            continue;
                        }

                        StringBuilder sb = new StringBuilder();
                        sb.Append("{");
                        for (int j = 0; j < cellCount; j++)
                        {
                            string fieldName = GetCellString(sheet, 0, j);
                            string fieldType = GetCellString(sheet, 1, j);
                            if (string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(fieldType))
                            {
                                continue;
                            }
                            string fieldValue = GetCellString(sheet, i, j);
                            if (string.IsNullOrEmpty(fieldValue))
                            {
                                continue;
                            }
                            if (fieldName == "Id" || fieldName == "_id")
                            {
                                fieldName = "_id";
                            }
                            sb.Append($"\"{fieldName}\":{Convert(fieldType, fieldValue)},");
                        }
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append("}");
                        //Debug.Log(sb);
                        T t = MongoHelper.FromJson<T>(sb.ToString());
                        //Debug.Log(MongoHelper.ToJson(t));
                        result.Add(t);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            //reader.Close();
            //reader.Dispose();
            file.Close();
            file.Dispose();
            return result;
        }
        private static string GetCellString(System.Data.DataTable sheet, int i, int j)
        {
            try
            {
                return sheet.Rows[i][j].ToString();
            }
            catch
            {
                return "";
            }
        }
        static StringBuilder sbCache = new StringBuilder();
        private static string Convert(string type, string value)
        {
            if (type.EndsWith("Enum[]"))
            {
                Type t = typeof(Database).Assembly.GetType(type.Substring(0, type.Length - 2));
                if (t == null) t = typeof(Database).Assembly.GetType(type.Substring(0, type.Length - 2));
                sbCache.Clear();
                string[] sp = value.Split(',');
                foreach (string s in sp) sbCache.Append((int)Enum.Parse(t, value));
                return $"[{sbCache.ToString()}]";
            }
            if (type.EndsWith("Enum"))
            {
                try
                {
                    Type t = typeof(Database).Assembly.GetType(type);
                    if (t == null) t = typeof(Database).Assembly.GetType(type);
                    return ((int)Enum.Parse(t, value)).ToString();
                }
                catch
                {
                    return value;
                }
            }
            switch (type)
            {
                case "int[]":
                case "int32[]":
                case "long[]":
                case "object[]":
                    return $"[{value}]";
                case "string[]":
                    sbCache.Clear();
                    string[] sp = value.Split(',');
                    foreach (string s in sp) sbCache.Append($"\"{s}\",");
                    sbCache.Remove(sbCache.Length - 1, 1);
                    return $"[{sbCache.ToString()}]";
                case "int":
                case "int32":
                case "int64":
                case "long":
                case "float":
                case "double":
                    return value;
                case "string":
                    return $"\"{value}\"";
                default:
                    if (type.EndsWith("[]"))
                    {
                        return $"[{value}]";
                    }
                    return value;
            }
        }
    }

}