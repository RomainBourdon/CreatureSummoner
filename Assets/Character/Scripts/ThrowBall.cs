using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    [Header("What Ball")]
    public GameObject ballToUse;

    [Header("What Creature")]
    public GameObject creatureToSpawn;
    private GameObject ballClone = null;

    private bool hasThrown = false;
    private bool ballUsed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0)) && (!ballUsed))
        {
            Debug.Log("throwing");
            ballUsed = true;
            hasThrown = true;
            ballClone = Instantiate(ballToUse, new Vector3(transform.position.x + 0.5f, transform.position.y + 1, transform.position.z), transform.rotation); 
            ballClone.GetComponent<BallSpawnCreature>().SetPlayer(gameObject);
            ballClone.GetComponent<BallSpawnCreature>().SetCreaturInBall(creatureToSpawn);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasThrown) 
        { 
            ballClone.GetComponent<Rigidbody>().AddForce(new Vector3(Camera.main.transform.forward.x * (800 * Time.fixedDeltaTime), 300 * Time.fixedDeltaTime, Camera.main.transform.forward.z * (800 * Time.fixedDeltaTime)), ForceMode.Impulse);
            
            hasThrown = false;
        }
    }

    public void ResetBall()
    {
        ballClone = null;
        ballUsed = false;
    }
}
