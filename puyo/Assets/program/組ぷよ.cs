using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 組ぷよ : MonoBehaviour
{
    float span = 1.0f;
    float delta = 0.0f;
    float key= 0.0f;
    GameObject[] puyos;
    float[] puyox = new float[100];
    float[] puyoy = new float[100];
    // Start is called before the first frame update
    void Start()
    {
        this.puyos = GameObject.FindGameObjectsWithTag("puyo");
        int i = 0;
        foreach (GameObject puyo in this.puyos)
        {
            //丸め誤差解消
            this.puyox[i] = Mathf.RoundToInt(puyo.transform.position.x * 10.0f) / 10.0f;
            this.puyoy[i] = Mathf.RoundToInt(puyo.transform.position.y * 10.0f) / 10.0f;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float nowx = Mathf.RoundToInt(transform.position.x * 10.0f) / 10.0f;
        float nowy = Mathf.RoundToInt(transform.position.y * 10.0f) / 10.0f;
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0.0f;
            transform.Translate(0, 0, 0, Space.World); //ここを(0, -0.1f, 0, Space.World)にすると自動落下
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            this.gameObject.transform.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.gameObject.transform.position += new Vector3(1, 0, 0);
        }
        transform.Translate(key, 0, 0, Space.World);

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.Translate(0, -1.0f, 0, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.gameObject.transform.RotateAround(transform.position, new Vector3(0, 0, 1), -90);
        } 

            
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                transform.Rotate(0, 0, 90.0f);
        }
        
        if (nowy == -5f)
        {
            gameObject.transform.DetachChildren();
            Destroy(gameObject);
        }
        var i = 0;
        foreach (GameObject puyo in this.puyos)
        {
            //落下終了条件（nowx、nowyは前回定義済）
            if (nowx + 0.5 == this.puyox[i] && nowy - 0.5 == this.puyoy[i] + 1.0f)
            {
                gameObject.transform.DetachChildren();    //親子関係の解除
                Destroy(gameObject);     //ぷよセットオブジェクト（親）を削除
                return;
            }
            i++;
        }

    }
    
}
