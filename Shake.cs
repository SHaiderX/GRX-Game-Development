using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Animator cameraAnim;

    public void CamShake(){
        cameraAnim.SetTrigger("Shake1");
    }
}
