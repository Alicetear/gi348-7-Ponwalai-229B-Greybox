using UnityEngine;

public class AreaSound : MonoBehaviour
{
    public AudioSource areaAudio;

    void Start()
    {
        if (areaAudio != null) areaAudio.Stop(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!areaAudio.isPlaying) areaAudio.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            areaAudio.Stop();
        }
    }
}
