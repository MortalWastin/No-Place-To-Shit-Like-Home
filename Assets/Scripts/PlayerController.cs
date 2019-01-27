using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public float speed;
    [Range(0, 1)]
    public float rotationSpeed;
    private List<PickUpObject> pickUp_Obj_List;
    private PickUpObject closestPickUpObject;

    public int currentSteps = 50;
    public float stepDistance = 1;
    public bool isMovable;
    public float totalWalk;

    public Vector3 cameraOffset;

    public bool isMoving = false;

    private void Awake()
    {
        cameraOffset = GameManager.Instance.PlayerCamera.transform.position - this.transform.position;
    }

    public void Start()
    {
        pickUp_Obj_List = new List<PickUpObject>();
        isMovable = true;
        totalWalk = 0f;
        UIManager.Instance.SetSteps(currentSteps);
    }

    void Update()
    {
        if (isMovable)
        {
            Movement();
            Interact();
        }
        else
            isMoving = false;
        Animation();
    }

    private void Interact()
    {
        if (pickUp_Obj_List.Count > 0)
        {
            ObjectsDistance();
            if (Input.GetKeyDown(PlayerSettings.Instance.Interaction))
            {
                if (closestPickUpObject.tag != "Toilet")
                {
                    GameManager.Instance.AddItem(closestPickUpObject);
                    UIManager.Instance.Interact(closestPickUpObject);
                    OnTriggerExit(closestPickUpObject.GetComponent<Collider>());
                }
                else
                    GameManager.Instance.EndGame(true);
            }
        }
    }
    private void Movement()
    {
        Vector3 deltaPosition = Vector3.zero;
        if (Input.GetKey(PlayerSettings.Instance.Up))
        {
            deltaPosition += Vector3.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKey(PlayerSettings.Instance.Down))
        {
            deltaPosition += Vector3.back * speed * Time.deltaTime;
        }

        if (Input.GetKey(PlayerSettings.Instance.Left))
        {
            deltaPosition += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(PlayerSettings.Instance.Right))
        {
            deltaPosition += Vector3.right * speed * Time.deltaTime;
        }
        transform.LookAt(Vector3.Lerp(transform.position + transform.forward, transform.position + deltaPosition, rotationSpeed));
        transform.position += deltaPosition;
        totalWalk += deltaPosition.magnitude;

        isMoving = (deltaPosition != Vector3.zero);

        if (totalWalk > stepDistance)
        {
            currentSteps -= 1;
            totalWalk = 0f;
            UIManager.Instance.SetSteps(currentSteps);
        }

        if (currentSteps <= 0)
            isMovable = false;

        GameManager.Instance.PlayerCamera.transform.position = this.transform.position + cameraOffset;
    }

    private void Animation()
    {
        if (isMoving)
        {
            //Make movement animation
            playerAnimator.SetBool("isMoving", true);

            FindObjectOfType<AudioManager>().Play("Step");
        }
        else
            playerAnimator.SetBool("isMoving", false);

    }

    private void ObjectsDistance()
    {
        float distance = 0.0f;
        foreach (PickUpObject t in pickUp_Obj_List)
        {
            float d = Vector3.Distance(this.transform.position, t.transform.position);
            if (distance > d || distance == 0)
            {
                distance = d;
                closestPickUpObject = t;
            }
        }
        UIManager.Instance.SetInteraction(closestPickUpObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PickUp"))
        {
            PickUpObject puo = other.GetComponent<PickUpObject>();
            pickUp_Obj_List.Add(puo);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PickUp"))
        {
            PickUpObject puo = other.GetComponent<PickUpObject>();
            pickUp_Obj_List.Remove(puo);
            if (pickUp_Obj_List.Count == 0)
            {
                UIManager.Instance.SetInteraction(null);
            }
        }
    }
}
