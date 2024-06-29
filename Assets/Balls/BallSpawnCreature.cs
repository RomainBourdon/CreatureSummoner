using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BallSpawnCreature : MonoBehaviour
{

    private GameObject creatureInBall;
    private GameObject spawnedCreature;
    private GameObject playerCharacter;
    private GameObject thirdPersonCamera;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            Destroy(gameObject);

            spawnedCreature = Instantiate(creatureInBall, new Vector3 (collision.contacts[0].point.x, collision.contacts[0].point.y +1, collision.contacts[0].point.z), Quaternion.identity);

            thirdPersonCamera = Camera.main.gameObject;
            thirdPersonCamera.GetComponent<SwapController>().SwitchToCreature(spawnedCreature.gameObject);
        }
    }

    public void SetCreaturInBall(GameObject _creatureInBall)
    {
        creatureInBall = _creatureInBall;
    }

    public void SetPlayer(GameObject _player)
    {
        playerCharacter = _player;
    }
}
