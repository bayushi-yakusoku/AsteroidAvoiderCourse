using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float speedRotationX;
    [SerializeField] private float speedRotationZ;

    [SerializeField] private int indexType;
    [SerializeField] private List<GameObject> listAsteroidType;

    [SerializeField] private bool screenWrapAround;

    [Space(10)]
    [SerializeField] private AsteroidInfo runTimeInfo;

    private GameObject asteroid;
    private AsteroidProperties asteroidProperties;

    private new Rigidbody rigidbody;

    private Camera cam;

    private void Awake()
    {
        asteroid = Instantiate(listAsteroidType[indexType], parent:transform);
        asteroidProperties = asteroid.GetComponentInChildren<AsteroidProperties>();

        rigidbody = GetComponent<Rigidbody>();

        //asteroid.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

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
        UpdateInfo();
        ScreenWrapAround();
    }

    private void ScreenWrapAround()
    {
        if (!screenWrapAround) return;

        //if (asteroidProperties.IsVisible) return;
        //Debug.LogError("invisible!");

        Vector3 newPosition = transform.position;

        Vector3 viewportPosition = cam.WorldToViewportPoint(newPosition);

        if (viewportPosition.x < 0)
            newPosition.x = -1f * (newPosition.x + 0.1f);
        else if (viewportPosition.x > 1)
            newPosition.x = -1f * (newPosition.x - 0.1f);

        if (viewportPosition.y < 0)
            newPosition.y = -1f * (newPosition.y + 0.1f);
        else if (viewportPosition.y > 1)
            newPosition.y = -1f * (newPosition.y - 0.1f);

        transform.position = newPosition;
    }

    private void UpdateInfo()
    {
        runTimeInfo.isVisible = asteroidProperties.IsVisible;
        runTimeInfo.diameter = asteroidProperties.diameter;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawSphere(this.transform.position, 1);
    }

    //private void OnBecameInvisible()
    //{
    //    Debug.Log("Invisible");
    //}
}

[System.Serializable]
class AsteroidInfo
{
    [ReadOnly] public bool isVisible;
    [ReadOnly] public float diameter;

}
