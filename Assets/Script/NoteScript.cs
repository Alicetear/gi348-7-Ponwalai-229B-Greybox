using UnityEngine;

public class NoteScript : MonoBehaviour
{
    private bool noteStatus;
    public GameObject note;
    public GameObject promptUI; // "?? E ?????????" — ????????????????????

    private bool playerInRange = false;

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
            // ??? note ??????????????
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
