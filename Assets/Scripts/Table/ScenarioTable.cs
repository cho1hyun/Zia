using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public enum ScenarioCommand
{
    None,
    Start,              //���丮id��
    Text,               //�ؽ�Ʈ
    Tip,                //�� �ؽ�Ʈ
    FadeIn,             //���̵���
    FadeOut,            //���̵�ƿ�
    SendMessage,        //�ý��ۻ��� ������ �ʿ��Ҷ�
    Wait,               //���
    WaitInput,          //�Է� ���
    Bg,                 //���
    BgOff,              //��� ����
    Bgm,                //�����
    StopBgm,            //����� ����
    Se,                 //ȿ����
    StopSe,             //ȿ���� ����
    Sprite,             //�̹��� ����
    SpriteOff,          //�̹��� ����
    Shake,              //ȭ�� ��鸲
    Effect,             //����Ʈ ����
    CharacterLeft,      //���� ĳ���� ����
    CharacterLeftOff,   //���� ĳ���� ����
    CharacterCenter,    //�߾� ĳ���� ����
    CharacterCenterOff, //�߾� ĳ���� ����
    CharacterRight,     //���� ĳ���� ����
    CharacterRightOff,  //���� ĳ���� ����
    Skip,               //���丮 ��ŵ�� �����ϴ� ��ġ
    EndScenario,        //�ó����� ui ����
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
    public Dictionary<string, ScenarioTable> Parse()
    {
        List<Dictionary<string, object>> data = TableManager.Instance.SetData();
        Dictionary<string, ScenarioTable> dic = new Dictionary<string, ScenarioTable>();

        for (var i = 0; i < data.Count; i++)
        {
            ScenarioTable tableData = new ScenarioTable();

            tableData.id = (string)data[i][nameof(tableData.Command)];
            tableData.Command = Enum.TryParse(Regex.Replace(data[i][nameof(tableData.Command)].ToString(), @"\D", ""), out ScenarioCommand type) ? type : ScenarioCommand.None;
            tableData.ScenarioID = int.TryParse(Regex.Replace(data[i][nameof(tableData.Command)].ToString(), @"[^0-9]", ""), out int ScenarioID) ? ScenarioID : 0;
            tableData.Arg1 = (string)data[i][nameof(tableData.Arg1)];
            tableData.Arg2 = (string)data[i][nameof(tableData.Arg2)];
            tableData.Arg3 = (string)data[i][nameof(tableData.Arg3)];
            tableData.Text = int.TryParse(data[i][nameof(tableData.Text)].ToString(), out int Text) ? Text : 0;

            if (!dic.ContainsKey(tableData.id))
                dic.Add(tableData.id, tableData);
        }

        return dic;
    }
}