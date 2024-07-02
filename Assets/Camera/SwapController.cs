using UnityEngine;

public class SwapController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject player;
    private GameObject creature;

    private GameObject currentTarget;

    CameraMultiController multiController;
    void Start()
    {
        multiController = GetComponent<CameraMultiController>();
        currentTarget = player;
    }

    // Update is called once per frame
    void Update()
    {
        if (creature != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if(currentTarget.name == creature.name)
                {
                    SwitchToPlayer();
                }
                else if (currentTarget.name == player.name)
                {
                    SwitchToCreature(creature);
                }

            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if(currentTarget == creature)
                {
                    SwitchToPlayer();
                }
                Destroy(creature);
                creature = null;
                player.GetComponent<ThrowBall>().ResetBall();
            }
        }

    }

    public void SwitchToCreature(GameObject _creature)
    {
        Debug.Log("switch to creature");
        creature = _creature;
        if (creature.GetComponent<CharacterMovement>() == null)
        {
            creature.AddComponent<CharacterMovement>();
        }
        currentTarget = creature;
    }

    public void SwitchToPlayer()
    {
        Debug.Log("switch to player");
        currentTarget = player;
    }
   
    public GameObject GetCurrentTarget()
    {
        return currentTarget; 
    }

}
