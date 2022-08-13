using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 direction;
    public float gravity = -9.8f;
    public float strength = 5f;
    public float tilt = 5f;

    // to change the sprite we need an access to the sprite renderer component
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;

    private void Awake() // when an object is initialized, so its the best place to access the sprite (once for the whole lifecycle)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() // very first frame when object is enabled
    {
        InvokeRepeating(nameof(AnimateSprite), 0.10f, 0.10f); //repeatedly called that funct over an over (invokerepeating). cycles every 0.15s
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    private void Update() //call automatically every single frame, handle things like input
    {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) /* 0 means it's left click */)
        {
            direction = Vector3.up * strength;
        }

        // handle if the game is on phone
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // i just touch the screen with my finger
            if(touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }

        }

        // gravity on the y axis of bird
        direction.y += gravity * Time.deltaTime; // karena gravity adalah (meter per second squared) jadinya kita kali 2
        transform.position += direction * Time.deltaTime; //frame rate indepedent, it is going to be a consistent frame rate

        //tilt the bird
        Vector3 rotation = transform.eulerAngles;
        rotation.z = direction.y * tilt;
        transform.eulerAngles = rotation;

    }

    // call this func every 0.15s
    // increase the index
    // reassign the sprites on our sprite renderer
    private void AnimateSprite()
    {
        spriteIndex++;

        if(spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }

        spriteRenderer.sprite = sprites[spriteIndex];

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            // not the best func on performance
            FindObjectOfType<GameManager>().GameOver();
        } else if(collision.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }

}
