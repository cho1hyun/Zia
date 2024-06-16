using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public enum ScenarioCommand
{
    None,
    Start,              //스토리id값
    Text,               //텍스트
    Tip,                //팁 텍스트
    FadeIn,             //페이드인
    FadeOut,            //페이드아웃
    SendMessage,        //시스템상의 조작이 필요할때
    Wait,               //대기
    WaitInput,          //입력 대기
    Bg,                 //배경
    BgOff,              //배경 끄기
    Bgm,                //배경음
    StopBgm,            //배경음 끄기
    Se,                 //효과음
    StopSe,             //효과음 끄기
    Sprite,             //이미지 띄우기
    SpriteOff,          //이미지 끄기
    Shake,              //화면 흔들림
    Effect,             //이펙트 연출
    CharacterLeft,      //좌측 캐릭터 변경
    CharacterLeftOff,   //좌측 캐릭터 끄기
    CharacterCenter,    //중앙 캐릭터 변경
    CharacterCenterOff, //중앙 캐릭터 끄기
    CharacterRight,     //우측 캐릭터 변경
    CharacterRightOff,  //우측 캐릭터 끄기
    Skip,               //스토리 스킵시 도달하는 위치
    EndScenario,        //시나리오 ui 종료
}

public class ScenarioTable
{
    public static string address = "https://docs.google.com/spreadsheets/d/1siOEyNftKKUtOfpN6gBoQppihAHFVm0PgnYhkH20ld4";
    public static int sheet = 0;

    public string id;
    public ScenarioCommand Command;
    public int ScenarioID;
    public string Arg1;
    public string Arg2;
    public string Arg3;
    public int Text;
}

public class ScenarioTable_Parser
{
    public Dictionary<int, ScenarioTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<int, ScenarioTable> dic = new Dictionary<int, ScenarioTable>();

        for (var i = 0; i < data.Count; i++)
        {
            ScenarioTable tableData = new ScenarioTable();

            tableData.id = (string)data[i][nameof(tableData.Command)];
            tableData.Command = Enum.TryParse(Regex.Replace(data[i][nameof(tableData.Command)].ToString(), @"\D", ""), out ScenarioCommand type) ? type : ScenarioCommand.None;
            if (tableData.Command == ScenarioCommand.None)
            {
                tableData.Command = Enum.TryParse(tableData.id, out ScenarioCommand type_) ? type_ : ScenarioCommand.None;
            }
            tableData.ScenarioID = int.TryParse(Regex.Replace(data[i][nameof(tableData.Command)].ToString(), @"[^0-9]", ""), out int ScenarioID) ? ScenarioID : 0;
            tableData.Arg1 = (string)data[i][nameof(tableData.Arg1)];
            tableData.Arg2 = (string)data[i][nameof(tableData.Arg2)];
            tableData.Arg3 = (string)data[i][nameof(tableData.Arg3)];
            tableData.Text = int.TryParse(data[i][nameof(tableData.Text)].ToString(), out int Text) ? Text : 0;

            Debug.Log(tableData.id + "," + tableData.Command.ToString() + "," + tableData.ScenarioID + "," + tableData.Arg1 + "," + tableData.Arg2 + "," + tableData.Arg3 + "," + tableData.Text);

            if (!dic.ContainsKey(i))
                dic.Add(i, tableData);
        }

        return dic;
    }
}