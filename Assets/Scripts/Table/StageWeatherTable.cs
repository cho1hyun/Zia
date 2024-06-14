using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageWeatherTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1H3uOW2WOdfTCiGOYcon9ODX6WgQW8NPPLffapOq5kqM";
    public static int sheet = 1746563633;

    public int id;
    public int WthName;
    public Attribute_ buffAttribute1;
    public Status buffStatus1;
    public float buffValue1;
    public Attribute_ buffAttribute2;
    public Status buffStatus2;
    public float buffValue2;
}

public class StageWeatherTable_Parser
{
    public Dictionary<int, StageWeatherTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, StageWeatherTable> dic = new Dictionary<int, StageWeatherTable>();

        for (int i = 0; i < data.Count; i++)
        {
            StageWeatherTable tableData = new StageWeatherTable();

            tableData.id = int.TryParse(data[i][nameof(tableData.id)].ToString(), out int id) ? id : 0;
            tableData.WthName = int.TryParse(data[i][nameof(tableData.WthName)].ToString(), out int WthName) ? WthName : 0;
            tableData.buffAttribute1 = int.TryParse(data[i][nameof(tableData.buffAttribute1)].ToString(), out int buffAttribute1) ? (Attribute_)buffAttribute1 : Attribute_.None;
            tableData.buffStatus1 = Enum.TryParse(data[i][nameof(tableData.buffStatus1)].ToString(), out Status buffStatus1) ? buffStatus1 : Status.None;
            tableData.buffValue1 = float.TryParse(data[i][nameof(tableData.buffValue1)].ToString(), out float buffValue1) ? buffValue1 : 0;
            tableData.buffAttribute2 = int.TryParse(data[i][nameof(tableData.buffAttribute2)].ToString(), out int buffAttribute2) ? (Attribute_)buffAttribute2 : Attribute_.None;
            tableData.buffStatus2 = Enum.TryParse(data[i][nameof(tableData.buffStatus2)].ToString(), out Status buffStatus2) ? buffStatus2 : 0;
            tableData.buffValue2 = float.TryParse(data[i][nameof(tableData.buffValue2)].ToString(), out float buffValue2) ? buffValue2 : 0;

            if (!dic.ContainsKey(tableData.id))
                dic.Add(tableData.id, tableData);
        }

        return dic;
    }
}