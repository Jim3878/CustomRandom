using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class DigitizeWave
{
    /// <summary>
    /// key : X_value(0~1)
    /// value: Y_value
    /// </summary>
    private Dictionary<float, float> lineChart;
    private float totalY;
    private float start, size;

    /// <summary>
    /// 將線性方程式數位化成折線圖
    /// </summary>
    /// <param name="accuracy">折線圖分割點數量</param>
    /// <param name="linearFunc">線性方程式 y=f(x)</param>
    /// <param name="start">線性方程式起點</param>
    /// <param name="size">線性方程式範圍大小</param>
    public DigitizeWave(int accuracy, Func<float, float> linearFunc, float start = 0, float size = 1)
    {
        totalY = 0;
        this.start = start;
        this.size = size;
        lineChart = new Dictionary<float, float>();

        float y;
        float x;
        for (int i = 0; i < accuracy; i++)
        {
            x = (float)i + 1 / (accuracy - 1) * size + start;

            y = linearFunc(x);
            lineChart.Add(x, y);
            totalY += y;
        }
    }

    public KeyValuePair<float, float>[] GetKeyPoints()
    {
        return (from data in lineChart orderby data.Key select data).ToArray();
    }

    public float GetTotal()
    {
        return totalY;
    }
}




