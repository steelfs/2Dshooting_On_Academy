using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

public class RankPanel : MonoBehaviour
{
    RankLine[] rankLines = null; //�гο� �پ��ִ� ��ŷ �ٵ�

    int[] highScore = null; //�ְ���� 1~5
    string[] rankerName = null;

    const int rankCount = 5;

    int updatedIndex; // ��Ŀ�� �߰��� �ε���

    TMP_InputField inputField;

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>();
        highScore = new int[rankCount]; //�ְ������� �迭
        rankerName = new string[rankCount];// ��Ŀ�̸� ����� �迭
        inputField = GetComponentInChildren<TMP_InputField>(true);//��ǲ�ʵ� ã��
        inputField.onEndEdit.AddListener(OnNameInputEnd); // onEndEdit ���� �� ����� �Լ����
    }

 

    private void Start()
    {
        LoadRankingData(); //�����Ҷ� ����� ������ �ҷ�����
    }


    private void SetDefaultData()//��ŷ�� �ʱⰪ���� �ǵ����� �Լ�
    {
        for(int i = 0; i< rankCount; i++)
        {
            int score = 10;
            for (int j = rankCount - i; j > 0; j--)
            {
                score *= 10;
            }
            highScore[i]  = score;

            char temp = 'A';
            temp = (char)((byte)temp + i);
            rankerName[i] = $"{temp}{temp}{temp}"; //�̸� : AAA~ EEE����

            rankLines[i].SetData(rankerName[i], highScore[i]); // ������ ����
        }
    }

    void SaveRankingData()
    {
        //PlayerPrefs �������� ������ �ִ�. ������Ʈ���� �����͸� �����Ѵ� ���� ���ϴ´�� ���� �����Ҽ��ִ�.

        //SaveData saveData = new SaveData();
        SaveData saveData = new();//������ �ٿ��� ����
        saveData.rankerNames = rankerName; //������ �����ϰ� 
        saveData.highScores = highScore;

        string json = JsonUtility.ToJson(saveData); // saveData�� json ������ ���ڿ��� �����Ѱ�

        //system.IO
        //Directory : �������� ���� ���Ǳ���� �����ϴ� Ŭ����
        //File :  ���ϰ��� ���� ���Ǳ���� �����ϴ� Ŭ����
        //Application.dataPath //�����Ϳ����� Assets, ������������� Data����
        string path = $"{Application.dataPath}/Save/";
        if (!Directory.Exists(path))//Exist : Ư�������� ������ true ������ false
        {
            Directory.CreateDirectory(path);
        }

        string fullPath = $"{path}Save.json";
        File.WriteAllText(fullPath, json);//fullPath ��ο� json������ ����� ��� �ؽ�Ʈ�� �����Ѵ�

    }

    /// <summary>
    ///  ��ŷ �����͸� �ҷ����� �Լ�
    /// </summary>
    /// <returns>�ҷ����� ���������� true, �ƴϸ� false</returns>
    bool LoadRankingData()//
    {
        bool result = false;

        string path = $"{Application.dataPath}/Save/";// ��� ���ϱ�
        string fullPath = $"{path}Save.json"; // ��ü��� 

        result = Directory.Exists(path) && File.Exists(fullPath); //������ ������ ������ �ִ��� Ȯ��
        if (result)// ������ ���� ��� ���� ����
        {
            //���� �ε�
            string json = File.ReadAllText(fullPath);// ���Ͽ� ���ִ� �ؽ�Ʈ ��� �б�

            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);// Json�������� �� ���ڿ��� �Ľ��ؼ� SaveData�������� ����

            highScore = loadedData.highScores;
            rankerName = loadedData.rankerNames;
        }
        else
        {
            //����Ʈ�� ����
            SetDefaultData();
        }
        RefreshRankLines(); //�� �ҷ������� ȭ�� ����
        return result;
    }
    void RefreshRankLines()// ���� ������ ������ ���� UI�� ����
    {
        Debug.Log("��������");
        for(int i = 0; i< rankCount; i++)
        {
            rankLines[i].SetData(rankerName[i], highScore[i]);
        }
    }
    
    /// <summary>
    /// ��ŷ������Ʈ�ϴ� �Լ�
    /// </summary>
    /// <param name="score">�� ����</param>
    void RankUpdate(int score) //1�� -> 5�� ����
    {
        //������ �������� score�� ������ ��ŷ�� ���ŵǾ�� �Ѵ�
        //�� �̸��� ���Ƿ� ����
        for (int i = 0; i < rankCount; i++)
        {
            if (highScore[i] < score) //�� ����� score���� ������ 
            {
                for(int j = rankCount -1; j > 1; j--) // �Ʒ��� ��ĭ�� �б�
                {
                    highScore[j] = highScore[j - 1];
                    rankerName[j] = rankerName[j - 1];
                }
                highScore[i] = score;
                //rankerName[i] = "akakak";
                updatedIndex = i; // �и��� ������ �ε��� �����صα�

                Vector3 newPos = inputField.transform.position;
                newPos.y = rankLines[i].transform.position.y;
                inputField.transform.position = newPos;
                inputField.gameObject.SetActive(true);
                break;
            }
        }
    }
    private void OnNameInputEnd(string text)// ��ǲ�ʵ��� �Է��� �������� ȣ��Ǵ� AddListner�� ��ϵ� �Լ�
    {
        rankerName[updatedIndex] = text; //�̸�����
        inputField.gameObject.SetActive(false); // ��ǲ�ʵ� �Ⱥ��̰� �ϱ�
        RefreshRankLines(); // ȭ�鰻��
        SaveRankingData(); // �����ϱ�
    }
    public void TestSave()
    {
        SaveRankingData();
    }
    public void TestLoad()
    {
        LoadRankingData();
    }
    public void TestRankUpdate(int score)
    {
        RankUpdate(score);
    }
}
