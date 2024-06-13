using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static Character Instance;

    public float speed;
    public float rotateSpeed;

    public List<Transform> Characters;

    Animator Main;

    KeyCode a, b, c, d;

    float x;
    float z;
    bool back;

    void Awake()
    {
        StartCoroutine(GameManagerWait());
    }

    void Update()
    {
        transform.GetChild(0).localScale = new Vector3(1, 1, back ? -1 : 1);

        transform.Translate(Vector3.forward * GetKeyInput().z * speed * Time.deltaTime);

        transform.eulerAngles += new Vector3(0, back ? -GetKeyInput().x : GetKeyInput().x * rotateSpeed * Time.deltaTime, 0);

        AnimMove(GetKeyInput() != Vector3.zero);
    }

    Vector3 GetKeyInput()
    {
        if (Input.GetKeyDown(a))
        {
            z = 1;
            back = false;
        }

        if (Input.GetKeyDown(b))
        {
            z = -1;
            back = true;
        }

        if ((z > 0 && Input.GetKeyUp(a)) || (z < 0 && Input.GetKeyUp(b))) 
        {
            z = 0;
        }

        if (Input.GetKeyDown(c))
        {
            x = -1;
        }

        if (Input.GetKeyDown(d))
        {
            x = 1;
        }

        if ((x < 0 && Input.GetKeyUp(c)) || (x > 0 && Input.GetKeyUp(d)))
        {
            x = 0;
        }

        return new Vector3(x, 0, z);
    }

    void AnimMove(bool isMove)
    {
        for (int i = 0; i < Characters.Count; i++)
        {
            Characters[i].GetChild(0).GetComponent<Animator>().SetBool("Move", isMove);
        }
    }

    public void switchCharacter(int n = 1)
    {
        Vector3 pos = Characters[0].localPosition;
        Characters[0].localPosition = Characters[n].localPosition;
        Characters[n].localPosition = pos;

        Transform transform = Characters[0];
        Characters[0] = Characters[n];
        Characters[n] = transform;

        Main = Characters[0].GetChild(0).GetComponent<Animator>();
    }

    IEnumerator GameManagerWait()
    {
        Instance = this;
        Main = Characters[0].GetChild(0).GetComponent<Animator>();

        a = KeyCode.UpArrow;
        b = KeyCode.DownArrow;
        c = KeyCode.LeftArrow;
        d = KeyCode.RightArrow;

        float time = 0.0f;
        while (GameManager.Instance == null && time < 10.0f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if (time < 10.0f)
        {
            a = GameManager.Instance.keys[0];
            b = GameManager.Instance.keys[1];
            c = GameManager.Instance.keys[2];
            d = GameManager.Instance.keys[3];
        }
    }
}
