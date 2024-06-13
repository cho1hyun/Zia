using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    public float rotateSpeed;

    KeyCode a, b, c, d;

    float x;
    float z;
    bool back;

    void Update()
    {
        transform.GetChild(0).localScale = new Vector3(1, 1, back ? -1 : 1);

        transform.Translate(Vector3.forward * GetKeyInput().z * speed * Time.deltaTime);

        transform.eulerAngles += new Vector3(0, back ? -GetKeyInput().x : GetKeyInput().x * rotateSpeed * Time.deltaTime, 0);

        AnimMove(GetKeyInput() != Vector3.zero);
    }

    Vector3 GetKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            x = 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            x = -1;
        }

        if ((x > 0 && Input.GetKeyUp(KeyCode.RightArrow)) || x < 0 && Input.GetKeyUp(KeyCode.LeftArrow))
        {
            x = 0;
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            z = 1;
            back = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            z = -1;
            back = true;
        }

        if ((z > 0 && Input.GetKeyUp(KeyCode.UpArrow)) || z < 0 && Input.GetKeyUp(KeyCode.DownArrow))
        {
            z = 0;
        }

        return new Vector3(x, 0, z);
    }

    void AnimMove(bool isMove)
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<Animator>().SetBool("Move", isMove);
        }
    }
}
