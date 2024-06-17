using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginUI : MonoBehaviour
{
    bool login = false;

    int i;

    public List<TMP_InputField> Inputs;

    public GameObject error;

    void OnEnable()
    {
        if (Inputs.Count != 0)
        {
            Inputs[0].Select();

            for (int i = 0; i < Inputs.Count; i++)
            {
                Inputs[i].onSubmit.AddListener(delegate { InputEnter(); });
            }
        }
    }

    void Update()
    {
        if (Inputs.Count != 0 && Input.GetKeyDown(KeyCode.Tab))
        {
            i++;
            i = i % 2;
            Inputs[i].Select();
        }

        if (Inputs.Count != 0 && Input.GetKeyDown(KeyCode.Escape))
            Login();
    }

    void InputEnter()
    {
        if (error != null)
        {
            error.SetActive(false);

            for (int i = 0; i < Inputs.Count; i++)
            {
                if (Inputs[i].text.Length == 0)
                {
                    error.SetActive(true);
                    return;
                }
            }

            Login(true);
        }
    }

    public void Exit()
    {
        gameObject.SetActive(false);

        if(login)
            UiManager.Instance.logo.LoginAction();
    }

    public void Login(bool _login = false)
    {
        if (gameObject.activeSelf)
            GetComponent<Animator>().SetTrigger("Exit");

        login = _login;
    }


}
