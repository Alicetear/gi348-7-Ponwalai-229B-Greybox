using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinemachineAutoFollow : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private CinemachineConfiner2D confiner;

    void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        confiner = GetComponent<CinemachineConfiner2D>();
    }

    void Start()
    {
        FindPlayer();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        FindPlayer();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPlayer();

        GameObject boundaryObj = GameObject.Find("MapBoundary");
        if (boundaryObj != null && confiner != null)
        {
            confiner.m_BoundingShape2D = boundaryObj.GetComponent<PolygonCollider2D>();
            confiner.InvalidateCache();
            Debug.Log("Confiner updated for: " + scene.name);
        }
    }

    void LateUpdate()
    {
        if (vcam.Follow == null)
            FindPlayer();
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            vcam.Follow = player.transform;
    }
}
