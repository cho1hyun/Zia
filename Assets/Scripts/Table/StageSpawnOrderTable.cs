using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawnOrderTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1H3uOW2WOdfTCiGOYcon9ODX6WgQW8NPPLffapOq5kqM";
    public static int sheet = 1060250954;

    public int Id;
    public int maxSpawn;
    public int SpawnSet1_ID;
    public int SpawnOrder1_GroupPos;
    public int SpawnSet2_ID;
    public int SpawnOrder2_GroupPos;
    public int SpawnSet3_ID;
    public int SpawnOrder3_GroupPos;
    public int SpawnSet4_ID;
    public int SpawnOrder4_GroupPos;
    public int SpawnSet5_ID;
    public int SpawnOrder5_GroupPos;
}

public class StageSpawnOrderTable_Parser
{
    public Dictionary<int, StageSpawnOrderTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, StageSpawnOrderTable> dic = new Dictionary<int, StageSpawnOrderTable>();

        for (int i = 0; i < data.Count; i++)
        {
            StageSpawnOrderTable tableData = new StageSpawnOrderTable();

            tableData.Id = int.TryParse(data[i][nameof(tableData.Id)].ToString(), out int Id) ? Id : 0;
            tableData.maxSpawn = int.TryParse(data[i][nameof(tableData.maxSpawn)].ToString(), out int maxSpawn) ? maxSpawn : 0;
            tableData.SpawnSet1_ID = int.TryParse(data[i][nameof(tableData.SpawnSet1_ID)].ToString(), out int SpawnSet1_ID) ? SpawnSet1_ID : 0;
            tableData.SpawnOrder1_GroupPos = int.TryParse(data[i][nameof(tableData.SpawnOrder1_GroupPos)].ToString(), out int SpawnOrder1_GroupPos) ? SpawnOrder1_GroupPos : 0;
            tableData.SpawnSet2_ID = int.TryParse(data[i][nameof(tableData.SpawnSet2_ID)].ToString(), out int SpawnSet2_ID) ? SpawnSet2_ID : 0;
            tableData.SpawnOrder2_GroupPos = int.TryParse(data[i][nameof(tableData.SpawnOrder2_GroupPos)].ToString(), out int SpawnOrder2_GroupPos) ? SpawnOrder2_GroupPos : 0;
            tableData.SpawnSet3_ID = int.TryParse(data[i][nameof(tableData.SpawnSet3_ID)].ToString(), out int SpawnSet3_ID) ? SpawnSet3_ID : 0;
            tableData.SpawnOrder3_GroupPos = int.TryParse(data[i][nameof(tableData.SpawnOrder3_GroupPos)].ToString(), out int SpawnOrder3_GroupPos) ? SpawnOrder3_GroupPos : 0;
            tableData.SpawnSet4_ID = int.TryParse(data[i][nameof(tableData.SpawnSet4_ID)].ToString(), out int SpawnSet4_ID) ? SpawnSet4_ID : 0;
            tableData.SpawnOrder4_GroupPos = int.TryParse(data[i][nameof(tableData.SpawnOrder4_GroupPos)].ToString(), out int SpawnOrder4_GroupPos) ? SpawnOrder4_GroupPos : 0;

            if (!dic.ContainsKey(tableData.Id))
                dic.Add(tableData.Id, tableData);
        }

        return dic;
    }
}