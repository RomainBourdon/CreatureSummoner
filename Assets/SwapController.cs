using UnityEngine;

public class SwapController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    public GameObject creature;

    CameraMultiController multiController;
    void Start()
    {
        multiController = GetComponent<CameraMultiController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (creature != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Debug.Log("right mouse button");
                if(multiController.followTarget.gameObject == creature.transform)
                {
                    multiController.ChangeFollowTarget(player.transform);
                }

                if (multiController.followTarget.gameObject == player && creature != null)
                {
                    multiController.ChangeFollowTarget(creature.transform);
                }

            }
        }

    }
}
