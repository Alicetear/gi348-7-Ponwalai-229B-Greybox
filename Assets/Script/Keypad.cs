using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Ans;
    [SerializeField] private Animator Door;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip correctSound; 
    [SerializeField] private AudioClip wrongSound;

    [SerializeField] private Color normalColor = new Color(0f, 0f, 0f, 1f);
    [SerializeField] private Color correctColor = Color.green;
    [SerializeField] private Color wrongColor = Color.red;

    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 5f;

    private bool isResultShown = false;

    private Vector3 originalPos;

    private string Answer = "1234";

    public void Number(int number)
    {
        if (audioSource && clickSound)
            audioSource.PlayOneShot(clickSound);

        if (isResultShown)
        {
            Ans.text = "";
            Ans.color = normalColor;
            isResultShown = false;
        }


        if (Ans.text.Length >= Answer.Length) return;

        Ans.text += number.ToString();
        Debug.Log($"Color: {Ans.color}, Alpha: {Ans.color.a}");
    }

    public void Execute()
    {
        if (audioSource && clickSound)
            audioSource.PlayOneShot(clickSound);

        if (Ans.text == Answer)
        {
            Ans.text = "Correct";
            Ans.color = correctColor;
            if (audioSource && correctSound)
                audioSource.PlayOneShot(correctSound);
            Door.SetBool("Open", true);
            StartCoroutine("StopDoor");
        }
        else
        {
            Ans.text = "Invalid";
            Ans.color = wrongColor;
            if (audioSource && wrongSound)
                audioSource.PlayOneShot(wrongSound);
            originalPos = Ans.rectTransform.localPosition;
            StopAllCoroutines();
            StartCoroutine(Shake());
        }

        isResultShown = true; 
    }

    IEnumerator StopDoor()
    {
        yield return new WaitForSeconds(0.5f);
        Door.SetBool("Open", false);
        Door.enabled = false;
    }

    IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            Ans.rectTransform.localPosition = originalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        Ans.rectTransform.localPosition = originalPos;
    }
}
