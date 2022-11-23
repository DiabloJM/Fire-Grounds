using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public void ChangeCanvas(GameObject next)
    {
        this.gameObject.SetActive(false);
        next.SetActive(true);
    }
}
