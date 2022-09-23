using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonGlint : MonoBehaviour
{
    public Transform shine;
    public float offset;
    public float speed = 1.5f;
    public float mindelay = 0.3f;
    public float maxdelay = 2f;

    public float pulse = 1.2f;
    public float pulseSpeed = 1f;
    
    void Start() {
        Shine();
        Pulse();
    }

    public void Pulse()
    {
        transform.DOScale(transform.localScale.x * pulse, pulseSpeed).SetLoops(-1, LoopType.Yoyo);
        //transform.DOScale(0.7f, 0.4f).SetLoops(-1, LoopType.Yoyo);
    }

    void Shine() {
        shine.DOLocalMoveX(offset, speed).SetEase(Ease.Linear).SetDelay(Random.Range(mindelay, maxdelay)).OnComplete(() =>
            {
                shine.DOLocalMoveX(-offset, 0);
                Shine();
            });
    }
}
