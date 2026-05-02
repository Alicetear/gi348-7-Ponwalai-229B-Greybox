using UnityEngine;

public class AmbientSound : MonoBehaviour
{
    public AudioClip[] ambientClips; 
    public float volume = 0.5f;
    public bool loop = true;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = loop;
        audioSource.volume = volume;

        if (ambientClips.Length > 0)
        {
            // ?????????????????????
            audioSource.clip = ambientClips[Random.Range(0, ambientClips.Length)];
            audioSource.Play();
        }
    }
}
