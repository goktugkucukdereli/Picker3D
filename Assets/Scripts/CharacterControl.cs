using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    public float dragSpeed;
    //[HideInInspector]
    public List<Rigidbody> collected;
    Vector3 direction;
    private Vector3? lastMousePos;
    private Vector2 diff;
    public float defaultMinX = -1.95f;
    public float defaultMaxX = 1.95f;
    public float currentMinX;
    public float currentMaxX;
    public static CharacterControl instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        collected = new List<Rigidbody>();
        SetDefaultClamp();
    }

    public void SetClampValues(float min, float max)
    {
        currentMinX = min;
        currentMaxX = max;
    }

    public void SetDefaultClamp()
    {
        currentMinX = defaultMinX;
        currentMaxX = defaultMaxX;
    }

    private void Update()
    {
        if (GameManager.instance.state != GameState.Playing) return;
        GetInput();
        Clamp();
    }

    private void Clamp()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(transform.position.x, currentMinX, currentMaxX), transform.position.y, transform.position.z), Time.deltaTime * 15);
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lastMousePos = null;
            direction = Vector3.zero;
        }
        if (lastMousePos != null)
        {
            diff = (Vector2)Input.mousePosition - (Vector2)lastMousePos;
            lastMousePos = Input.mousePosition;
            direction = Vector3.Lerp(direction, Vector3.right * diff.x, Time.deltaTime * 5);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.state != GameState.Playing)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        Movement();
    }

    private void Movement()
    {
        rb.velocity = Vector3.ClampMagnitude((direction * dragSpeed), 7) + (Vector3.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            Checkpoint checkpoint = other.GetComponent<Checkpoint>();
            other.enabled = false;
            StartCoroutine(PushCollecteds(checkpoint));
        }
        else if (other.CompareTag("Finish"))
        {
            GameManager.instance.Win();
        }
    }

    private IEnumerator PushCollecteds(Checkpoint check)
    {
        float tempSpeed = speed;
        float tempDragSpeed = dragSpeed;
        speed = 0;
        dragSpeed = 0;

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < collected.Count; i++)
        {
            collected[i].AddForce(transform.forward * 500);
        }
        yield return new WaitForSeconds(2f);

        if (check.totalCollecteds >= check.requiredCollecteds && !check.gotObstacle)
        {
            //move on;
            speed = tempSpeed;
            dragSpeed = tempDragSpeed;
            check.MoveOn();
        }
        else
        {
            check.noMoreCheck = true;
            GameManager.instance.GameOver();
        }
    }
}