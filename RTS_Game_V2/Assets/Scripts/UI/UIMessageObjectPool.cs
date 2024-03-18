using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public delegate void YesButtonDelegate();
public struct ObjectContent
{
    public string Nametext { get; set; }
    public string Description { get; set; }
    public string Message { get; set; }
    public GameObject GameObjectReq { get; }
    public YesButtonDelegate YesButtonDelegate { get; set; }

    public ObjectContent(GameObject gameobjectReq) : this()
    {
        GameObjectReq = gameobjectReq;
    }
}

public enum PopupType
{
    NAME,
    INFORMATION,
    TAKE,
    OPEN,
    DELETE,
    DROP,
    USE
}

public class UIMessageObjectPool : MonoBehaviour
{
    public static UIMessageObjectPool instance;
    private List<PopupPanel> pooledObjects;
    [SerializeField] GameObject poolPanel;
    [SerializeField] List<PopupPanel> popupPrefabsList;

    void Awake()
    {
        instance = this;
        pooledObjects = new List<PopupPanel>();
    }

    private bool CheckForEmptyObject(PopupType popupType ,out PopupPanel pooledObject)
    {
        pooledObject = null;
        foreach (var item in pooledObjects)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                if(item.MessageType == popupType)
                {
                    pooledObject = item;
                    return true;
                }
            }
        }
        return false;
    }

    public void DisplayMessage(ObjectContent content, PopupType popupType)
    {
        if (CheckForEmptyObject(popupType, out PopupPanel popupPanel))
        {
            popupPanel.PrepareMessageMenu(content);
            popupPanel.gameObject.SetActive(true);
        }
        else
        {
            if (CreateObject(popupType))
            {
                DisplayMessage(content, popupType);
            }
        }
    }

    private bool CreateObject(PopupType popupType)
    {
        if(popupPrefabsList.Any((x) => x.MessageType == popupType))
        {
            PopupPanel prefab = popupPrefabsList.First((x) => x.MessageType == popupType);

            if (prefab != null)
            {
                PopupPanel tmp = Instantiate(prefab);
                tmp.gameObject.SetActive(false);
                pooledObjects.Add(tmp);
                tmp.transform.SetParent(poolPanel.transform);
                return true;
            }
        }

        return false;
    }

}
