using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Weapon,
    Memoria,
}

public enum WeaponSubType
{
    None,
    Dragger,
    Sword,
    Spear,
    Gauntlet,
    Gem,
    Bow,
    Gun,
    ThrowingStars,
    Instrument,
    SwordShield,
    Cane,
    Spirit,
    Memoria,
}

public enum optionType
{
    None,
    Atk,
    Def,
    Hp,
    Mp,
    CrR,
    Speed1,
    Speed2,
    TruDmg,
    Heal,
}

public enum target
{
    None,
    m,
    e,
    t,
}

public enum range
{
    None,
    Zero,
    One,
    Two,
    Three,
}

public class EquipTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1hiYAEjQ_zRCI0jAD2v7oqjBMEdfEC81KkVMZawhieXc";
    public static int sheet = 1233384277;

    public int id;
    public int name;
    public int desc;
    public Rank rank;
    public WeaponType type;
    public WeaponSubType subType;
    public target target;
    public optionType optionType1;
    public int optionValue1;
    public int optionValueUp1;
    public optionType optionType2;
    public float optionValue2;
    public float optionValueUp2;
    public float optionPer2;
    public float speed;
    public range range;
    public string model;
    public Vector3 size;
    public Vector3 pos;
}

public class EquipTable_Parser
{
    public Dictionary<int, EquipTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, EquipTable> dic = new Dictionary<int, EquipTable>();

        for (int i = 0; i < data.Count; i++)
        {
            EquipTable tableData = new EquipTable();

            tableData.id = int.TryParse(data[i][nameof(tableData.id)].ToString(), out int id) ? id : 0;
            tableData.name = int.TryParse(data[i][nameof(tableData.name)].ToString(), out int name) ? name : 0;
            tableData.desc = int.TryParse(data[i][nameof(tableData.desc)].ToString(), out int desc) ? desc : 0;
            tableData.rank = Enum.TryParse(data[i][nameof(tableData.rank)].ToString(), out Rank rank) ? rank : Rank.None;
            tableData.type = Enum.TryParse(data[i][nameof(tableData.type)].ToString(), out WeaponType type) ? type : WeaponType.None;
            tableData.subType = Enum.TryParse(data[i][nameof(tableData.subType)].ToString(), out WeaponSubType subType) ? subType : WeaponSubType.None;
            tableData.target = Enum.TryParse(data[i][nameof(tableData.target)].ToString(), out target target) ? target : target.None;
            tableData.optionType1 = Enum.TryParse(data[i][nameof(tableData.optionType1)].ToString(), out optionType optionType1) ? optionType1 : optionType.None;
            tableData.optionValue1 = int.TryParse(data[i][nameof(tableData.optionValue1)].ToString(), out int optionValue1) ? optionValue1 : 0;
            tableData.optionValueUp1 = int.TryParse(data[i][nameof(tableData.optionValueUp1)].ToString(), out int optionValueUp1) ? optionValueUp1 : 0;
            tableData.optionType2 = Enum.TryParse(data[i][nameof(tableData.optionType2)].ToString(), out optionType optionType2) ? optionType2 : optionType.None;
            tableData.optionValue2 = float.TryParse(data[i][nameof(tableData.optionValue2)].ToString(), out float optionValue2) ? optionValue2 : 0;
            tableData.optionValueUp2 = float.TryParse(data[i][nameof(tableData.optionValueUp2)].ToString(), out float optionValueUp2) ? optionValueUp2 : 0;
            tableData.optionPer2 = float.TryParse(data[i][nameof(tableData.optionPer2)].ToString(), out float optionPer2) ? optionPer2 : 0;
            tableData.speed = float.TryParse(data[i][nameof(tableData.speed)].ToString(), out float speed) ? speed : 0;
            tableData.range = Enum.TryParse(Range(data[i][nameof(tableData.range)].ToString()), out range range) ? range : range.None;
            tableData.model = (string)data[i][nameof(tableData.model)];
            tableData.size = new Vector3(float.TryParse(data[i]["sizeX"].ToString(), out float sizeX) ? sizeX : 0, float.TryParse(data[i]["sizeY"].ToString(), out float sizeY) ? sizeY : 0, float.TryParse(data[i]["sizeZ"].ToString(), out float sizeZ) ? sizeZ : 0);
            tableData.pos = new Vector3(float.TryParse(data[i]["posX"].ToString(), out float posX) ? posX : 0, float.TryParse(data[i]["posY"].ToString(), out float posY) ? posY : 0, float.TryParse(data[i]["posZ"].ToString(), out float posZ) ? posZ : 0);

            if (!dic.ContainsKey(tableData.id))
                dic.Add(tableData.id, tableData);
        }

        return dic;
    }

    string Range(string option)
    {
        switch (option)
        {
            case "0":
                return "Zero";
            case "1":
                return "One";
            case "2":
                return "Two";
            case "3":
                return "Three";
            default:
                return "None";
        }
    }
}
