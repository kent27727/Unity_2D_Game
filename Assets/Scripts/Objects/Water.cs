using UnityEngine;

public class Water : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<AudioSource>()?.Play();
    }
}
