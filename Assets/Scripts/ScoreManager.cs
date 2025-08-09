using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int badScore = 0;
    private int goodScore = 0;


    void UpdateGoodScore(int score)
    {
        goodScore += score;
    }

    void UpdateBadScore(int score)
    {
        badScore += score;
    }

    int GetEnding(bool isLove, bool isHuman)
    {
        int value = (int)(isLove && isHuman ? PlayerEnding.LoveAndHuman : !isLove && isHuman ? PlayerEnding.DeleteAndHuman : isLove && !isHuman ? PlayerEnding.LoveNotHuman : PlayerEnding.DeleteNotHuman);
        return value;
    }

    
}

enum PlayerEnding
{
    LoveAndHuman,
    DeleteAndHuman,
    LoveNotHuman,
    DeleteNotHuman
}


