using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : MonoBehaviour
{
    RankLine[] rankLines = null;

    int[] highScore = null;
    string[] rankerName = null;

    const int rankCount = 5;

    private void Awake()
    {
        rankLines = GetComponentsInChildren<RankLine>();
        highScore = new int[rankCount];
        rankerName = new string[rankCount];
    }
    private void Start()
    {
        rankLines[0].SetData("AAA", 123456);
        rankLines[1].SetData("BBB", 123456);
        rankLines[2].SetData("CCC", 123456);
        rankLines[3].SetData("DDD", 123456);
        rankLines[4].SetData("AAB", 123456);
    }
    private void SetDefaultData()
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
            rankerName[i] = $"{temp}{temp}{temp}";

            rankLines[i].SetData(rankerName[i], highScore[i]);
        }
    }
}
