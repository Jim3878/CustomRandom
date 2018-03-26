using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class graphic : MonoBehaviour
{
    public Color start, end;
    public AnimationCurve up, down;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    [ContextMenu("calculate")]
    public void Cal()
    {
        cc = CalCounter();
    }

    KeyValuePair<float, int>[][] cc;
    private void OnDrawGizmos()
    {
        if (cc == null)
            cc = CalCounter();
        //Debug.Log(cc.Length);
        for (int i = 0; i < cc.Length; i++)
        {
            try
            {
                Color c = Color.Lerp(start, end, i / (float)cc.Length);
                Draw(cc[i], c);
            }
            catch (IndexOutOfRangeException)
            {
                Debug.Log(i);
                throw;
            }
        }
    }

    void Draw(KeyValuePair<float, int>[] counter, Color color)
    {
        Gizmos.color = color;
        //float width = (counter[counter.Length - 1].Key - counter[0].Key) / counter.Length;
        Vector3 from, to;
        for (int i = 1; i < counter.Length; i++)
        {
            from = new Vector3(counter[i - 1].Key, counter[i - 1].Value / 1000f);
            to = new Vector3(counter[i].Key, counter[i].Value / 1000f);
            Gizmos.DrawLine(from, to);
        }
    }

    KeyValuePair<float, int>[][] CalCounter()
    {
        List<KeyValuePair<float, int>[]> cc = new List<KeyValuePair<float, int>[]>();
        for (int i = 0; i <= 30; i += 5)
        {
            cc.Add(GetPunyonCounter.GetCounterByLevel(i, up.Evaluate, down.Evaluate));
        }

        return cc.ToArray();
    }

}

public class GetPunyonCounter
{

    public static KeyValuePair<float, int>[] GetCounterByLevel(int lv, Func<float, float> up, Func<float, float> down)
    {
        float start = lv * (4 / 30f) + 2;
        float end = lv * (9.5f / 30f) + 4;
        float avg = lv * 11.4f / 30f + 2.01f;
        int randomCount = 10000;
        var r = GetCounter(lv, randomCount);
        float sum = 0;
        for (int i = 0; i < r.Length; i++)
        {
            sum += r[i].Key * r[i].Value;
        }

        Debug.Log(string.Format("[{0}] avg = {1}", lv, sum / (float)randomCount));

        return r;
    }

    public static KeyValuePair<float, int>[] GetCounter(int lv, int randomCount)
    {
        var s = PunyonStuffRoll.inst(lv);
        var kp = new Dictionary<float, int>();
        float result;
        for (int i = 0; i < randomCount; i++)
        {
            result = GetResult(s.Roll());
            if (!kp.ContainsKey(result))
            {
                kp.Add(result, 0);
            }
            kp[result]++;
        }


        var skp = from data in kp orderby data.Key select data;

        return skp.ToArray();
    }

    static float GetResult(float result)
    {
        //return result;
        return Mathf.Round(result);
    }
}
