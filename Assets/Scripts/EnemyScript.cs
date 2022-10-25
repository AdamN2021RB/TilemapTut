using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;
    private Rigidbody2D rd2d;

    private float startTime;

    public float fasts = 1.0F;

    private float journeyLength;

    //public float speed;
    // Start is called before the first frame update
    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        //rd2d.AddForce(new Vector2(hozMovement * fasts, vertMovement * fasts));

        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * fasts;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers and pingpong the movement.
        transform.position = Vector2.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fracJourney, 1));
    }
}
