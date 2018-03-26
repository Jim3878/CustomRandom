using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheatRandom
{

    //int accuracy;
    //Func<float, float> linearFunc;
    KeyValuePair<float, float>[] ladder;
    public KeyValuePair<float, float>[] laderGet
    {
        get
        {
            return ladder;
        }
    }
    float seedMin, seedMax;

    public CheatRandom(float min, float max, Func<float, float> linearFunc, int accuracy = 100)
    {
        var digitWave = new DigitizeWave(accuracy, linearFunc, min, max - min);
        var waveKeyPoints = digitWave.GetKeyPoints();
        SetLadder(waveKeyPoints);

        //Debug.Log("[cheatDice]seedMax = " + seedMax);
        //Debug.Log("[cheatDice]seedMin = " + seedMin);
    }

    private void SetLadder(KeyValuePair<float, float>[] keyValuePoints)
    {
        this.ladder = new KeyValuePair<float, float>[keyValuePoints.Length - 1];
        float sumOfValue = 0;
        

        //將數位化的波狀圖每層疊加為階梯狀
        for (int i = 0; i < keyValuePoints.Length - 1; i++)
        {
            if (keyValuePoints[i].Value < 0)
            {
                throw new ArgumentException("機率最低值為0");
            }

            sumOfValue += keyValuePoints[i].Value;
            ladder[i] = new KeyValuePair<float, float>(keyValuePoints[i].Key, sumOfValue);
        }
        seedMin = ladder[0].Value;
        seedMax = sumOfValue;
    }

    public float Random()
    {
        try
        {
            float seed = UnityEngine.Random.Range(seedMin, seedMax);
            //Debug.Log("[cheatDice]seed = " + seed);
            for (int i = 1; i < ladder.Length; i++)
            {
                if (ladder[i - 1].Value <= seed && ladder[i].Value > seed)
                {
                    return ladder[i - 1].Key;
                }
            }

            string excep = "";
            for (int i = 0; i < ladder.Length; i++)
            {
                excep += string.Format("{0} :　x = {1}  y = {2}　\n", i, ladder[i].Key, ladder[i].Value);
            }
            throw new Exception("資料異常\n" + excep);
        }
        catch (Exception)
        {
            string excep = "";
            for (int i = 0; i < ladder.Length; i++)
            {
                excep += string.Format("{0} :　x = {1}  y = {2}　\n", i, ladder[i].Key, ladder[i].Value);
            }
            Debug.Log(excep);
            throw;
        }

    }
}
