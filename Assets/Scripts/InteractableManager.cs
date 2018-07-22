using System.Collections;
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
        selectableList = new List<SelectableObject>();
        var children = transform.GetComponentsInChildren<SelectableObject>();
        foreach (var child in children)
        {
            selectableList.Add(child);
            child.manager = this;
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

    public Dictionary<int, XmlDetail> GetScenesFromXML()// GameController controller
    {
        var scenesDictionary = new Dictionary<int, XmlDetail>();
        var xmlString = File.ReadAllText(GameConsts.SceneXmlPath);
        var xml = XElement.Parse(xmlString);

        var sceneListXml = xml.Element("SceneList");
        foreach (var sceneXml in sceneListXml.Elements("SelectableObject"))
        {
            var xmlDetail = new XmlDetail();
            xmlDetail.ObjectID = int.Parse(sceneXml.Attribute("ObjectID").Value);
            xmlDetail.ActivationCount = int.Parse(sceneXml.Attribute("ActivationCount").Value);
            xmlDetail.ActionType = int.Parse(sceneXml.Attribute("ActionType").Value);
            xmlDetail.RelatedObjectID = int.Parse(sceneXml.Attribute("RelatedObjectID").Value);
            xmlDetail.PosX = float.Parse(sceneXml.Attribute("PosX").Value);
            xmlDetail.PosY = float.Parse(sceneXml.Attribute("PosY").Value);
            xmlDetail.PosZ = float.Parse(sceneXml.Attribute("PosZ").Value);

            scenesDictionary.Add(xmlDetail.ObjectID, xmlDetail);
        }

        return scenesDictionary;
    }
}

public class XmlDetail
{
    public int ActionType { get; set; }
    public int ObjectID { get; set; }
    public int RelatedObjectID { get; set; }
    public int ActivationCount { get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
    public float PosZ { get; set; }
}

public enum ActionType
{
    Destroy = 1,
    EndLevel = 2,
}
