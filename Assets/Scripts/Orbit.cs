using UnityEngine;
using Cinemachine;

public class Orbit : MonoBehaviour
{
    public float speed = 10f;

    private CinemachineOrbitalTransposer orbital;

    void Start()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
        if (vcam != null)
            orbital = vcam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    void Update()
    {
        if (orbital != null)
            orbital.m_XAxis.Value += Time.deltaTime * speed;
    }
}
