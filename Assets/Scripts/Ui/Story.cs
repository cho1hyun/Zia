using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    public Image BackGround;
    public TMP_Text TextField;
    public TMP_Text NameField;
    public Transform AutoOnOff;
    public GameObject SkipObj;

    public List<GameObject> TextOver;
    public List<GameObject> Allui;

    bool skip;
    bool auto;
    bool next;

    Coroutine scenarioCorutine;
    List<ScenarioTable> skipScenario;

    public void SetScenario(int scrnario)
    {
        GetComponent<CanvasGroup>().alpha = 0;

        gameObject.SetActive(true);

        Auto();

        List<ScenarioTable> scenarios = TableManager.Instance.GetScenarioList(scrnario);
        skipScenario = new List<ScenarioTable>();
        for (int i = 0; i < scenarios.Count; i++)
        {
            if (scenarios[i].Command == ScenarioCommand.Skip)
            {
                skip = true;
            }
            if (skip)
            {
                skipScenario.Add(scenarios[i]);
            }
        }

        scenarioCorutine = StartCoroutine(SetScenarios(scenarios));
    }

    public void SkipBtn()
    {
        if (Allui[0].activeSelf)
        {
            SkipObj.SetActive(!SkipObj.activeSelf);
        }
        else
        {
            for (int i = 0; i < Allui.Count; i++)
            {
                Allui[i].SetActive(true);
            }
        }
    }

    public void Skip()
    {
        SkipBtn();

        StopCoroutine(scenarioCorutine);

        for (int i = 0; i < TextOver.Count; i++)
        {
            TextOver[i].SetActive(false);
        }

        StartCoroutine(SetScenarios(skipScenario));
    }

    public void AutoBtn()
    {
        auto = !auto;
        Auto();
    }

    void Auto()
    {
        AutoOnOff.GetChild(1).gameObject.SetActive(auto);
        AutoOnOff.GetChild(2).gameObject.SetActive(!auto);
    }

    public void View()
    {
        TextOver[0].SetActive(true);

        for (int i = 0; i < Allui.Count; i++)
        {
            Allui[i].SetActive(false);
        }
    }

    public void Onclick()
    {
        if (Allui[0].activeSelf)
        {
            next = true;

            for (int i = 0; i < TextOver.Count; i++)
            {
                TextOver[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < Allui.Count; i++)
            {
                Allui[i].SetActive(true);
            }
        }
    }

    IEnumerator SetScenarios(List<ScenarioTable> scenarios)
    {
        SpriteAtlas spriteAtlasS = Resources.Load<SpriteAtlas>("Atlas/Scenario");

        for (int i = 0; i < scenarios.Count; i++)
        {
            next = false;

            switch (scenarios[i].Command)
            {
                case ScenarioCommand.None:
                    break;
                case ScenarioCommand.Start:
                    break;
                case ScenarioCommand.Text:
                    TextField.text = string.Empty;
                    string text = TableManager.Instance.GetScenarioText(scenarios[i].Text);
                    NameField.text = TableManager.Instance.GetScenarioText(int.Parse(scenarios[i].Arg1));
                    for (int j = 0; j < text.Length; j++)
                    {
                        TextField.text += text[j];
                        yield return null;
                    }
                    for (int j = 0; j < TextOver.Count; j++)
                    {
                        TextOver[i].SetActive(true);
                    }
                    while (!auto && !next)
                    {
                        yield return null;
                    }
                    break;
                case ScenarioCommand.Tip:
                    break;
                case ScenarioCommand.FadeIn:
                    UiManager.Instance.Action(0);
                    yield return new WaitForSeconds(float.Parse(scenarios[i].Arg1));
                    break;
                case ScenarioCommand.FadeOut:
                    GetComponent<CanvasGroup>().alpha = 1;
                    UiManager.Instance.pade.PadeOff();
                    yield return new WaitForSeconds(float.Parse(scenarios[i].Arg1));
                    break;
                case ScenarioCommand.SendMessage:
                    break;
                case ScenarioCommand.Wait:
                    break;
                case ScenarioCommand.WaitInput:
                    break;
                case ScenarioCommand.Bg:
                    BackGround.gameObject.SetActive(true);
                    BackGround.sprite = spriteAtlasS.GetSprite(scenarios[i].Arg1);
                    break;
                case ScenarioCommand.BgOff:
                    BackGround.gameObject.SetActive(false);
                    break;
                case ScenarioCommand.Bgm:
                    //
                    break;
                case ScenarioCommand.StopBgm:
                    //
                    break;
                case ScenarioCommand.Se:
                    //
                    break;
                case ScenarioCommand.StopSe:
                    //
                    break;
                case ScenarioCommand.Sprite:
                    break;
                case ScenarioCommand.SpriteOff:
                    break;
                case ScenarioCommand.Shake:
                    BackGround.GetComponent<Animator>().SetBool("Shake", true);
                    yield return new WaitForSeconds(float.Parse(scenarios[i].Arg1));
                    BackGround.GetComponent<Animator>().SetBool("Shake", false);
                    break;
                case ScenarioCommand.Effect:
                    break;
                case ScenarioCommand.CharacterLeft:
                    break;
                case ScenarioCommand.CharacterLeftOff:
                    break;
                case ScenarioCommand.CharacterCenter:
                    break;
                case ScenarioCommand.CharacterCenterOff:
                    break;
                case ScenarioCommand.CharacterRight:
                    break;
                case ScenarioCommand.CharacterRightOff:
                    break;
                case ScenarioCommand.Skip:
                    break;
                case ScenarioCommand.EndScenario:
                    UiManager.Instance.Action(4);
                    break;
                default:
                    break;
            }

            yield return null;
        }
    }
}