using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioTextTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1siOEyNftKKUtOfpN6gBoQppihAHFVm0PgnYhkH20ld4";
    public static int sheet = 661317002;

    public int id;
    public string kor;
    public string eng;
    public string jap;
}

public class ScenarioTextTable_Parser
{
    public Dictionary<int, ScenarioTextTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, ScenarioTextTable> dic = new Dictionary<int, ScenarioTextTable>();

        for (var i = 0; i < data.Count; i++)
        {
            ScenarioTextTable tableData = new ScenarioTextTable();

            tableData.id = int.TryParse(data[i][nameof(tableData.id)].ToString(), out int id) ? id : 0;
            tableData.kor = (string)data[i][nameof(tableData.kor)];
            tableData.eng = (string)data[i][nameof(tableData.eng)];
            tableData.jap = (string)data[i][nameof(tableData.jap)];

            if (!dic.ContainsKey(tableData.id))
                dic.Add(tableData.id, tableData);
        }

        return dic;
    }
}