using UnityEngine;

public class LiftTrigger : MonoBehaviour
{
    Animator anim;

    public GameObject liftUI;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (anim != null) 
            {
              anim.SetBool("isOpen", true);
            }

            if (liftUI != null)
            {
                liftUI.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (anim != null) anim.SetBool("isOpen", false);

        if (liftUI != null)
        {
            liftUI.SetActive(false);
        }
    }
}
