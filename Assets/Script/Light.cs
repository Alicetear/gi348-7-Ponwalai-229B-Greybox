using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Light : MonoBehaviour
{
    private Image image;

    public float minAlpha = 0.5f;
    public float maxAlpha = 0.9f;
    public float flickerSpeed = 0.1f;

    private float timer = 0f;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (image == null) return;
        timer += Time.deltaTime;
        if (timer >= flickerSpeed)
        {
            Color c = image.color;
            c.a = Random.Range(minAlpha, maxAlpha);
            image.color = c;
            timer = 0f;
        }
    }
}
