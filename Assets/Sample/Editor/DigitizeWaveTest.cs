using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class DigitizeWaveTest
{

    [Test]
    public void WaveTest()
    {
        int accuray = 50;
        float start = 0;
        float end = 10;
        var digit = new DigitizeWave(accuray, (x) => x, start, end - start);
        var kp = digit.GetKeyPoints();

        Assert.AreEqual(accuray + 1, kp.Length);
        Assert.AreEqual(start, kp[0].Key);

        for (int i = 1; i <= accuray; i++)
        {
            Assert.AreEqual(end * i / accuray, kp[i].Key);
        }
    }

    [Test]
    public void DigitizeWave_StartFromHeadEndInTailStar()
    {
        int accuray = 5;
        var digit = new DigitizeWave(accuray, (x) => Mathf.Sin(x));

        Assert.AreEqual(6, digit.GetKeyPoints().Length);
        Assert.AreEqual(Mathf.Sin(0), digit.GetKeyPoints()[0].Value);
        Assert.AreEqual(Mathf.Sin(1), digit.GetKeyPoints()[digit.GetKeyPoints().Length-1].Value);
        
    }

    [Test]
    public void DigitizeWave_OutPutValueTest()
    {
        // Use the Assert class to test conditions.
        int accuray = 5;
        var digit = new DigitizeWave(accuray, (x) => Mathf.Cos(x));

        var points = digit.GetKeyPoints();
        for (int i = 0; i < points.Length; i++)
        {
            Assert.AreEqual(Mathf.Cos(digit.GetKeyPoints()[0].Key), digit.GetKeyPoints()[0].Value);
        }
    }

    [Test]
    public void DigitizeWave_OutPutValue_IsSorting()
    {
        // Use the Assert class to test conditions.
        int accuray = 5;
        var digit = new DigitizeWave(accuray, (x) => Mathf.Cos(x));

        var points = digit.GetKeyPoints();
        for (int i = 0; i < points.Length - 1; i++)
        {
            Assert.IsTrue(points[i].Key < points[i + 1].Key);
        }
    }

}
