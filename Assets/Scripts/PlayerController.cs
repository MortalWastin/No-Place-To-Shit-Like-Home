using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    [Range(0, 1)]
    public float rotationSpeed;
    public Transform pickUp_UI;
    private List<PickUpObject> pickUp_Obj_List;
    private PickUpObject closestPickUpObject;

    public bool isMovable;

    private void Start()
    {
        pickUp_Obj_List = new List<PickUpObject>();
        isMovable = true;
    }

    void Update()
    {
        if (isMovable)
        {
            Movement();
            Interact();
        }
    }

    private void Interact()
    {
        if (pickUp_Obj_List.Count > 0)
        {
            ObjectsDistance();
            if (Input.GetKeyDown(KeyCode.F))
            {
                GameManager.Instance.AddItem(closestPickUpObject);
                OnTriggerExit(closestPickUpObject.GetComponent<Collider>());
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
            pickUp_UI.gameObject.SetActive(true);
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
                pickUp_UI.gameObject.SetActive(false);
                UIManager.Instance.SetInteraction(null);
            }
        }
    }
}
