using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SyncAnimation : NetworkBehaviour
{   
    public Animator animator;
    public float updateRate = 1.0f;
    
    [SyncVar]
    public bool isAiming;
    [SyncVar]
    public bool isGrounded;
    [SyncVar]
    public bool isFlyingFast;

    [SyncVar]
    public float horizontal;
    [SyncVar]
    public float vertical;

   


    void Start ()
    {
        if (isServer)
            return;

        StartCoroutine(UpdateServer());
	}
	
	void Update ()
    {
        Animate();
    }

    void Animate()
    {
        animator.SetBool("Aiming", isAiming);
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("FlyingFast", isFlyingFast);

        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }

    IEnumerator UpdateServer()
    {
        while (true)
        {
            Debug.Log("UpdateServer");

            CmdSetAiming(isAiming);
            CmdSetGrounded(isGrounded);
            CmdSetFlyingFast(isFlyingFast);
            CmdSetHorizontal(horizontal);
            CmdSetVertical(vertical);

            yield return new WaitForSeconds(updateRate);
        }
    }

    [Command]
    private void CmdSetAiming(bool value)
    {
        isAiming = value;
    }

    [Command]
    private void CmdSetGrounded(bool value)
    {
        isGrounded = value;
    }

    [Command]
    private void CmdSetFlyingFast(bool value)
    {
        isFlyingFast = value;
    }

    [Command]
    private void CmdSetHorizontal(float value)
    {
        horizontal = value;
    }

    [Command]
    private void CmdSetVertical(float value)
    {
        vertical = value;
    }

}
