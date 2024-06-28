using UnityEngine;

public class BallSpawnCreature : MonoBehaviour
{

    public GameObject creatureInBall;
    public GameObject spawnedCreature;
    public GameObject playerCharacter;
    public GameObject thirdPersonCamera;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);

            spawnedCreature = Instantiate(creatureInBall, new Vector3 (collision.contacts[0].point.x, collision.contacts[0].point.y +1, collision.contacts[0].point.z), Quaternion.identity);

            spawnedCreature.AddComponent<CharacterMovement>();
            playerCharacter.GetComponent<CharacterMovement>().enabled = false;
            thirdPersonCamera = Camera.main.gameObject;
            thirdPersonCamera.GetComponent<CameraMultiController>().ChangeFollowTarget(spawnedCreature.transform);
            thirdPersonCamera.GetComponent<SwapController>().creature = spawnedCreature.gameObject;
        }
    }
}
