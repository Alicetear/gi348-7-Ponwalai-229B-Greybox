using UnityEngine;

public class NoteScript : MonoBehaviour
{
    private bool noteStatus;
    public GameObject note;
    public GameObject promptUI;

   
    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip openSound;      
    public AudioClip lockedSound;

    [Header("Animation")]
    public Animator doorAnimator;

    private bool playerInRange = false;

    void Start()
    {
        if (promptUI != null) promptUI.SetActive(false);
        if (note != null) note.SetActive(false);
    }
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
            ToggleNote();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (promptUI != null) promptUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptUI != null) promptUI.SetActive(false);
            if (noteStatus) ToggleNote();
        }
    }

    public void ToggleNote()
    {
        if (note == null) return;
        noteStatus = !noteStatus;
        note.SetActive(noteStatus);
    }

    public bool GetNotStatus()
    {
        return noteStatus;
    }
}
