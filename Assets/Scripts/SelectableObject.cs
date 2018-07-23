using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    [HideInInspector] public ActionType actionType;
    public int objectID;
    [HideInInspector] public int relatedObjectID;
    [HideInInspector] public int activationCount;
    [HideInInspector] public float posX;
    [HideInInspector] public float posY;
    [HideInInspector] public float posZ;
    [HideInInspector] public string failActionMessage;
    [HideInInspector] public InteractableManager manager;

    private bool _isActivated;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == gameObject.name)
                {
                    Debug.Log(gameObject.name);
                    MakeAction();
                }
            }
        }
    }

    public void InitInfo(SelectableDetail selectableInfo)
    {
        actionType = (ActionType)selectableInfo.ActionType;
        activationCount = selectableInfo.ActivationCount;
        relatedObjectID = selectableInfo.RelatedObjectID;
        posX = selectableInfo.PosX;
        posY = selectableInfo.PosY;
        posZ = selectableInfo.PosZ;
        failActionMessage = selectableInfo.FailActionMessage;
    }

    private void MakeAction()
    {
        if (activationCount == 0)
        {
            if (relatedObjectID != 0)
            {
                manager.UpdateSelectable(relatedObjectID);
            }

            switch (actionType)
            {
                case ActionType.UseObject:
                    Destroy(gameObject);
                    break;

                case ActionType.EndLevel:
                    Debug.Log("Game Won!!!");
                    break;
                case ActionType.Move:
                    var transform = gameObject.GetComponent<Transform>();
                    if (!_isActivated)
                    {
                        originalPosition = transform.position;
                        transform.position = new Vector3(originalPosition.x + posX, originalPosition.y + posY, originalPosition.z + posZ);
                        _isActivated = true;
                    }
                    else
                    {
                        transform.position = originalPosition;
                        _isActivated = false;
                    }
                    break;
                case ActionType.Rotate:
                    var transform1 = gameObject.GetComponent<Transform>();
                    if (!_isActivated)
                    {
                        originalRotation = transform1.rotation;
                        transform1.rotation = new Quaternion(originalRotation.x + posX, originalRotation.y + posY, originalRotation.z + posZ, originalRotation.w);
                        _isActivated = true;
                    }
                    else
                    {
                        transform1.rotation = originalRotation;
                        _isActivated = false; ;
                    }

                    break;
                case ActionType.ActivateAnimation:
                    //GetComponent<>

                    break;
            }
        }
        else
        {
            Debug.Log(failActionMessage);
        }


    }
}

