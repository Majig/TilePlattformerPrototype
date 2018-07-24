using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    // Config Fields
    [SerializeField] float runSpeed;

    // Cached components
    Rigidbody2D myRigidbody;
    CapsuleCollider2D myBody;
    BoxCollider2D myFeeler;

	// Use this for initialization
	void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myBody = GetComponent<CapsuleCollider2D>();
        myFeeler = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update ()
    {
        myRigidbody.velocity = new Vector2(runSpeed, 0f);
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
        runSpeed *= -1;
    }
}
