using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public enum OpenCondition
{
    None,
    preStage,
    clearStage,
}

public class StageDungeonTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1H3uOW2WOdfTCiGOYcon9ODX6WgQW8NPPLffapOq5kqM";
    public static int sheet = 1413209913;

    public int id;
    public int DgName;
    public int DgChapter;
    public int DgStage;
    public int DgType;
    public int DgWth;
    public int RecommendLvFrom;
    public int RecommendLvUntil;
    public int ReqLv;
    public OpenCondition OpenCondition;
    public string DgSound;
    public int Episode;
    public int RewardGold;
    public int RewardExp;
    public int SpawnOrder1_GroupID;
}

public class StageDungeonTable_Parser
{
    public Dictionary<int, StageDungeonTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, StageDungeonTable> dic = new Dictionary<int, StageDungeonTable>();

        for (int i = 0; i < data.Count; i++)
        {
            StageDungeonTable tableData = new StageDungeonTable();

            tableData.id = int.TryParse(data[i][nameof(tableData.id)].ToString(), out int id) ? id : 0;
            tableData.DgName = int.TryParse(data[i][nameof(tableData.DgName)].ToString(), out int DgName) ? DgName : 0;
            tableData.DgChapter = int.TryParse(data[i][nameof(tableData.DgChapter)].ToString(), out int DgChapter) ? DgChapter : 0;
            tableData.DgStage = int.TryParse(data[i][nameof(tableData.DgStage)].ToString(), out int DgStage) ? DgStage : 0;
            tableData.DgType = int.TryParse(data[i][nameof(tableData.DgType)].ToString(), out int DgType) ? DgType : 0;
            tableData.DgWth = int.TryParse(data[i][nameof(tableData.DgWth)].ToString(), out int DgWth) ? DgWth : 0;
            tableData.RecommendLvFrom = int.TryParse(data[i][nameof(tableData.RecommendLvFrom)].ToString(), out int RecommendLvFrom) ? RecommendLvFrom : 0;
            tableData.RecommendLvUntil = int.TryParse(data[i][nameof(tableData.RecommendLvUntil)].ToString(), out int RecommendLvUntil) ? RecommendLvUntil : 0;
            tableData.ReqLv = int.TryParse(data[i][nameof(tableData.ReqLv)].ToString(), out int ReqLv) ? ReqLv : 0;
            tableData.OpenCondition = Enum.TryParse(data[i][nameof(tableData.OpenCondition)].ToString(), out OpenCondition OpenCondition) ? OpenCondition : OpenCondition.None;
            tableData.DgSound = (string)data[i][nameof(tableData.DgSound)];
            tableData.Episode = int.TryParse(data[i][nameof(tableData.Episode)].ToString(), out int Episode) ? Episode : 0;
            tableData.RewardGold = int.TryParse(data[i][nameof(tableData.RewardGold)].ToString(), out int RewardGold) ? RewardGold : 0;
            tableData.RewardExp = int.TryParse(data[i][nameof(tableData.RewardExp)].ToString(), out int RewardExp) ? RewardExp : 0;
            tableData.SpawnOrder1_GroupID = int.TryParse(data[i][nameof(tableData.SpawnOrder1_GroupID)].ToString(), out int SpawnOrder1_GroupID) ? SpawnOrder1_GroupID : 0;

            if (!dic.ContainsKey(tableData.id))
                dic.Add(tableData.id, tableData);
        }

        return dic;
    }
}