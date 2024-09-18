using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public SpriteRenderer sprite;
    public SpriteRenderer sprite2;
    public Animator animator;

    public void Init(Sprite sp)
    {
        sprite.sprite = sp;
         sprite2.sprite = sp;
    }

    //  public ParticleSystem effect;

    public void DestroyTarget(float targetLife)
    {

        Invoke(nameof(rewwer), targetLife);
    }

    private void rewwer()
    {
        animator.SetTrigger("destroy");
        Destroy(gameObject, 0.2f);
    }
}
