using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour {

    public float SpeedForce = 1f;
    public float TourqeForce = 1f;
    public bool Sterable = false;

    private Rigidbody2D rb;
    private float InputForward=0;
    private float InputRight=0;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb = GetComponent<Rigidbody2D>();
        if (Sterable)
        {
            InputRight = Input.GetAxis("Horizontal");
            InputForward = Input.GetAxis("Vertical");
        }
        if (InputForward != 0)
        {
            rb.AddForce(transform.up*InputForward * SpeedForce);
        }
        if (rb.velocity.magnitude > 0)
        {
            rb.AddTorque(InputRight*TourqeForce*-1.0f * Mathf.Sign(InputForward));
        }
	}

    public void SetInput(float v1,float v2)
    {
        InputForward = v1;
        InputRight = v2;
    }
}
