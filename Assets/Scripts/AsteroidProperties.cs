using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AsteroidProperties : MonoBehaviour
{
    [SerializeField] public float diameter;

    private bool isVisible;
    public bool IsVisible
    {
        get => isVisible;
        set => isVisible = value;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (!IsSelected()) return;

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(this.transform.position, diameter);
    }

    private bool IsSelected()
    {
        if (Selection.transforms.Contains(transform))
            return true;

        return false;
    }
}
