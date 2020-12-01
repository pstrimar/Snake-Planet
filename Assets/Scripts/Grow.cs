using Cinemachine;
using UnityEngine;

public class Grow : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    [SerializeField] float growFactor = .025f;
    private SnakeMovement movement;
    private float originalCameraYOffset;

    private void Awake()
    {
        movement = GetComponent<SnakeMovement>();
        originalCameraYOffset = vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y;
    }

    private void OnEnable()
    {
        MainMenuUI.onPlay += HandlePlay;
    }

    private void OnDisable()
    {
        MainMenuUI.onPlay -= HandlePlay;
    }

    private void Update()
    {
        // Increases min distance between body parts over time as size increases
        movement.MinDistance += Time.deltaTime * growFactor / 4;

        // Increases scale of body parts over time
        transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;

        // Increases follow distance of camera over time as size increases
        vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y += Time.deltaTime * growFactor * 2;
    }

    // Resets camera follow distance;
    private void HandlePlay()
    {
        vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = originalCameraYOffset;
    }
}
