using System.Collections;
using System.Collections.Generic;
using Mono.Cecil;
using Unity.VisualScripting;
using UnityEngine;

public class EntryInteractable : CameraFocus
{
    private Sprite transitionSprite;
    private SpriteRenderer spriteRenderer;
    private SoundManager soundManager;
    private bool isActivated;

    private void Awake()
    {
        transitionSprite = Resources.Load<Sprite>("Sprites/Chest2");
        spriteRenderer = GetComponent<SpriteRenderer>();
        soundManager = FindObjectOfType<SoundManager>();
        isActivated = false;
    }

    public override void Activate()
    {
        if (!isActivated)
        {
            spriteRenderer.sprite = transitionSprite;
            soundManager.PlaySoundFX(SoundFXType.CHEST);
            isActivated = true;
        }
    }
}
