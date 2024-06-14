using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    None,
    tutorial,
    scenario,
    monster,
    item,
    gacha,
    enforce,
}

public enum rewardType
{
    None,
    gold,
    exp,
}

public class QuestTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1M-vfQzjD5Od41Cp_fcPVKp6538F-pHs8DqJG-zXnHRQ";
    public static int sheet = 2009638316;

    public int id;
    public int name;
    public int des;
    public QuestType type;
    public OpenCondition OpenCondition;
    public int openValue;
    public int accomplishValue;
    public rewardType rewardType1;
    public int rewardValue1;
    public rewardType rewardType2;
    public int rewardValue2;
}

public class QuestTable_Parser
{
    public Dictionary<int, QuestTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, QuestTable> dic = new Dictionary<int, QuestTable>();

        for (int i = 0; i < data.Count; i++)
        {
            QuestTable tableData = new QuestTable();

            tableData.id = int.TryParse(data[i][nameof(tableData.id)].ToString(), out int id) ? id : 0;
            tableData.name = int.TryParse(data[i][nameof(tableData.name)].ToString(), out int name) ? name : 0;
            tableData.des = int.TryParse(data[i][nameof(tableData.des)].ToString(), out int des) ? des : 0;
            tableData.type = Enum.TryParse(data[i][nameof(tableData.type)].ToString(), out QuestType type) ? type : QuestType.None;
            tableData.OpenCondition = Enum.TryParse(data[i][nameof(tableData.OpenCondition)].ToString(), out OpenCondition OpenCondition) ? OpenCondition : OpenCondition.None;
            tableData.openValue = int.TryParse(data[i][nameof(tableData.openValue)].ToString(), out int openValue) ? openValue : 0;
            tableData.accomplishValue = int.TryParse(data[i][nameof(tableData.accomplishValue)].ToString(), out int accomplishValue) ? accomplishValue : 0;
            tableData.rewardType1 = Enum.TryParse(data[i][nameof(tableData.rewardType1)].ToString(), out rewardType rewardType1) ? rewardType1 : rewardType.None;
            tableData.rewardValue1 = int.TryParse(data[i][nameof(tableData.rewardValue1)].ToString(), out int rewardValue1) ? rewardValue1 : 0;
            tableData.rewardType2 = Enum.TryParse(data[i][nameof(tableData.rewardType2)].ToString(), out rewardType rewardType2) ? rewardType2 : rewardType.None;
            tableData.rewardValue2 = int.TryParse(data[i][nameof(tableData.rewardValue2)].ToString(), out int rewardValue2) ? rewardValue2 : 0;

            if (!dic.ContainsKey(tableData.id))
                dic.Add(tableData.id, tableData);
        }

        return dic;
    }
}