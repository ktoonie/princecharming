using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class InputManager : MonoBehaviour
{
    private bool draggingItem = false;
    private GameObject draggedObject;
    private Vector2 touchOffset;

    //public int pawnCount;
    public GameObject pawn;
    private float randX;
    private float randY;
    public List<GameObject> group = new List<GameObject>();
    private Clock c;
    private Slides s;
<<<<<<< HEAD
    private float meepleSize = 15f;
=======
    private Mat m;

    private Stopwatch stopWatch;
    private static float elapsedTime;
    private static float percentTime;
    private static bool paused;
>>>>>>> Tony

    private void Start()
    {
        c = FindObjectOfType<Clock>();
        s = FindObjectOfType<Slides>();
        m = FindObjectOfType<Mat>();

        stopWatch = new Stopwatch();
        paused = true;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            Vector2 mousePos = Input.mousePosition;
            Debug.Log("("+ mousePos.x + ", " + mousePos.y + ")");
            Debug.Log("Middle Mouse Button Pressed");
        }

        if (onLeftClick)
        {
            DragOrPickUp();
        }
        else
        {
            if (draggingItem)
                DropItem();
        }
        if (onRightClick)
        {
            Remove();
        }
        if (Input.GetKeyDown("up"))
        {
            addPawn();
        }
        if (Input.GetKeyDown("down"))
        {
            savePositions();
        }
        if (Input.GetKeyDown("right"))
        {
            nextSlide();
        }
        if (Input.GetKeyDown("left"))
        {
            prevSlide();
        }
        if (Input.GetKeyDown("space"))
        {
            toggleWatch();
        }
        if (Input.GetKeyDown("c"))
        {
            createSlide();
        }

        if (!paused)
        {
            elapsedTime = (float)stopWatch.Elapsed.TotalSeconds;
            percentTime = elapsedTime % 1;
            if (stopWatch.ElapsedMilliseconds > 1000)
            {
                //UnityEngine.Debug.Log("BOOP " + stopWatch.ElapsedMilliseconds);
                if(s.currentSlide < s.totalSlide)
                {
                    s.NextSlide();
                }
                else
                {
                    //UnityEngine.Debug.Log("Back to Slide 1");
                    s.GetSlide(1);
                }
                stopWatch.Restart();
            } else
            {
                foreach (GameObject p in group)
                {
                    int current = s.currentSlide;
                    int next = s.currentSlide + 1;
                    int previous = s.currentSlide - 1;
                    if (current < s.totalSlide)
                    {
                        p.GetComponent<Meeple>().transform.position = Vector2.Lerp(p.GetComponent<Meeple>().positions[previous], p.GetComponent<Meeple>().positions[current], elapsedTime);
                        //UnityEngine.Debug.Log(p.GetComponent<Meeple>().transform.position + " // " + p.GetComponent<Meeple>().positions[previous] + " // " + p.GetComponent<Meeple>().positions[current] + " // " + percentTime);
                    }
                    //p.GetComponent<Meeple>().transform.position = p.GetComponent<Meeple>().positions[current];
                    //UnityEngine.Debug.Log("Prev: " + previous + ", Next " + current);
                    //p.GetComponent<Meeple>().transform.position = Vector2.Lerp(p.GetComponent<Meeple>().positions[s.currentSlide - 2], p.GetComponent<Meeple>().positions[s.currentSlide - 1], percentTime);
                }
            }
        }
    }
    Vector2 CurrentTouchPosition
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    private void toggleWatch()
    {
        if (paused)
        {
            stopWatch.Start();
            //getSlide(1);
        }
        else
        {
            stopWatch.Stop();
            stopWatch.Reset();
            getSlide(s.currentSlide);
        }
        paused = !paused;
    }
    private void DragOrPickUp()
    {
        var inputPosition = CurrentTouchPosition;
        if (draggingItem)
        {
            Vector3 bound = inputPosition + touchOffset;
            bound.x = Mathf.Clamp(bound.x, m.left, m.right);
            bound.y = Mathf.Clamp(bound.y, m.bottom, m.top);

            draggedObject.transform.position = bound;
        }
        else
        {
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null)
                {
                    draggingItem = true;
                    draggedObject = hit.transform.gameObject;
                    touchOffset = (Vector2)hit.transform.position - inputPosition;
                    draggedObject.transform.localScale = new Vector2(meepleSize, meepleSize);
                }
            }
        }
    }
    private void Remove()
    {
        var inputPosition = CurrentTouchPosition;
        RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
        if (touches.Length > 0)
        {
            var hit = touches[0];
            if (hit.transform != null)
            {
                draggedObject = hit.transform.gameObject;
                group.Remove(draggedObject);
                Destroy(draggedObject);
            }
        }        
    }
    private bool onLeftClick
    {
        get
        {
            return Input.GetMouseButton(0);
        }
    }
    private bool onRightClick
    {
        get
        {
            return Input.GetMouseButton(1);
        }
    }
    void DropItem()
    {
        draggingItem = false;
        draggedObject.transform.localScale = new Vector2(meepleSize, meepleSize);
        //savePositions();
        draggedObject.GetComponent<Meeple>().Save(s.currentSlide);
    }
    public void createSlide()
    {
        if (s.AddSlide())
        {
            foreach (GameObject p in group)
            {
                p.GetComponent<Meeple>().Insert(s.currentSlide-1);
            }
        } else
        {
            foreach (GameObject p in group)
            {
                p.GetComponent<Meeple>().Add();
            }
        }
    }
    public void removeSlide()
    {
        if (s.totalSlide > 1)
        {
            foreach (GameObject p in group)
            {
                p.GetComponent<Meeple>().Remove(s.currentSlide);
            }
            if (s.currentSlide < s.totalSlide)
            {
                if (s.currentSlide > 1)
                {
                    s.currentSlide--;
                }
                s.totalSlide--;
                //UnityEngine.Debug.Log("Removing slide " + s.currentSlide);
            }
            else
            {
                s.currentSlide--;
                s.totalSlide--;
                //UnityEngine.Debug.Log("Removing slide " + s.currentSlide);
            }
            recallPositions(s.currentSlide);
        }
    }
    public void getSlide(int slide)
    {
        if (s.GetSlide(slide))
        {
            recallPositions(slide);
        }
    }
    public void nextSlide()
    {
        if (s.NextSlide())
        {
            recallPositions(s.currentSlide);
        }
    }
    public void prevSlide()
    {
        if (s.PrevSlide())
        {
            recallPositions(s.currentSlide);
        }
    }
    private void savePositions()
    {
        foreach (GameObject p in group)
        {
            p.GetComponent<Meeple>().Save(s.currentSlide);
        }
    }
    private void recallPositions(int pos)
    {
        foreach (GameObject p in group)
        {
            p.GetComponent<Meeple>().Recall(pos);
        }
    }
    public void addPawn()
    {
        randX = Random.Range(m.left, m.right);
        randY = Random.Range(m.bottom, m.top);
        GameObject cheerLeader = Instantiate(pawn);
        group.Add(cheerLeader);
        cheerLeader.transform.localScale = new Vector2(meepleSize, meepleSize);
        cheerLeader.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        for (int i = 0; i < s.totalSlide; i++)
        {
            cheerLeader.GetComponent<Meeple>().positions.Add(cheerLeader.transform.position);
            //Debug.Log("Filling positions " + i);
        }
    }
}
