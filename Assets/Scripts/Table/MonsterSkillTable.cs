using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class MonsterSkillTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1b4nRos8ft8KDj_KAaWpuTex4-7rZ5zGE3iC-b73a2QQ";
    public static int sheet = 1226051760;

    public int id;
    public int name;
    public int desc;
    public Attribute_ attribute;
    public SkillType type;
    public float castingTime;
    public float cool;
    public float startCool;
    public int effectRange;
    public int effectArea;
    public Target target;
    public SkillEffect effect1;
    public Status status1;
    public float effectValue1;
    public float effectDuration1;
    public SkillEffect effect2;
    public Status status2;
    public float effectValue2;
    public float effectDuration2;
    public string se;
}

public class MonsterSkillTable_Parser
{
    public Dictionary<int, MonsterSkillTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, MonsterSkillTable> dic = new Dictionary<int, MonsterSkillTable>();

        for (int i = 0; i < data.Count; i++)
        {
            MonsterSkillTable tableData = new MonsterSkillTable();

            tableData.id = int.TryParse(data[i][nameof(tableData.id)].ToString(), out int id) ? id : 0;
            tableData.name = int.TryParse(data[i][nameof(tableData.name)].ToString(), out int name) ? name : 0;
            tableData.desc = int.TryParse(data[i][nameof(tableData.desc)].ToString(), out int desc) ? desc : 0;
            tableData.attribute = int.TryParse(data[i][nameof(tableData.attribute)].ToString(), out int attribute) ? (Attribute_)attribute : Attribute_.None;
            tableData.type = Enum.TryParse(data[i][nameof(tableData.type)].ToString(), out SkillType type) ? type : SkillType.None;
            tableData.castingTime = int.TryParse(data[i][nameof(tableData.castingTime)].ToString(), out int castingTime) ? castingTime : 0;
            tableData.cool = int.TryParse(data[i][nameof(tableData.cool)].ToString(), out int cool) ? cool : 0;
            tableData.startCool = int.TryParse(data[i][nameof(tableData.startCool)].ToString(), out int startCool) ? startCool : 0;
            tableData.effectRange = int.TryParse(data[i][nameof(tableData.effectRange)].ToString(), out int effectRange) ? effectRange : 0;
            tableData.effectArea = int.TryParse(data[i][nameof(tableData.effectArea)].ToString(), out int effectArea) ? effectArea : 0;
            tableData.target = Enum.TryParse(data[i][nameof(tableData.target)].ToString(), out Target target) ? target : Target.None;
            tableData.effect1 = Enum.TryParse(data[i][nameof(tableData.effect1)].ToString(), out SkillEffect effect1) ? effect1 : SkillEffect.None;
            tableData.status1 = Enum.TryParse(data[i][nameof(tableData.status1)].ToString(), out Status status1) ? status1 : Status.None;
            tableData.effectValue1 = int.TryParse(data[i][nameof(tableData.effectValue1)].ToString(), out int effectValue1) ? effectValue1 : 0;
            tableData.effectDuration1 = int.TryParse(data[i][nameof(tableData.effectDuration1)].ToString(), out int effectDuration1) ? effectDuration1 : 0;
            tableData.effect2 = Enum.TryParse(data[i][nameof(tableData.effect2)].ToString(), out SkillEffect effect2) ? effect2 : SkillEffect.None;
            tableData.status2 = Enum.TryParse(data[i][nameof(tableData.status2)].ToString(), out Status status2) ? status2 : Status.None;
            tableData.effectValue2 = float.TryParse(data[i][nameof(tableData.effectValue2)].ToString(), out float effectValue2) ? effectValue2 : 0;
            tableData.effectDuration2 = float.TryParse(data[i][nameof(tableData.effectDuration2)].ToString(), out float effectDuration2) ? effectDuration2 : 0;
            tableData.se = (string)data[i][nameof(tableData.se)];

            if (!dic.ContainsKey(tableData.id))
                dic.Add(tableData.id, tableData);
        }

        return dic;
    }
}
