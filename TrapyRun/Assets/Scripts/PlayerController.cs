using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    // Public Variables
    [HideInInspector] public bool gameOver = false;
    [HideInInspector] public static Action ChangeCameraAngle;
    [HideInInspector] public static  Action MoveHelicopter;

    // Private Variables
    Animator animator;
    Rigidbody rb;
    Collider boxCollider;

    Rigidbody[] rigidbodies;
    Collider[] colliders;

    float firstFingerX, lastFingerX;
    float minYPos = -15;

    #endregion

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();

        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();

        setCollidersEnabled(false);
        setRigidbodiesKinematic(true);

        rb.isKinematic = false;
        boxCollider.enabled = true;
    }

    void Update()
    {
        MoveHorizontal();
        checkIsFloating();
        checkIsFall();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "FinishLine")
        {
            Camera.main.transform.Find("Confetti_01").gameObject.SetActive(true);
            Camera.main.transform.Find("Confetti_02").gameObject.SetActive(true);
            ChangeCameraAngle();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "HelicopterStop")
        {
            MoveScript ms = GetComponent<MoveScript>();
            PlayerController pc = GetComponent<PlayerController>();

            if (ms != null)
            {
                ms.enabled = false;
                animator.SetBool("isIdle", true);

                MoveHelicopter();

                StartCoroutine(DestroyPlayer(1.5f));
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (gameOver) return;

        if (collision.gameObject.CompareTag("GroundCube"))
        {
            if (collision.gameObject.GetComponent<CubeScript>() != null)
                collision.gameObject.GetComponent<CubeScript>().fall();
        }
    }

    public void die()
    {
        GetComponent<MoveScript>().enabled = false;
        GetComponent<PlayerController>().enabled = false;

        activateRagdoll();

        gameOver = true;
    }

    float getMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = transform.position.y + 5;

        return Camera.main.ScreenToWorldPoint(mousePos).x;
    }

    IEnumerator DestroyPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void MoveHorizontal()
    {
        if (Input.GetMouseButtonDown(0)) firstFingerX = getMousePos();
        else if (Input.GetMouseButton(0))
        {
            lastFingerX = getMousePos();

            float dif = lastFingerX - firstFingerX;

            transform.rotation = Quaternion.Euler(0, dif * 50, 0);
            transform.position += new Vector3(dif, 0, 0) * 0.6f;

            firstFingerX = lastFingerX;
        }
    }

    void setCollidersEnabled(bool enabled)
    {
        foreach (Collider item in colliders)
        {
            item.enabled = enabled;
        }
    }

    void setRigidbodiesKinematic(bool kinematic)
    {
        foreach (Rigidbody item in rigidbodies)
        {
            item.isKinematic = kinematic;
        }
    }

    void activateRagdoll()
    {
        boxCollider.enabled = false;
        rb.isKinematic = true;
        animator.enabled = false;

        setCollidersEnabled(true);
        setRigidbodiesKinematic(false);
    }

    void checkIsFloating()
    {
        if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 1))
        {
            if (hitInfo.collider == null)
            {
                if (!animator.GetBool("isFloating")) animator.SetBool("isFloating", true);
            }
        }
        else
        {
            if (animator.GetBool("isFloating")) animator.SetBool("isFloating", false);
        }
    }

    void checkIsFall()
    {
        if (transform.position.y <= minYPos)
            die();
    }
}
