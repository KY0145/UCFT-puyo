using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanpuyo : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] puyos;
    GameObject Director;
    int num = 0; //num=0で落下、終了時にfallcheckへ、num=1なら休止
    float[] puyox = new float[100];
    float[] puyoy = new float[100];

    //ぷよ消去後に一斉落下するため、全ぷよのthis.num=0（監督スクリプトより呼び出し）
    public void numreset()
    {
        int i = 0;
        this.num = 0; //落下を再開(Updateメソッドに従う)
                      //注意:現時点でのフィールド中のぷよ位置をここで再調査
        this.puyos = GameObject.FindGameObjectsWithTag("puyo");
        foreach (GameObject puyo in this.puyos)
        {
            //丸め誤差解消(フィールド中の全ぷよの位置)
            this.puyox[i] = Mathf.RoundToInt(puyo.transform.position.x * 10.0f) / 10.0f;
            this.puyoy[i] = Mathf.RoundToInt(puyo.transform.position.y * 10.0f) / 10.0f;
            i++;
        }
    }
    void Start()
    {
        int i = 0;
        this.Director = GameObject.Find("Director");
        this.puyos = GameObject.FindGameObjectsWithTag("puyo");
        foreach (GameObject puyo in this.puyos)
        {
            //丸め誤差解消(フィールド中の全ぷよの位置)
            this.puyox[i] = Mathf.RoundToInt(puyo.transform.position.x * 10.0f) / 10.0f;
            this.puyoy[i] = Mathf.RoundToInt(puyo.transform.position.y * 10.0f) / 10.0f;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //親がいない場合＝自分の親(root)が自分自身の場合、と解釈
        if (transform.root.gameObject == gameObject)
        {
            int i = 0;
            //丸め誤差解消(自分の今の位置)
            float nowx = Mathf.RoundToInt(transform.position.x * 10.0f) / 10.0f;
            float nowy = Mathf.RoundToInt(transform.position.y * 10.0f) / 10.0f;

            if (this.num == 1) return; //落下完了済みなので以下の処理不要
            if (transform.root.gameObject == gameObject)
            {
                //コンビ解散後の挙動を記述
                if (nowy == -5.5f)
                {
                    this.num = 1; //落下完了をお知らせ
                    return;
                }
                i = 0;
                foreach (GameObject puyo in this.puyos)
                {
                    //丸め誤差解消(フィールド中の全ぷよの位置)
                    this.puyox[i] = Mathf.RoundToInt(puyo.transform.position.x * 10.0f) / 10.0f;
                    this.puyoy[i] = Mathf.RoundToInt(puyo.transform.position.y * 10.0f) / 10.0f;
                    if (nowx == this.puyox[i] && nowy == this.puyoy[i] + 1.0f)
                    {
                        this.num = 1; //落下完了をお知らせ
                        return;
                    }
                    i++;
                }
                //落下完了していないので引き続き落下
                transform.Translate(0, -1.0f, 0, Space.World);
            }
        }
    }
}