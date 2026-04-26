using Cinemachine;
using UnityEngine;

public class CinemachineAutoFollow : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;

    void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        FindPlayer();
    }

    // ?? Player ??????????????? Scene ????
    void OnEnable()
    {
        FindPlayer();
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
