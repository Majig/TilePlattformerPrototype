using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    // config fields
    [SerializeField] AudioClip myAudioClip;
    [SerializeField] int pointsWorth = 100;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.GetComponent<Player>() != null)
        {
            AudioSource.PlayClipAtPoint(myAudioClip, Camera.main.transform.position);
            FindObjectOfType<GameSession>().AddToScore(pointsWorth);
            Destroy(gameObject);
        }
    }
}
