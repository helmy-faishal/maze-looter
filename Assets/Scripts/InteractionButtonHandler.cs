using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionButtonHandler : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        
        if (playerManager == null)
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }
    }

    private void Start()
    {
        button.onClick.AddListener(ProcessInteraction);
    }

    void ProcessInteraction()
    {
        if (playerManager.objectInteracted == null)
        {
            return;
        }

        playerManager.objectInteracted.OnObjectInteracted?.Invoke();
    }
}
