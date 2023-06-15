using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LifePanel : MonoBehaviour
{
    //플레이어의 HP가 변경되면 UI ㅔㅇ서 변경되서 보여주기
    TextMeshProUGUI lifeTxt;
    TextMeshProUGUI lifeTxt2;

    Player player;
    private void Awake()
    {
        Transform textTransform = transform.GetChild(2);
        lifeTxt2 = textTransform.GetComponent<TextMeshProUGUI>();


        lifeTxt = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        player = GameManager.Inst.Player;

        player.onLifeChange += UpdateLifeText;
    }
    private void Start()
    {
        GameManager.Inst.Player.onLifeChange += (life) => lifeTxt2.text = life.ToString();
    }
    void UpdateLifeText(int life)
    {
        lifeTxt.text = life.ToString();
    }
}
