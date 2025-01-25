using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_AnimationManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private float timeBetweenFrames;
    private int _spriteIndex = 0;
    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= timeBetweenFrames)
        {
            AnimateSprite();
            _timer = 0f;
        }
    }
    
    
    private void AnimateSprite()
    {
        _spriteIndex++;
        if (_spriteIndex >= sprites.Length)
        {
            _spriteIndex = 0;
        }

        sr.sprite = sprites[_spriteIndex];
    }
}
