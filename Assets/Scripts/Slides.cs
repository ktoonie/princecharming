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

    public bool GetSlide(int slide)
    {
        if (slide <= totalSlide && slide > 0)
        {
            currentSlide = slide;
            return true;
        }
        else
        {
            return false;
        }
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
    public int RemoveSlide()
    {
        // Conditions:
        // 1/1 - Do nothing
        // y/x - Remove slide y, lower total, currentslide stays the same
        // x/x - Remove slide x, lower total, currentslide lowers
        if (totalSlide > 1)
        {
            if (currentSlide < totalSlide)
            {
                //Case 1: 1/2 Remove current slide, lower total, current slide stays the same
                totalSlide--;
                //Debug.Log("Removing slide " + currentSlide);
                return 1;
            }
            else
            {
                //Case 2: 2/2 Remove current slide, lower total, currentslide lowers
                currentSlide--;
                totalSlide--;
                //Debug.Log("Removing slide " + currentSlide);
                return 2;
            }
        }
        else
        {
            return 0;
        }
    }
}
