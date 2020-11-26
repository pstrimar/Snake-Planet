using Cinemachine;
using UnityEngine;

public class Grow : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    public MainMenuUI mainMenuUI;
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
        if (mainMenuUI != null)
        {
            mainMenuUI.onPlay += HandlePlay;
        }
    }

    private void OnDisable()
    {
        if (mainMenuUI != null)
        {
            mainMenuUI.onPlay -= HandlePlay;
        }
    }

    private void Update()
    {
        movement.minDistance += Time.deltaTime * growFactor / 4;
        transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * growFactor;
        vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y += Time.deltaTime * growFactor * 2;
    }

    private void HandlePlay()
    {
        vCam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y = originalCameraYOffset;
    }
}
