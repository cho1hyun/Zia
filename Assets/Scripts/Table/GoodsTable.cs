using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rank
{
    None,
    A,
    S,
    SR,
    SSR,
}

public class GoodsTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1hiYAEjQ_zRCI0jAD2v7oqjBMEdfEC81KkVMZawhieXc";
    public static int sheet = 0;

    public int id;
    public int name;
    public int desc;
    public Rank rank;
}

public class GoodsTable_Parser
{
    public Dictionary<int, GoodsTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, GoodsTable> dic = new Dictionary<int, GoodsTable>();

        for (int i = 0; i < data.Count; i++)
        {
            GoodsTable tableData = new GoodsTable();

            tableData.id = int.TryParse(data[i][nameof(tableData.id)].ToString(), out int id) ? id : 0;
            tableData.name = int.TryParse(data[i][nameof(tableData.name)].ToString(), out int name) ? name : 0;
            tableData.desc = int.TryParse(data[i][nameof(tableData.desc)].ToString(), out int desc) ? desc : 0;
            tableData.rank = Enum.TryParse(data[i][nameof(tableData.rank)].ToString(), out Rank rank) ? rank : Rank.None;

            if (!dic.ContainsKey(tableData.id))
                dic.Add(tableData.id, tableData);
        }

        return dic;
    }
}