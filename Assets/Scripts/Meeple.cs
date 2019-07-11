using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meeple : MonoBehaviour
{
    private SpriteRenderer rend;
    public List<Vector2> positions = new List<Vector2>();
    private GameObject Slide;
    private Slides s;

    void OnMouseDown()
    {
        rend.sortingOrder = 1;
    }

    void OnMouseUp()
    {
        rend.sortingOrder = 0;
    }

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
    }

    public void Add()
    {
        positions.Add(transform.position);
        //Debug.Log("New count is: "+positions.Count);
    }

    public void Insert(int slide)
    {
        positions.Insert(slide, transform.position);
    }

    public void Save(int slide)
    {
        positions[slide-1] = transform.position;
        Debug.Log("Saving position: " + positions[slide - 1] + " in slide " + slide);
    }

    public void Recall(int slide)
    {
        transform.position = positions[slide-1];
        Debug.Log("Recalling position: " + positions[slide-1] + " in slide " + slide);
    }
}
