using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

public class RankPanel : MonoBehaviour
{
    RankLine[] rankLines = null; //패널에 붙어있는 랭킹 줄들

    int[] highScore = null; //최고득점 1~5
    string[] rankerName = null;

    const int rankCount = 5;

    int updatedIndex; // 랭커가 추가된 인덱스

    TMP_InputField inputField;

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>();
        highScore = new int[rankCount]; //최고점수용 배열
        rankerName = new string[rankCount];// 랭커이름 저장용 배열
        inputField = GetComponentInChildren<TMP_InputField>(true);//인풋필드 찾기
        inputField.onEndEdit.AddListener(OnNameInputEnd); // onEndEdit 됐을 때 실행될 함수등록
    }

 

    private void Start()
    {
        LoadRankingData(); //시작할때 저장된 데이터 불러오기
    }


    private void SetDefaultData()//랭킹을 초기값으로 되돌리는 함수
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
            rankerName[i] = $"{temp}{temp}{temp}"; //이름 : AAA~ EEE까지

            rankLines[i].SetData(rankerName[i], highScore[i]); // 데이터 세팅
        }
    }

    void SaveRankingData()
    {
        //PlayerPrefs 편리하지만 문제가 있다. 레지스트리에 데이터를 저장한다 따라서 원하는대로 값을 변경할수있다.

        //SaveData saveData = new SaveData();
        SaveData saveData = new();//윗줄을 줄여서 쓴것
        saveData.rankerNames = rankerName; //데이터 복사하고 
        saveData.highScores = highScore;

        string json = JsonUtility.ToJson(saveData); // saveData를 json 형식의 문자열로 변경한것

        //system.IO
        //Directory : 폴더관련 각종 편의기능을 제공하는 클래스
        //File :  파일관련 각종 편의기능을 제공하는 클래스
        //Application.dataPath //에디터에서는 Assets, 빌드버전에서는 Data폴더
        string path = $"{Application.dataPath}/Save/";
        if (!Directory.Exists(path))//Exist : 특정폴더가 있으면 true 없으면 false
        {
            Directory.CreateDirectory(path);
        }

        string fullPath = $"{path}Save.json";
        File.WriteAllText(fullPath, json);//fullPath 경로에 json변수에 저장된 모든 텍스트를 저장한다

    }

    /// <summary>
    ///  랭킹 데이터를 불러오는 함수
    /// </summary>
    /// <returns>불러오기 성공했으면 true, 아니면 false</returns>
    bool LoadRankingData()//
    {
        bool result = false;

        string path = $"{Application.dataPath}/Save/";// 경로 구하기
        string fullPath = $"{path}Save.json"; // 전체경로 

        result = Directory.Exists(path) && File.Exists(fullPath); //저장할 폴더와 파일이 있는지 확인
        if (result)// 폴더와 파일 모두 있을 때만
        {
            //파일 로딩
            string json = File.ReadAllText(fullPath);// 파일에 써있는 텍스트 모두 읽기

            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);// Json형식으로 된 문자열을 파싱해서 SaveData형식으로 저장

            highScore = loadedData.highScores;
            rankerName = loadedData.rankerNames;
        }
        else
        {
            //디폴트값 세팅
            SetDefaultData();
        }
        RefreshRankLines(); //다 불러왔으면 화면 갱신
        return result;
    }
    void RefreshRankLines()// 현제 설정된 데이터 값을 UI에 갱신
    {
        Debug.Log("리프레시");
        for(int i = 0; i< rankCount; i++)
        {
            rankLines[i].SetData(rankerName[i], highScore[i]);
        }
    }
    
    /// <summary>
    /// 랭킹업데이트하는 함수
    /// </summary>
    /// <param name="score">새 점수</param>
    void RankUpdate(int score) //1등 -> 5등 까지
    {
        //기존의 점수보다 score가 높으면 랭킹이 갱신되어야 한다
        //새 이름은 임의로 설정
        for (int i = 0; i < rankCount; i++)
        {
            if (highScore[i] < score) //각 등수가 score보다 작으면 
            {
                for(int j = rankCount -1; j > 1; j--) // 아래로 한칸씩 밀기
                {
                    highScore[j] = highScore[j - 1];
                    rankerName[j] = rankerName[j - 1];
                }
                highScore[i] = score;
                //rankerName[i] = "akakak";
                updatedIndex = i; // 밀리기 시작한 인덱스 저장해두기

                Vector3 newPos = inputField.transform.position;
                newPos.y = rankLines[i].transform.position.y;
                inputField.transform.position = newPos;
                inputField.gameObject.SetActive(true);
                break;
            }
        }
    }
    private void OnNameInputEnd(string text)// 인풋필드의 입력이 끝났을때 호출되는 AddListner에 등록된 함수
    {
        rankerName[updatedIndex] = text; //이름변경
        inputField.gameObject.SetActive(false); // 인풋필드 안보이게 하기
        RefreshRankLines(); // 화면갱신
        SaveRankingData(); // 저장하기
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
