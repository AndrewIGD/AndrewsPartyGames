using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourMasters_MeteorSpawner : MonoBehaviour
{
    public GameObject meteor;
    public GameObject targetPrefab;
    public GameObject corner1;
    public GameObject corner2;
    public GameObject destination1;
    public GameObject destination2;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnMeteor", 5.5f);
    }

    private void SpawnMeteor()
    {
        Invoke("SpawnMeteor", 0.5f);
        Meteor();
    }

    private void Meteor()
    {
        GameObject newMeteor = Instantiate(meteor);
        newMeteor.transform.position = new Vector2(Random.Range(corner1.transform.position.x, corner2.transform.position.x), corner1.transform.position.y);
        GameObject target = Instantiate(targetPrefab);
        target.transform.position = new Vector2(Random.Range(destination1.transform.position.x, destination2.transform.position.x), destination1.transform.position.y);
        target.transform.parent = transform;
        newMeteor.transform.parent = transform;
        newMeteor.GetComponent<SpaceShooter_Meteor>().explode = false;
        target.GetComponent<SpaceShooter_target>().isEnabled = true;
        newMeteor.GetComponent<SpaceShooter_Meteor>().target = target;
        newMeteor.GetComponent<SpaceShooter_Meteor>().speed = Random.Range(5f,15f);
        float scale = Random.Range(0.05f, 0.15f);
        newMeteor.transform.localScale = new Vector3(scale, scale, 1);
    }
}
