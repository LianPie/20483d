using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollison : MonoBehaviour
{
    Cube cube;

    void Awake()
    {
        cube = GetComponent<Cube>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Cube otherCube = collision.gameObject.GetComponent<Cube>();
        //check if contacted with other cube
        if (otherCube != null && cube.CubeID > otherCube.CubeID)
        {
            if (cube.CubeNumber == otherCube.CubeNumber)
            {
                Vector3 contactPoint = collision.contacts[0].point;
                //check if the cube num is less than the max number in cubeSpwaner
                if (otherCube.CubeNumber < CubeSpawner.Instance.maxCubeNumber)
                {
                    Debug.Log("Hit: "+ cube.CubeNumber);
                    //spawn result cube
                    Cube newCube = CubeSpawner.Instance.Spawn(cube.CubeNumber * 2, contactPoint + Vector3.up * 1.5f);
                    float pushForce = 2.5f;
                    newCube.CubeRb.AddForce(new Vector3(0, 3f, 1f) * pushForce, ForceMode.Impulse);

                    //add Torque
                    float randomValue = Random.Range(-20f,20f);
                    Vector3 randomDirection = Vector3.one * randomValue;
                    newCube.CubeRb.AddTorque(randomDirection);
                }
                //boom boom boom
                Collider[] surroundedCubes = Physics.OverlapSphere(contactPoint, 2f);
                float explosionForce = 400f;
                float explosionRadius = 1.5f;
                foreach (Collider coll in surroundedCubes)
                {
                    if(coll.attachedRigidbody != null)
                    {
                        coll.attachedRigidbody.AddExplosionForce(explosionForce, contactPoint, explosionRadius);
                    }
                    //explosion fx
                    Fx.Instance.playCubeExplosionFx(contactPoint, cube.CubeColor);
                    //destroy 2 cubes
                    CubeSpawner.Instance.DestroyCube(cube);
                    CubeSpawner.Instance.DestroyCube(otherCube);
                }

                
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
