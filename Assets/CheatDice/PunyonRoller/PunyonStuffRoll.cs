using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PunyonStuffRoll : MonoBehaviour
{
    public AnimationCurve up, down;
    private PunyonStuffRandom random;

    public static PunyonStuffRoll inst(int lv)
    {
        PunyonStuffRoll psr = Resources.Load<GameObject>("Prefabs/PunyonStuffRoll").GetComponent<PunyonStuffRoll>();
        psr.Initialize(lv);

        return psr;
    }

    public float Roll()
    {
        return random.Random();
    }

    private void Initialize(int lv)
    {
        GetCounterByLevel(lv);
    }

    private void GetCounterByLevel(int lv)
    {
        float start = lv * (4 / 30f) + 2;
        float end = lv * (9.5f / 30f) + 4;
        float avg = lv * 11.4f / 30f + 2.01f;
        int randomCount = 10000;
        GetCounter(start, end, avg, 100, randomCount, up.Evaluate, down.Evaluate);
    }

    private void GetCounter(float start, float end, float avg, int acc, int randomCount, Func<float, float> up, Func<float, float> down)
    {
        random = new PunyonStuffRandom(start, end, avg, acc, up, down);
    }


    public class PunyonStuffRandom
    {

        CheatRandom cheat;
        public PunyonStuffRandom(float start, float end, float avg, int acc, Func<float, float> funcUp, Func<float, float> funcDown)
        {
            Func<float, float> dummyFunc = (x) =>
            {

                if (x <= avg)
                {
                    return funcUp((x - start) / Mathf.Abs(avg - start));
                }
                else
                {
                    return funcDown((x - avg) / Mathf.Abs(end - avg));
                }
            };

            Func<float, float> sinFunc = (x) =>
            {
                if (x <= avg)
                {
                    return Mathf.Pow(Mathf.Sin(funcUp(x) * Mathf.PI * 0.5f), 4f);
                }
                else
                {
                    return Mathf.Pow(Mathf.Sin(funcUp(x) * Mathf.PI * 0.25f + Mathf.PI * 0.25f), 4f);
                }
            };

            cheat = new CheatRandom(start, end, dummyFunc, acc);
        }

        public float Random()
        {
            return cheat.Random();
        }
    }
}