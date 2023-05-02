using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float speedRotationX;
    [SerializeField] private float speedRotationZ;

    [SerializeField] private int indexType;
    [SerializeField] private List<GameObject> listAsteroidType;
 
    private GameObject asteroid;
    private new Rigidbody rigidbody;

    private void Awake()
    {
        asteroid = Instantiate(listAsteroidType[indexType], parent:transform);

        rigidbody = GetComponent<Rigidbody>();

        //asteroid.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody.angularVelocity = Vector3.left * speedRotationX 
            + Vector3.forward * speedRotationZ;

        rigidbody.angularDrag = 0f;
    }

    private void FixedUpdate()
    {
        //rigidbody.AddTorque(Vector3.left * speedRotationX, ForceMode.VelocityChange);
        //rigidbody.AddTorque(Vector3.forward * speedRotationZ, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawSphere(this.transform.position, 1);
    }
}
