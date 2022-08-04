using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanpuyo : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] puyos;
    GameObject Director;
    int num = 0; //num=0�ŗ����A�I������fallcheck�ցAnum=1�Ȃ�x�~
    float[] puyox = new float[100];
    float[] puyoy = new float[100];

    //�Ղ������Ɉ�ė������邽�߁A�S�Ղ��this.num=0�i�ēX�N���v�g���Ăяo���j
    public void numreset()
    {
        int i = 0;
        this.num = 0; //�������ĊJ(Update���\�b�h�ɏ]��)
                      //����:�����_�ł̃t�B�[���h���̂Ղ�ʒu�������ōĒ���
        this.puyos = GameObject.FindGameObjectsWithTag("puyo");
        foreach (GameObject puyo in this.puyos)
        {
            //�ۂߌ덷����(�t�B�[���h���̑S�Ղ�̈ʒu)
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
            //�ۂߌ덷����(�t�B�[���h���̑S�Ղ�̈ʒu)
            this.puyox[i] = Mathf.RoundToInt(puyo.transform.position.x * 10.0f) / 10.0f;
            this.puyoy[i] = Mathf.RoundToInt(puyo.transform.position.y * 10.0f) / 10.0f;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�e�����Ȃ��ꍇ�������̐e(root)���������g�̏ꍇ�A�Ɖ���
        if (transform.root.gameObject == gameObject)
        {
            int i = 0;
            //�ۂߌ덷����(�����̍��̈ʒu)
            float nowx = Mathf.RoundToInt(transform.position.x * 10.0f) / 10.0f;
            float nowy = Mathf.RoundToInt(transform.position.y * 10.0f) / 10.0f;

            if (this.num == 1) return; //���������ς݂Ȃ̂ňȉ��̏����s�v
            if (transform.root.gameObject == gameObject)
            {
                //�R���r���U��̋������L�q
                if (nowy == -5.5f)
                {
                    this.num = 1; //�������������m�点
                    return;
                }
                i = 0;
                foreach (GameObject puyo in this.puyos)
                {
                    //�ۂߌ덷����(�t�B�[���h���̑S�Ղ�̈ʒu)
                    this.puyox[i] = Mathf.RoundToInt(puyo.transform.position.x * 10.0f) / 10.0f;
                    this.puyoy[i] = Mathf.RoundToInt(puyo.transform.position.y * 10.0f) / 10.0f;
                    if (nowx == this.puyox[i] && nowy == this.puyoy[i] + 1.0f)
                    {
                        this.num = 1; //�������������m�点
                        return;
                    }
                    i++;
                }
                //�����������Ă��Ȃ��̂ň�����������
                transform.Translate(0, -1.0f, 0, Space.World);
            }
        }
    }
}