using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slides : MonoBehaviour
{
    public int currentSlide;
    public int totalSlide;

    // Use this for initialization
    void Start()
    {
        currentSlide = totalSlide = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool NextSlide()
    {
        if (currentSlide < totalSlide)
        {
            currentSlide++;
            //Debug.Log(currentSlide + "/" + totalSlide);
            return true;
        } else
        {
            //Debug.Log(currentSlide + "/" + totalSlide);
            return false;
        }
    }
    public bool PrevSlide()
    {
        if (currentSlide > 1)
        {
            currentSlide--;
            //Debug.Log(currentSlide + "/" + totalSlide);
            return true;
        }
        else
        {
            //Debug.Log(currentSlide + "/" + totalSlide);
            return false;
        }
    }
    public bool AddSlide()
    {
        if (currentSlide < totalSlide)
        {
            currentSlide++;
            totalSlide++;
            //Debug.Log("Inserting " + currentSlide + "/" + totalSlide);
            return true;
        } else
        {
            currentSlide++;
            totalSlide++;
            //Debug.Log("Adding " + currentSlide + "/" + totalSlide);
            return false;
        }
    }
}
