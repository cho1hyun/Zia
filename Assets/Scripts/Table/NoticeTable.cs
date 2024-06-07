using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoticeType
{
    None,
    Link,
    Shop,
}

public class NoticeTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1VgdmSFiG4mLbmuxQIWtF3mq6p7b8bP1_u_xOw2N3-iw";
    public static int sheet = 0;

    public int id;
    public int name;
    public int desc;
    public string img;
    public NoticeType type;
    public string link;
}

public class NoticeTable_Parser
{
    public Dictionary<int, NoticeTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, NoticeTable> dic = new Dictionary<int, NoticeTable>();

        for (var i = 0; i < data.Count; i++)
        {
            NoticeTable tableData = new NoticeTable();

            tableData.id = int.TryParse(data[i][nameof(tableData.id)].ToString(), out int id) ? id : 0;
            tableData.name = int.TryParse(data[i][nameof(tableData.name)].ToString(), out int name) ? name : 0;
            tableData.desc = int.TryParse(data[i][nameof(tableData.desc)].ToString(), out int desc) ? desc : 0;
            tableData.img = (string)data[i][nameof(tableData.img)];
            //tableData.type = Enum.TryParse(enumType: typeof NoticeType,);
            tableData.link = (string)data[i][nameof(tableData.link)];

            if (!dic.ContainsKey(tableData.id))
                dic.Add(tableData.id, tableData);
        }

        return dic;
    }
}
