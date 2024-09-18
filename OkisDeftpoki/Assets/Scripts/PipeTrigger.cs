using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ball")
        {
            print("Øàð");
            collision.gameObject.GetComponent<Ball>().animator.SetTrigger("catch"); // (false);
            Destroy(collision.gameObject, 1f);
            ServiceLocator.GetService<ScoreManager>().ChangeValue(25);//, true);
        }
    }
}
