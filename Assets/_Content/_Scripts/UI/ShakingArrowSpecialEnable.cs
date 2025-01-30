using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingArrowSpecialEnable : MonoBehaviour
{
    [SerializeField] Interactable interactableToCheck;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (interactableToCheck.IsValid == true)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}
