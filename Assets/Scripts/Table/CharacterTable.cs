using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Gender
{
    None,
    man,
    woman,
}

public enum Class_
{
    None = -1,
    Zero = 0,
    One = 1,
    Two = 2,
}

public enum Attribute_
{
    None = 0,
    Mare = 1,
    Flamma = 2,
    Vita = 3,
    Lucere = 4,
    Umbra = 5,
}


public class CharacterTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1vkClcU9ztyWF85WvDl03ZYKn55BRDEUydWM1-bXoQ68";
    public static int sheet = 0;

    public int id;
    public int name;
    public int desc;
    public Gender gender;
    public Rank rank;
    public Class_ class_;
    public WeaponSubType weapon;
    public Attribute_ attribute;
    public int hp;
    public int atk;
    public int def;
    public float speed1;
    public float speed2;
    public int skillset;
    public string model;
    public Vector3 size;
}

public class CharacterTable_Parser
{
    public Dictionary<int, CharacterTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, CharacterTable> dic = new Dictionary<int, CharacterTable>();

        for (int i = 0; i < data.Count; i++)
        {
            CharacterTable tableData = new CharacterTable();

            tableData.id = int.TryParse(data[i][nameof(tableData.id)].ToString(), out int id) ? id : 0;
            tableData.name = int.TryParse(data[i][nameof(tableData.name)].ToString(), out int name) ? name : 0;
            tableData.desc = int.TryParse(data[i][nameof(tableData.desc)].ToString(), out int desc) ? desc : 0;
            tableData.gender = Enum.TryParse(data[i][nameof(tableData.gender)].ToString(), out Gender gender) ? gender : Gender.None;
            tableData.rank = Enum.TryParse(data[i][nameof(tableData.rank)].ToString(), out Rank rank) ? rank : Rank.None;
            tableData.class_ = Enum.TryParse(data[i][nameof(tableData.class_)].ToString(), out Class_ class_) ? class_ : Class_.None;
            tableData.weapon = Enum.TryParse(data[i][nameof(tableData.weapon)].ToString(), out WeaponSubType weapon) ? weapon : WeaponSubType.None;
            tableData.attribute = int.TryParse(data[i][nameof(tableData.attribute)].ToString(), out int attribute) ? (Attribute_)attribute : (Attribute_)(-1);
            tableData.hp = int.TryParse(data[i][nameof(tableData.hp)].ToString(), out int hp) ? hp : 0;
            tableData.atk = int.TryParse(data[i][nameof(tableData.atk)].ToString(), out int atk) ? atk : 0;
            tableData.def = int.TryParse(data[i][nameof(tableData.def)].ToString(), out int def) ? def : 0;
            tableData.speed1 = float.TryParse(data[i][nameof(tableData.speed1)].ToString(), out float speed1) ? speed1 : 0;
            tableData.speed2 = float.TryParse(data[i][nameof(tableData.speed2)].ToString(), out float speed2) ? speed2 : 0;
            tableData.skillset = int.TryParse(data[i][nameof(tableData.skillset)].ToString(), out int skillset) ? skillset : 0;
            tableData.model = (string)data[i][nameof(tableData.model)];
            tableData.size = new Vector3(float.TryParse(data[i]["sizeX"].ToString(), out float sizeX) ? sizeX : 0, float.TryParse(data[i]["sizeY"].ToString(), out float sizeY) ? sizeY : 0, float.TryParse(data[i]["sizeZ"].ToString(), out float sizeZ) ? sizeZ : 0);

            if (!dic.ContainsKey(tableData.id))
                dic.Add(tableData.id, tableData);
        }

        return dic;
    }
}