using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]//  //Serializable 이 클래스는 직렬화 되어야한다고 표시해놓은 어트리뷰트
public class SaveData 
{
    public string[] rankerNames; // 랭커들 이름 저장 
    public int[] highScores; // 점수 저장 
}
