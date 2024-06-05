using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    None = 0,
    KOR = 1,
    ENG = 2,
    JAP = 3,
}

public class LocalizeTextTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1nK7Gqg9pPeJ4UCRqbSGTVQn2l8pD2qmSlyBWTEOb4B8";
    public static int sheet = 0;

    public int id;
    public string kor;
    public string eng;
    public string jap;
}

public class LocalizeTextTable_Parser
{
    public Dictionary<int, LocalizeTextTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, LocalizeTextTable> dic = new Dictionary<int, LocalizeTextTable>();

        for (var i = 0; i < data.Count; i++)
        {
            LocalizeTextTable tableData = new LocalizeTextTable();

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