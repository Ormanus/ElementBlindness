using System.Collections.Generic;
using UnityEngine;

public class StoneThrowing : MonoBehaviour
{
    public static List<ElementStoneBase> inventory = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Throw");
        }
    }
}
