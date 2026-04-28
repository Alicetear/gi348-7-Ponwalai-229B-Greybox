using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class LightTrigger : MonoBehaviour
{
    public Light2D globalLight;
    public Light2D flashlight;
    public KeyCode toggleKey = KeyCode.F;
    public AudioClip lightOffSound;
    public AudioClip flashlightToggleSound;
    public GameObject blockObject;

    [Header("UI")]
    public float messageDuration = 5f;

    private GameObject player;
    private bool lightsOff = false;
    private AudioSource audioSource;
    private Text hintText;

    void Start()
    {
        if (flashlight != null)
            flashlight.enabled = false;

        player = GameObject.FindWithTag("Player");
        audioSource = gameObject.AddComponent<AudioSource>();

        CreateHintUI();
    }

    void CreateHintUI()
    {
        GameObject canvasObj = new GameObject("HintCanvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;
        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        GameObject textObj = new GameObject("HintText");
        textObj.transform.SetParent(canvasObj.transform, false);

        hintText = textObj.AddComponent<Text>();
        hintText.text = "";
        hintText.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        hintText.fontSize = 24;
        hintText.color = Color.white;
        hintText.alignment = TextAnchor.MiddleCenter;

        RectTransform rt = textObj.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0.1f);
        rt.anchorMax = new Vector2(1, 0.2f);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (globalLight != null)
                globalLight.intensity = 0f;

            if (flashlight != null)
                flashlight.enabled = false;

            if (blockObject != null)
                blockObject.SetActive(true);

            lightsOff = true;

            if (lightOffSound != null)
                audioSource.PlayOneShot(lightOffSound);

            if (hintText != null)
            {
                hintText.text = $"Press [F] to turn the flashlight on/off.";
                Invoke(nameof(HideHint), messageDuration);
            }
            Destroy(GetComponent<Collider2D>());
        }
    }

    void HideHint()
    {
        if (hintText != null)
            hintText.text = "";
    }

    void Update()
    {
        if (!lightsOff || player == null) return;

        if (Input.GetKeyDown(toggleKey))
        {
            if (flashlight != null)
            {
                flashlight.enabled = !flashlight.enabled;

                if (flashlightToggleSound != null)
                    audioSource.PlayOneShot(flashlightToggleSound);
            }
        }

        if (flashlight == null || !flashlight.enabled) return;

        flashlight.transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            flashlight.transform.position.z
        );

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            float angle = Mathf.Atan2(moveY, moveX) * Mathf.Rad2Deg;
            flashlight.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
        }
    }
}
