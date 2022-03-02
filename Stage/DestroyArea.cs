using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerExit2D (Collider2D c)
    {
        Destroy (c.gameObject);
    }
}
