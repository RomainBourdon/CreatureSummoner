using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public GameObject ballToUse;
    public GameObject ballClone = null;

    public int numberOfBalls = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if((Input.GetKeyDown(KeyCode.Mouse0)) && (ballClone == null) && (numberOfBalls > 0))
        {
            numberOfBalls = 0;
            ballClone = Instantiate(ballToUse, new Vector3(transform.position.x+0.5f, transform.position.y+1, transform.position.z), transform.rotation);

            ballClone.GetComponent<Rigidbody>().AddForce(new Vector3(transform.forward.x * (800 * Time.fixedDeltaTime), 300 * Time.fixedDeltaTime, transform.forward.z * (800 * Time.fixedDeltaTime)), ForceMode.Impulse);
            ballClone.GetComponent<BallSpawnCreature>().playerCharacter = gameObject;
        }
    }
}
