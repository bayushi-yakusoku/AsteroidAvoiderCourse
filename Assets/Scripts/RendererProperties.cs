using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererProperties : MonoBehaviour
{
    [SerializeField] private AsteroidProperties asteroidProperties;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameInvisible()
    {
        asteroidProperties.IsVisible = false;
    }

    private void OnBecameVisible()
    {
        asteroidProperties.IsVisible = true;
    }
}
