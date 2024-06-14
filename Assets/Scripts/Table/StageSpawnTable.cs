using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawnTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1H3uOW2WOdfTCiGOYcon9ODX6WgQW8NPPLffapOq5kqM";
    public static int sheet = 724643132;

    public int spawnSetID;
    public int mob1_ID;
    public int mob1_Lv;
    public int mob1No;
    public int mob2_ID;
    public int mob2_Lv;
    public int mob2No;
}

public class StageSpawnTable_Parser
{
    public Dictionary<int, StageSpawnTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, StageSpawnTable> dic = new Dictionary<int, StageSpawnTable>();

        for (int i = 0; i < data.Count; i++)
        {
            StageSpawnTable tableData = new StageSpawnTable();

            tableData.spawnSetID = int.TryParse(data[i][nameof(tableData.spawnSetID)].ToString(), out int spawnSetID) ? spawnSetID : 0;
            tableData.mob1_ID = int.TryParse(data[i][nameof(tableData.mob1_ID)].ToString(), out int mob1_ID) ? mob1_ID : 0;
            tableData.mob1_Lv = int.TryParse(data[i][nameof(tableData.mob1_Lv)].ToString(), out int mob1_Lv) ? mob1_Lv : 0;
            tableData.mob1No = int.TryParse(data[i][nameof(tableData.mob1No)].ToString(), out int mob1No) ? mob1No : 0;
            tableData.mob2_ID = int.TryParse(data[i][nameof(tableData.mob2_ID)].ToString(), out int mob2_ID) ? mob2_ID : 0;
            tableData.mob2_Lv = int.TryParse(data[i][nameof(tableData.mob2_Lv)].ToString(), out int mob2_Lv) ? mob2_Lv : 0;
            tableData.mob2No = int.TryParse(data[i][nameof(tableData.mob2No)].ToString(), out int mob2No) ? mob2No : 0;

            if (!dic.ContainsKey(tableData.spawnSetID))
                dic.Add(tableData.spawnSetID, tableData);
        }

        return dic;
    }
}