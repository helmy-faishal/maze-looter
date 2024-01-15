using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    None,
    Treasure,
    ExitMaze,
}

public class Interactable : MonoBehaviour
{
    public bool isPickable = false;
    public InteractableType interactableType = InteractableType.None;
    public bool isPickedUp = false;

    public Action OnObjectInteracted;

}
