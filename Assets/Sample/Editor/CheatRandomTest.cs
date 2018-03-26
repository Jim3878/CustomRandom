using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class CheatRandomTest
{

    [Test]
    public void CheatRandomTestSimplePasses()
    {
        float avg = 10.2f;
        float start = 6f;
        float end = 13.5f;

        Func<float, float> func = (x) =>
        {
            if (x <= avg)
            {
                //Debug.Log("fuc small= " + (x-start) / (avg - start));
                return (x - start) / (avg - start);
            }
            else
            {
                //Debug.Log("fuc= large" + (end - x) / (end - avg));
                return (end - x) / (end - avg);
            }
        };

        Func<float, float> sinFunc = (x) =>
        {
            return Mathf.Pow(Mathf.Sin(func(x) * Mathf.PI*0.5f),2f);
        };
        int acc = 100;
        var cheat = new CheatRandom(start, end, sinFunc, acc);
        Dictionary<float, int> randomCount = new Dictionary<float, int>();

        int max = 100000;
        for (int i = 0; i < max; i++)
        {
            var result = cheat.Random();
            if (!randomCount.ContainsKey(GetResult(result)))
            {
                randomCount.Add(GetResult(result), 0);
            }
            randomCount[GetResult(result)] += 1;
        }

        float sum = 0;
        var r = from data in randomCount orderby data.Key select data;
        int index = 0;
        foreach (var kp in r)
        {
            sum += (kp.Key * kp.Value);
            //Debug.Log(((index / (float)acc) * (end - start)) + start);
            //Assert.AreEqual(((index / (float)acc) * (end - start)) + start, kp.Key);
            Debug.Log(string.Format("[{0}] = {1:N0}", kp.Key, kp.Value));
            index += 1;
        }
        Debug.Log("avg = " + sum / (float)max);
    }

    public float GetResult(float result)
    {
        return Mathf.Round(result);
    }
}
