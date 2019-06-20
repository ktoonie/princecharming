using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        c = FindObjectOfType<Clock>();
        s = FindObjectOfType<Slides>();
    }
    void Update()
    {
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
            c.toggleWatch();
        }
        if (Input.GetKeyDown("c"))
        {
            createSlide();
        }
    }
    Vector2 CurrentTouchPosition
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    private void DragOrPickUp()
    {
        var inputPosition = CurrentTouchPosition;
        if (draggingItem)
        {
            draggedObject.transform.position = inputPosition + touchOffset;
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
                    draggedObject.transform.localScale = new Vector2(1, 1);
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
        draggedObject.transform.localScale = new Vector2(.75f, .75f);
        //savePositions();
        draggedObject.GetComponent<Meeple>().Save(s.currentSlide);
    }
    public void createSlide()
    {
        if (s.AddSlide())
        {
            foreach (GameObject p in group)
            {
                p.GetComponent<Meeple>().Insert(s.currentSlide);
            }
        } else
        {
            foreach (GameObject p in group)
            {
                p.GetComponent<Meeple>().Add();
            }
        }
    }
    public void nextSlide()
    {
        if (s.NextSlide())
        {
            recallPositions();
        }
    }
    public void prevSlide()
    {
        if (s.PrevSlide())
        {
            recallPositions();
        }
    }
    private void savePositions()
    {
        foreach (GameObject p in group)
        {
            p.GetComponent<Meeple>().Save(s.currentSlide);
        }
    }
    private void recallPositions()
    {
        foreach (GameObject p in group)
        {
            p.GetComponent<Meeple>().Recall(s.currentSlide);
        }
    }
    public void addPawn()
    {
        randX = Random.Range(-8.0f, 8.0f);
        randY = Random.Range(-4.0f, 4.0f);
        GameObject cheerLeader = Instantiate(pawn);
        group.Add(cheerLeader);
        cheerLeader.transform.position = new Vector2(randX, randY);
        cheerLeader.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        for (int i = 0; i < s.totalSlide; i++)
        {
            cheerLeader.GetComponent<Meeple>().positions.Add(cheerLeader.transform.position);
            Debug.Log("Filling positions " + i);
        }
    }
}
