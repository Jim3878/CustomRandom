using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheatRandom
{

    int accuracy;
    Func<float, float> linearFunc;
    KeyValuePair<float, float>[] Ladder;

    public CheatRandom(float min, float max, Func<float, float> linearFunc, int accuracy = 100)
    {
        var digitWave = new DigitizeWave(accuracy, linearFunc, min, max - min);
        var waveKeyPoints = digitWave.GetKeyPoints();


    }

    private void SetLadder(KeyValuePair<float, float>[] keyValuePoints)
    {
        this.Ladder = new KeyValuePair<float, float>[keyValuePoints.Length - 1];
        for (int i = 1; i < keyValuePoints.Length; i++)
        {
            Ladder[i-1]=
        }
    }
}
