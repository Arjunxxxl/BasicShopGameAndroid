using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Data")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Vector3 moveDir;

    [Header("Mouse Data")]
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private Vector3 inputDir;
    [SerializeField] private float moveThreshold;

    [Space]
    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        moveDir = Vector3.zero;
        anim.SetBool("walk", false);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        MouseInput();
#endif

#if UNITY_ANDROID
        TouchInput();
#endif
    }

    private void Move()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        anim.SetBool("walk", true);
    }

    private void Rotate()
    {
        Quaternion toRotation = Quaternion.LookRotation(moveDir, transform.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10 * Time.deltaTime);
    }

    private void MouseInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            endPos = Input.mousePosition;
            inputDir = endPos - startPos;

            if(inputDir.magnitude > moveThreshold)
            {
                moveDir.x = inputDir.x;
                moveDir.y = inputDir.z;
                moveDir.z = inputDir.y;

                moveDir.Normalize();

                Move();
                Rotate();
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            startPos = Input.mousePosition;

            endPos = Input.mousePosition;
            inputDir = endPos - startPos;
            anim.SetBool("walk", false);
        }
    }

    private void TouchInput()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch(touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    endPos = touch.position;
                    inputDir = endPos - startPos;

                    if (inputDir.magnitude > moveThreshold)
                    {
                        moveDir.x = inputDir.x;
                        moveDir.y = inputDir.z;
                        moveDir.z = inputDir.y;

                        moveDir.Normalize();

                        Move();
                        Rotate();
                    }
                    break;

                case TouchPhase.Ended:
                    startPos = touch.position;

                    endPos = Input.mousePosition;
                    inputDir = endPos - startPos;
                    anim.SetBool("walk", false);
                    break;
            }
        }
    }
}
