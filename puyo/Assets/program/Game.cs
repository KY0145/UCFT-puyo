using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    GameObject[] puyos;
    float[] puyox = new float[100];
    float[] puyoy = new float[100];
    int[] checks = new int[100];
    int[] samecolornums = new int[100];
    int deleteall = 0;
    //deleteall：これまでに消してきた総削除ぷよ数
    int deletenum = 0;
    //deletenum：今回消したぷよ数
    public Text deletenumtext;
    List<int> samecolorset = new List<int>();
    int i = 0;

    void start()
    {
        this.puyos = GameObject.FindGameObjectsWithTag("puyo");
        this.deletenum = 0;
        //ぷよ落下終了時に毎回0にリセット
        i = 0;
        foreach (GameObject puyo in this.puyos)
        {
            if (this.samecolornums[i] >= 4) this.deletenum++;
            i++;
        }

        //textに総削除puyo数表示
        this.deleteall += this.deletenum;
        this.deletenumtext.GetComponent<Text>().text = this.deleteall.ToString("D3") + "個消した";

        i = 0;
        foreach (GameObject puyo in this.puyos)
        {

            //checks[i]：i番ぷよの確認作業終了フラグ…0：未完了、1：完了
            this.checks[i] = 0;
            //samecolornums[i]：i番ぷよと隣り合っている同色ぷよの数、基本は1（自分自身）
            this.samecolornums[i] = 1;
            //puyox[i],puyoy[i]：i番ぷよの位置座標（丸め誤差対策済）
            this.puyox[i] = Mathf.RoundToInt(puyo.transform.position.x * 10.0f) / 10.0f;
            this.puyoy[i] = Mathf.RoundToInt(puyo.transform.position.y * 10.0f) / 10.0f;
            i++;
        }
        i = 0;
        foreach (GameObject puyo in this.puyos)
        {
            if (this.samecolornums[i] >= 4)
            {
                Destroy(puyo);
            }
            i++;
        }
        for (i = 0; i < this.puyos.Length; i++)
        {
            if (this.puyox[i] == -0.5f && this.puyoy[i] == 6.5f)
            {
                SceneManager.LoadScene("gameover");
            }
        }
        for (i = 0; i < this.puyos.Length; i++)
        {
            if (this.samecolornums[i] < 4)
            {
                this.puyos[i].GetComponent<Tanpuyo>().numreset();
            }

        }
        

        
    }

    

    public void Check(int i)
    {
        
        this.samecolorset.Add(i);
        //元からchecks[i]=1ならi番ぷよは調査済なので確認しない
        if (this.checks[i] == 1) return;
        this.checks[i] = 1;    //これからi番ぷよを調査するので0→1に直しておく
        for (int j = 0; j < this.puyos.Length; j++)
        {
            if (this.puyox[i] == this.puyox[j] && this.puyoy[i] == this.puyoy[j] + 1.0f &&
            this.puyos[i].transform.name == this.puyos[j].transform.name && this.checks[j] == 0)
            {
                // <span class="crayon-c">下（j番ぷよ：未調査）と自分自身（i番ぷよ）が同色</span>
                Check(j);
            }
            if (this.puyox[i] == this.puyox[j] && this.puyoy[i] == this.puyoy[j] - 1.0f &&
            this.puyos[i].transform.name == this.puyos[j].transform.name && this.checks[j] == 0)
            {
                // <span class="crayon-c">上（j番ぷよ：未調査）と自分自身（i番ぷよ）が同色</span>
                Check(j);
            }
            if (this.puyox[i] == this.puyox[j] + 1.0f && this.puyoy[i] == this.puyoy[j] &&
            this.puyos[i].transform.name == this.puyos[j].transform.name && this.checks[j] == 0)
            {
                // <span class="crayon-c">左（j番ぷよ：未調査）と自分自身（i番ぷよ）が同色</span>
                Check(j);
            }
            if (this.puyox[i] == this.puyox[j] - 1.0f && this.puyoy[i] == this.puyoy[j] &&
            this.puyos[i].transform.name == this.puyos[j].transform.name && this.checks[j] == 0)
            {
                // <span class="crayon-c">右（j番ぷよ：未調査）と自分自身（i番ぷよ）が同色</span>
                Check(j);
            }
        }
        return;
        
    }

}





