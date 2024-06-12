using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SkillType
{
    None,
    skill0,
    skill1,
    skill2,
    skill3,
    evasion,
    Ult,
}

public enum SkillEffect
{
    None,
    invincibility,
    attack,
    attackPercent,
    buffAtk,
    buffSpeed,
    buffCrtDmg,
    passiveBuffAtk,
    passiveBuffSpeed,
    passiveBuffCrtDmg,
    debuffDef,
    debuffMovementSpeed,
    stun,
    burn,
    knockBack,
    resurrection,
    restreint,
    shield,
    dispal,
}

public enum Status
{
    None,
    atk,
    def,
    speed1,
}

public class CharacterSkillTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1vkClcU9ztyWF85WvDl03ZYKn55BRDEUydWM1-bXoQ68";
    public static int sheet = 1226051760;

    public int skillset_id;
    public Dictionary<int, SkillSet> skillSet;
}

public class SkillSet
{
    public int name;
    public int desc;
    public Attribute_ attribute;
    public SkillType type;
    public float castingTime;
    public float cool;
    public float startCool;
    public int gage;
    public Range_ effectRange;
    public Range_ effectArea;
    public Target target;
    public SkillEffect effect1;
    public Status status1;
    public float effectValue1;
    public float effectDuration1;
    public SkillEffect effect2;
    public Status status2;
    public float effectValue2;
    public float effectDuration2;
    public string icon;
    public string se;
}

public class CharacterSkillTable_Parser
{
    public Dictionary<int, CharacterSkillTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, CharacterSkillTable> dic = new Dictionary<int, CharacterSkillTable>();

        CharacterSkillTable tableData = new CharacterSkillTable();

        Dictionary<int, SkillSet> groupDic = new Dictionary<int, SkillSet>();
        SkillSet groupData = new SkillSet();

        for (int i = 0; i < data.Count; i++)
        {
            if (data[i][nameof(tableData.skillset_id)].ToString() != string.Empty)
            {
                tableData.skillset_id = int.TryParse(data[i][nameof(tableData.skillset_id)].ToString(), out int skillset_id) ? skillset_id : 0;

                groupDic = new Dictionary<int, SkillSet>();
            }
            else
            {
                groupData = new SkillSet();

                groupData.name = int.TryParse(data[i][nameof(groupData.name)].ToString(), out int name) ? name : 0;
                groupData.desc = int.TryParse(data[i][nameof(groupData.desc)].ToString(), out int desc) ? desc : 0;
                groupData.attribute = int.TryParse(data[i][nameof(groupData.attribute)].ToString(), out int attribute) ? (Attribute_)attribute : (Attribute_)(-1);
                groupData.type = Enum.TryParse(data[i][nameof(groupData.type)].ToString(), out SkillType type) ? type : SkillType.None;
                groupData.castingTime = float.TryParse(data[i][nameof(groupData.castingTime)].ToString(), out float castingTime) ? castingTime : 0;
                groupData.cool = float.TryParse(data[i][nameof(groupData.cool)].ToString(), out float cool) ? cool : 0;
                groupData.startCool = float.TryParse(data[i][nameof(groupData.startCool)].ToString(), out float startCool) ? startCool : 0;
                groupData.gage = int.TryParse(data[i][nameof(groupData.gage)].ToString(), out int gage) ? gage : 0;
                groupData.effectRange = int.TryParse(data[i][nameof(groupData.effectRange)].ToString(), out int effectRange) ? (Range_)effectRange : (Range_)(-1);
                groupData.effectArea = int.TryParse(data[i][nameof(groupData.effectArea)].ToString(), out int effectArea) ? (Range_)effectArea : (Range_)(-1);
                groupData.target = Enum.TryParse(data[i][nameof(groupData.target)].ToString(), out Target target) ? target : Target.None;
                groupData.effect1 = Enum.TryParse(data[i][nameof(groupData.effect1)].ToString(), out SkillEffect effect1) ? effect1 : SkillEffect.None;
                groupData.status1 = Enum.TryParse(data[i][nameof(groupData.status1)].ToString(), out Status status1) ? status1 : Status.None;
                groupData.effectValue1 = float.TryParse(data[i][nameof(groupData.effectValue1)].ToString(), out float effectValue1) ? effectValue1 : 0;
                groupData.effectDuration1 = float.TryParse(data[i][nameof(groupData.effectDuration1)].ToString(), out float effectDuration1) ? effectDuration1 : 0;
                groupData.effect2 = Enum.TryParse(data[i][nameof(groupData.effect2)].ToString(), out SkillEffect effect2) ? effect2 : SkillEffect.None;
                groupData.status2 = Enum.TryParse(data[i][nameof(groupData.status2)].ToString(), out Status status2) ? status2 : Status.None;
                groupData.effectValue2 = float.TryParse(data[i][nameof(groupData.effectValue2)].ToString(), out float effectValue2) ? effectValue2 : 0;
                groupData.effectDuration2 = float.TryParse(data[i][nameof(groupData.effectDuration2)].ToString(), out float effectDuration2) ? effectDuration2 : 0;
                groupData.icon = (string)data[i][nameof(groupData.icon)];
                groupData.se = (string)data[i][nameof(groupData.se)];

            }

            if (groupDic.ContainsKey(groupData.name) == false)
            {
                groupDic.Add(groupData.name, groupData);
            }

            tableData.skillSet = groupDic;

            if (dic.ContainsKey(tableData.skillset_id) == false)
            {
                dic.Add(tableData.skillset_id, tableData);
            }
            else
            {
                dic[tableData.skillset_id].skillSet = groupDic;
            }
        }

        return dic;
    }
}