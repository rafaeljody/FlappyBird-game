using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed = 5f;
    private float leftEdge;


    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x -1f /* to make pipes dissapears fully*/;
    }

    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime; // frame rate independent, constantly moving

        if(transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
