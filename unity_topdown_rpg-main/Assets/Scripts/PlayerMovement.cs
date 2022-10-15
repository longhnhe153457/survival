using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed;

    public Rigidbody2D rb;
	[SerializeField] Transform hand;
    int redirect = 1;
    Vector2 movement;
    GameObject player;
    Vector3 scale;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update() {
        MovementInput();
		RotateHand();
    }

    private void FixedUpdate() {
        rb.velocity = movement * moveSpeed;
    }

	void RotateHand() {
		float angle = Utility.AngleTowardsMouse(hand.position);
        if (angle > -90 && angle < 90 && redirect < 0)
        {
            Flip();
            angle = angle < -45 ? -45 : (angle > 45 ? 45 : angle);
        } else if ((angle < -90 || angle > 90) && redirect > 0) {
            Flip();
            angle = -( angle < -45 ? -45 : (angle > 45 ? 45 : angle));
            angle = 45;
        } else {
            angle = angle < -45 ? -45 : (angle > 45 ? 45 : angle);
        }
        
		hand.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
	}

    void MovementInput () {
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");
        if (mx >= 0 && redirect > 0)
        {
            movement = new Vector2(mx, my).normalized;
        }
        else if (mx <= 0 && redirect < 0)
        {
            movement = new Vector2(mx, my).normalized;
        }
        else {
            Flip();
            movement = new Vector2(mx, my).normalized;
        }
    }

    void Flip()
    {
        scale = player.transform.localScale;
        scale.x *= -1;
        player.transform.localScale = scale;
        redirect *= -1;
    }
}
