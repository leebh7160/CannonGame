using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box : MonoBehaviour
{
    GameManager gameManager;
    public GameManager GameManager
    {
        set => gameManager = value;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject instantObject = collision.gameObject;

        int objectNumber = -1;

        if (instantObject.tag != "box")
            return;

        if (instantObject != null)
        {
            objectNumber = int.Parse(instantObject.name.Substring(instantObject.name.Length - 1, 1));

            if (instantObject.name.Contains("X"))
                gameManager.BoxDownCheck("X", objectNumber);
            if (instantObject.name.Contains("Y"))
                gameManager.BoxDownCheck("Y", objectNumber);

        }
    }
}
