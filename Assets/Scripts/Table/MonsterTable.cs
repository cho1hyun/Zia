using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    None = -1,
    Zero = 0,
    One = 1,
    Two = 2,
}

public class MonsterTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1b4nRos8ft8KDj_KAaWpuTex4-7rZ5zGE3iC-b73a2QQ";
    public static int sheet = 0;

    public int id;
    public int name;
    public int desc;
    public MonsterType type;
    public int lvl;
    public Attribute_ attribute;
    public int hp;
    public int atk;
    public int def;
    public float speed1;
    public float speed2;
    public int Skill1;
    public int Skill2;
    public int Skill3;
    public string model;
    public Vector3 size;
}

public class MonsterTable_Parser
{
    public Dictionary<int, MonsterTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, MonsterTable> dic = new Dictionary<int, MonsterTable>();

        for (int i = 0; i < data.Count; i++)
        {
            MonsterTable tableData = new MonsterTable();

            tableData.id = int.TryParse(data[i][nameof(tableData.id)].ToString(), out int id) ? id : 0;
            tableData.name = int.TryParse(data[i][nameof(tableData.name)].ToString(), out int name) ? name : 0;
            tableData.desc = int.TryParse(data[i][nameof(tableData.desc)].ToString(), out int desc) ? desc : 0;
            tableData.type = Enum.TryParse(data[i][nameof(tableData.type)].ToString(), out MonsterType type) ? type : MonsterType.None;
            tableData.attribute = int.TryParse(data[i][nameof(tableData.attribute)].ToString(), out int attribute) ? (Attribute_)attribute : (Attribute_)(-1);
            tableData.hp = int.TryParse(data[i][nameof(tableData.hp)].ToString(), out int hp) ? hp : 0;
            tableData.atk = int.TryParse(data[i][nameof(tableData.atk)].ToString(), out int atk) ? atk : 0;
            tableData.def = int.TryParse(data[i][nameof(tableData.def)].ToString(), out int def) ? def : 0;
            tableData.speed1 = float.TryParse(data[i][nameof(tableData.speed1)].ToString(), out float speed1) ? speed1 : 0;
            tableData.speed2 = float.TryParse(data[i][nameof(tableData.speed2)].ToString(), out float speed2) ? speed2 : 0;
            tableData.Skill1 = int.TryParse(data[i][nameof(tableData.Skill1)].ToString(), out int Skill1) ? Skill1 : 0;
            tableData.Skill2 = int.TryParse(data[i][nameof(tableData.Skill2)].ToString(), out int Skill2) ? Skill2 : 0;
            tableData.Skill3 = int.TryParse(data[i][nameof(tableData.Skill3)].ToString(), out int Skill3) ? Skill3 : 0;
            tableData.model = (string)data[i][nameof(tableData.model)];
            tableData.size = new Vector3(float.TryParse(data[i]["sizeX"].ToString(), out float sizeX) ? sizeX : 0, float.TryParse(data[i]["sizeY"].ToString(), out float sizeY) ? sizeY : 0, float.TryParse(data[i]["sizeZ"].ToString(), out float sizeZ) ? sizeZ : 0);

            if (!dic.ContainsKey(tableData.id))
                dic.Add(tableData.id, tableData);
        }

        return dic;
    }