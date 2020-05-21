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
            var mBuff = Export<BuffModel>("data.xlsx", "buff");
            var mDiceFace = Export<DiceFaceModel>("data.xlsx", "diceFace");
            var mDice = Export<DiceModel>("data.xlsx", "dice");
            var mMagic = Export<MagicModel>("data.xlsx", "magic");
            var mRandomDice = Export<RandomDiceModel>("data.xlsx", "randomDice");
            var mSkill = Export<SkillModel>("data.xlsx", "skill");
            var mSystem = Export<SystemModel>("data.xlsx", "system");
            var mTeam = Export<TeamModel>("data.xlsx", "team");
            var mUnit = Export<UnitModel>("data.xlsx", "unit");

            var buffs = Export<BuffConfig>("data.xlsx", "buff");
            var diceFaces = Export<DiceFaceConfig>("data.xlsx", "diceFace");
            var dices = Export<DiceConfig>("data.xlsx", "dice");
            for (int i = 0; i < dices.Count; i++)
            {
                if (mDice[i]._Faces != null)
                {
                    dices[i].Faces = mDice[i]._Faces.Select(x => mDiceFace.FindIndex(s => s._Id == x)).ToArray();
                    if (dices[i].Faces.Contains(-1)) throw new Exception($"Dice {dices[i]._Id } 出错： Faces");
                }
            }
            var magics = Export<MagicConfig>("data.xlsx", "magic");
            for (int i = 0; i < magics.Count; i++)
            {
                if (mMagic[i]._SkillId != null)
                {
                    magics[i].SkillId = mSkill.FindIndex(s => s._Id == mMagic[i]._SkillId);
                    if (magics[i].SkillId == -1) throw new Exception($"Magic { magics[i]._Id } 出错： SkillId");
                }
            }
            var randomDices = Export<RandomDiceConfig>("data.xlsx", "randomDice");
            var skills = Export<SkillConfig>("data.xlsx", "skill");
            for (int i = 0; i < skills.Count; i++)
            {
                if (mSkill[i]._Buffs != null)
                {
                    skills[i].Buffs = mSkill[i]._Buffs.Select(x => mBuff.FindIndex(s => s._Id == x)).ToArray();
                    if (skills[i].Buffs.Contains(-1)) throw new Exception($"Skill {skills[i]._Id } 出错： Buffs");
                }
            }
            var systems = Export<SystemConfig>("data.xlsx", "system");
            var teams = Export<TeamConfig>("data.xlsx", "team");
            for (int i = 0; i < teams.Count; i++)
            {
                if (mTeam[i]._UnitIds != null)
                {
                    teams[i].UnitIds = mTeam[i]._UnitIds.Select(x => mUnit.FindIndex(s => s._Id == x)).ToArray();
                    if (teams[i].UnitIds.Contains(-1)) throw new Exception($"Team {teams[i]._Id } 出错： UnitIds");
                }
            }
            var units = Export<UnitConfig>("data.xlsx", "unit");
            for (int i = 0; i < units.Count; i++)
            {
                if (mUnit[i]._Skills != null)
                {
                    units[i].Skills = mUnit[i]._Skills.Select(x => mSkill.FindIndex(s => s._Id == x)).ToArray();
                    if (units[i].Skills.Contains(-1)) throw new Exception($"Unit {units[i]._Id } 出错： Skills");
                }
            }

            writeData("buff.txt", buffs);
            writeData("diceFace.txt", diceFaces);
            writeData("magic.txt", magics);
            writeData("dice.txt", dices);
            writeData("randomDice.txt", randomDices);
            writeData("skill.txt", skills);
            writeData("system.txt", systems);
            writeData("team.txt", teams);
            writeData("unit.txt", units);

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
                if (t == null) t = typeof(Database).Assembly.GetType("MuqDice." + type.Substring(0, type.Length - 2));
                sbCache.Clear();
                string[] sp = value.Split(',');
                foreach (string s in sp) 
                    sbCache.Append((int)Enum.Parse(t, s) + ",");
                sbCache.Remove(sbCache.Length - 1, 1);
                return $"[{sbCache.ToString()}]";
            }
            if (type.EndsWith("Enum"))
            {
                try
                {
                    Type t = typeof(Database).Assembly.GetType(type);
                    if (t == null) t = typeof(Database).Assembly.GetType("MuqDice." + type);
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