using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite idleSprite;
    public Sprite[] animationSprites;
    public float animationTime = 0.25f;
    public bool loop = true;
    public bool idle = true;
    private int animationFrame;
    private void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable(){
        spriteRenderer.enabled = true;
    }
    private void OnDisable(){
        spriteRenderer.enabled = false;
    }
    private void Start(){
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    private void NextFrame(){
        
        animationFrame++; 
        Debug.Log("Next frame: "+ animationFrame);
        if(loop && animationFrame >= animationSprites.Length){
            animationFrame = 0;
        }
        if(idle){
            spriteRenderer.sprite = animationSprites[0];
        }else if(animationFrame >= 0 && animationFrame < animationSprites.Length){
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }
}
