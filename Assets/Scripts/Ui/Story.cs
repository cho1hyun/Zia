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

    public GameObject TextOver;
    public List<GameObject> Allui;

    bool auto;
    bool texting;

    Coroutine scenarioCorutine;
    List<ScenarioTable> scenarios;

    int skipCount;
    List<int> now;

    string txt;

    public void SetScenario(int scrnario)
    {
        auto = false;
        now = new List<int>();

        GetComponent<CanvasGroup>().alpha = 1;

        gameObject.SetActive(true);

        scenarios = TableManager.Instance.GetScenarioList(scrnario);

        for (int i = 0; i < scenarios.Count; i++)
        {
            if (scenarios[i].Command == ScenarioCommand.Text)
            {
                now.Add(i);
            }

            if (scenarios[i].Command == ScenarioCommand.Skip)
            {
                skipCount = i;
            }
        }

        AutoOnOff.GetChild(1).gameObject.SetActive(auto);
        AutoOnOff.GetChild(2).gameObject.SetActive(!auto);

        SetScenarios();
    }
    IEnumerator ShowText()
    {
        TextField.text = string.Empty;
        for (int i = 0; i < txt.Length; i++)
        {
            TextField.text += txt[i];
            yield return new WaitForSeconds(0.05f);
        }

        if (!auto)
        {
            texting = false;
            TextOver.SetActive(true);
        }
        else
        {
            now[0]++;
            SetScenarios();
        }
        scenarioCorutine = null;
    }

    IEnumerator Wait(float t)
    {
        float time = 0.0f;
        while (time < t)
        {
            time += Time.deltaTime;
            yield return null;
        }
        now[0]++;
        SetScenarios();

        scenarioCorutine = null;
    }

    IEnumerator Shake(float t)
    {
        float time = 0.0f;
        while (time < t)
        {
            time += Time.deltaTime;
            yield return null;
        }

        BackGround.GetComponent<Animator>().SetBool("Shake", false);

        now[0]++;
        SetScenarios();

        scenarioCorutine = null;
    }

    void FillText()
    {
        if (scenarioCorutine != null)
            StopCoroutine(scenarioCorutine);

        TextField.text = txt;
        texting = false;
        TextOver.SetActive(true);
    }


    void SetScenarios()
    {
        SpriteAtlas spriteAtlasS = Resources.Load<SpriteAtlas>("Atlas/Scenario");

        switch (scenarios[now[0]].Command)
        {
            case ScenarioCommand.None:
                break;
            case ScenarioCommand.Start:
                break;
            case ScenarioCommand.Text:
                texting = true;
                txt = TableManager.Instance.GetScenarioText(scenarios[now[0]].Text);
                Debug.Log(txt);
                scenarioCorutine = StartCoroutine(ShowText());
                if (scenarios[now[0]].Arg1 != string.Empty)
                    NameField.text = TableManager.Instance.GetScenarioText(int.Parse(scenarios[now[0]].Arg1));
                break;
            case ScenarioCommand.Tip:
                break;
            case ScenarioCommand.FadeIn:
                UiManager.Instance.Action(0);
                scenarioCorutine = StartCoroutine(Wait(float.Parse(scenarios[now[0]].Arg1)));
                break;
            case ScenarioCommand.FadeOut:
                GetComponent<CanvasGroup>().alpha = 1;
                UiManager.Instance.pade.PadeOff();
                scenarioCorutine = StartCoroutine(Wait(float.Parse(scenarios[now[0]].Arg1)));
                break;
            case ScenarioCommand.SendMessage:
                break;
            case ScenarioCommand.Wait:
                scenarioCorutine = StartCoroutine(Wait(float.Parse(scenarios[now[0]].Arg1)));
                break;
            case ScenarioCommand.WaitInput:
                //
                break;
            case ScenarioCommand.Bg:
                BackGround.gameObject.SetActive(true);
                BackGround.sprite = spriteAtlasS.GetSprite(scenarios[now[0]].Arg1);
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
                scenarioCorutine = StartCoroutine(Shake(float.Parse(scenarios[now[0]].Arg1)));
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
                UiManager.Instance.Action(0);
                break;
            default:
                break;
        }
    }

    public void View()
    {
        for (int i = 0; i < Allui.Count; i++)
        {
            Allui[i].SetActive(false);
        }
    }

    public void Auto()
    {
        auto = !auto;
        AutoOnOff.GetChild(1).gameObject.SetActive(auto);
        AutoOnOff.GetChild(2).gameObject.SetActive(!auto);
    }

    public void Skip()
    {
        if (scenarioCorutine != null)
            StopCoroutine(scenarioCorutine);

        now[0] = skipCount;
        SetScenarios();
    }

    public void Onclick()
    {
        if (texting && !auto)
        {
            FillText();
        }

        if (TextOver.activeSelf)
        {
            TextOver.SetActive(false);
            now[0]++;
            SetScenarios();
        }

        if (Allui[0].activeSelf)
        {
            for (int i = 0; i < Allui.Count; i++)
            {
                Allui[i].SetActive(true);
            }
        }
    }
}