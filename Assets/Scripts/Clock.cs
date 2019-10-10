using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Clock : MonoBehaviour
{

    public Stopwatch stopWatch;
    public static float elapsedTime;
    public static float percentTime;
    public static bool paused;

    // Use this for initialization
    void Start()
    {
        stopWatch = new Stopwatch();
        paused = true;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime = (float)stopWatch.Elapsed.TotalSeconds;
        percentTime = (Mathf.Round(elapsedTime * 100f) / 100f) % 1;

        GameObject Slide = GameObject.Find("Slides");
        Slides s = Slide.GetComponent<Slides>();

        if (!paused)
        {
            //s.currentSlide = (int)elapsedTime;
            float RoundedTime = Mathf.Round(elapsedTime * 100f) / 100f;
            if (stopWatch.ElapsedMilliseconds > 1000)
            {
                //UnityEngine.Debug.Log("BOOP "+ stopWatch.ElapsedMilliseconds);
                stopWatch.Restart();
            }
            //UnityEngine.Debug.Log(stopWatch.ElapsedMilliseconds);//percentTime + " / " + Mathf.Round(elapsedTime * 100f) / 100f + " / " + s.currentSlide);
        }
    }
    public void toggleWatch()
    {
        if (paused)
        {
            stopWatch.Start();
        }
        else
        {
            stopWatch.Stop();
            stopWatch.Reset();
        }
        paused = !paused;
    }
}
