using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{

    List<SelectableObject> selectableList;

    // Use this for initialization
    void Start()
    {
        var details = GetScenesFromXML();

        selectableList = new List<SelectableObject>();
        var selectableObjects = transform.GetComponentsInChildren<SelectableObject>();
        foreach (var selectableObject in selectableObjects)
        {
            if(!details.ContainsKey(selectableObject.objectID))
            {
                Debug.Log(string.Format("Key {0} is missing on {1}", selectableObject.objectID, selectableObject.gameObject.name));
                continue;
            }

            var childInfo = details[selectableObject.objectID];

            selectableObject.InitInfo(childInfo);

            selectableObject.manager = this;
            selectableList.Add(selectableObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateSelectable(int selectableID)
    {
        foreach (var selectable in selectableList.Where(s => s.objectID == selectableID))
        {
            selectable.activationCount--;
        }
    }

    public Dictionary<int, SelectableDetail> GetScenesFromXML()// GameController controller
    {
        var scenesDictionary = new Dictionary<int, SelectableDetail>();
        var xmlString = File.ReadAllText(GameConsts.SceneXmlPath);
        var xml = XElement.Parse(xmlString);

        var sceneListXml = xml.Element("SceneList");
        foreach (var sceneXml in sceneListXml.Elements("SelectableObject"))
        {
            var xmlDetail = new SelectableDetail();

            xmlDetail.ObjectID = int.Parse(sceneXml.Attribute("ObjectID").Value);
            xmlDetail.ActivationCount = int.Parse(sceneXml.Attribute("ActivationCount").Value);
            xmlDetail.ActionType = int.Parse(sceneXml.Attribute("ActionType").Value);
            xmlDetail.RelatedObjectID = int.Parse(sceneXml.Attribute("RelatedObjectID").Value);

            if (sceneXml.Attribute("PosX") != null)
                xmlDetail.PosX = float.Parse(sceneXml.Attribute("PosX").Value);
            if (sceneXml.Attribute("PosY") != null)
                xmlDetail.PosY = float.Parse(sceneXml.Attribute("PosY").Value);
            if (sceneXml.Attribute("PosZ") != null)
                xmlDetail.PosZ = float.Parse(sceneXml.Attribute("PosZ").Value);
            if (sceneXml.Attribute("FailActionMessage") != null)
                xmlDetail.FailActionMessage = sceneXml.Attribute("FailActionMessage").Value;

            scenesDictionary.Add(xmlDetail.ObjectID, xmlDetail);
        }

        return scenesDictionary;
    }
}

public class SelectableDetail
{
    public int ActionType { get; set; }
    public int ObjectID { get; set; }
    public int RelatedObjectID { get; set; }
    public int ActivationCount { get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
    public float PosZ { get; set; }
    public string FailActionMessage { get; set; }
}

public enum ActionType
{
    UseObject = 1,
    Move = 2,
    Rotate = 3,
    ActivateAnimation = 4,
    EndLevel = 10,

}
