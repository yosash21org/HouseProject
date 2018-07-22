using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    [SerializeField]
    private int _actionType;
    public int objectID;
    public int relatedObjectID;
    public int activationCount;

    [HideInInspector]
    public InteractableManager manager;

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

    public void InitInfo()
    {

    }

    private void MakeAction()
    {
        if (activationCount == 0)
        {
            if(relatedObjectID != 0)
            {
                manager.UpdateSelectable(relatedObjectID);
            }

            switch (_actionType)
            {
                case 1:
                    Destroy(gameObject);
                    break;

                case 2:
                    Debug.Log("Game Won!!!");
                    break;
                case 3:
                    var transform = gameObject.GetComponent<Transform>();
                    if(!_isActivated)
                    {
                        originalPosition = transform.position;
                        transform.position = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z + 1f);
                        _isActivated = true;
                    }
                    else
                    {
                        transform.position = originalPosition;
                        _isActivated = false;
                    }
                    break;
                case 4:
                    var transform1 = gameObject.GetComponent<Transform>();
                    if(!_isActivated)
                    {
                        originalRotation = transform1.rotation;
                        transform1.rotation = new Quaternion(originalRotation.x, originalRotation.y + 90, originalRotation.z, originalRotation.w);
                        _isActivated = true;
                    }
                    else
                    {
                        transform1.rotation = originalRotation;
                        _isActivated = false; ;
                    }
                 
                    break;
            }
        }
        else
        {
            Debug.Log("Action Cannot Be Done!!");
        }
        

    }
}

