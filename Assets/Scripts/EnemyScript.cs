using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    private Rigidbody2D rd2d;

    private float startTime;

    public Transform startMarker;
    public Transform endMarker;
    public float fasts = 1.0F;

    private float journeyLength;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        //rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }
}
