using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;

public class LightFlicker : MonoBehaviour
{
    [Header("Light Settings")]
    public Light2D light2D;

    [Header("Horror Flicker Settings")]
    public float minIntensity = 0f;
    public float maxIntensity = 1.2f;
    public float offDuration = 0.3f;
    public float onDuration = 0.1f;
    public int burstCount = 3;
    public float minIdleTime = 4f;
    public float maxIdleTime = 10f;

    [Header("Sprite Settings")]
    public SpriteRenderer spriteRenderer;
    public Sprite spriteOff;
    public Sprite spriteOn;

    void Start()
    {
        if (light2D == null)
            light2D = GetComponent<Light2D>();

        StartCoroutine(HorrorFlicker());
    }

    IEnumerator HorrorFlicker()
    {
        while (true)
        {
            float idleTime = Random.Range(minIdleTime, maxIdleTime);
            SetLight(true);
            yield return new WaitForSeconds(idleTime);

            int flickers = Random.Range(2, burstCount + 1);
            for (int i = 0; i < flickers; i++)
            {
                SetLight(false);
                yield return new WaitForSeconds(Random.Range(0.2f, offDuration));

                SetLight(true);
                light2D.intensity = Random.Range(0.6f, maxIntensity);
                yield return new WaitForSeconds(Random.Range(0.1f, onDuration));
            }

            bool longBlackout = Random.value > 0.4f;
            if (longBlackout)
            {
                SetLight(false);
                yield return new WaitForSeconds(Random.Range(1f, 3f));
            }
        }
    }

    void SetLight(bool isOn)
    {
        light2D.intensity = isOn ? maxIntensity : 0f;

        if (spriteRenderer != null)
            spriteRenderer.sprite = isOn ? spriteOn : spriteOff;
    }
}