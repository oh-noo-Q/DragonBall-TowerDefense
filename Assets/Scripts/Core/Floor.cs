using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Floor : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] Transform playerSpawnPosition;
    [SerializeField] public Transform[] ObjectSpawnPositions;

    [SerializeField] List<IInteractableObject> interactableObjects = new List<IInteractableObject>();
    [SerializeField] List<IInteractableObject> interactedObjects = new List<IInteractableObject>();

    [SerializeField] bool isTopFloor = false;
    [SerializeField] KeyType gate = KeyType.NULL;

    [SerializeField] private GameObject hoverEffect;
    [SerializeField] private GameObject availableEffect;

    private Collider2D collider;

    public SpriteRenderer Sprite => sprite;
    public Transform PlayerSpawnPosition => playerSpawnPosition;
    public List<IInteractableObject> InteractedObjects => interactedObjects;
    public List<IInteractableObject> InteractableObjects {
        get => interactableObjects; 
        set => interactableObjects = value;
    }

    public bool IsTopFloor {
        get => isTopFloor;
        set => isTopFloor = value;
    }

    private void OnValidate()
    {
        if (interactableObjects.Count > 3)
        {
            interactableObjects.RemoveAt(interactableObjects.Count - 1);
        }
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        collider = (Collider2D)GetComponent(typeof(Collider2D));
    }

    private void OnEnable()
    {
        hoverEffect.SetActive(false);
        availableEffect.SetActive(false);

        this.RegisterListener(EventID.OnPlayerDrag, OnPlayerDrag);
        this.RegisterListener(EventID.OnPlayerDrop, OnPlayerDrop);
    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.OnPlayerDrag, OnPlayerDrag);
        this.RemoveListener(EventID.OnPlayerDrop, OnPlayerDrop);
    }

    public void Setup(FloorData data)
    {
        SetDroppable(false);
        interactableObjects.Clear();

        gate = data.gate;
        if (gate != KeyType.NULL)
        {
            //Instantiate gate
            collider.enabled = false;
        }

        foreach(InteractableObjData interactableObjData in data.interactableObjects)
        {
            //string objID = interactableObjData.ID;
            IInteractableObject objPrefab = ObjectPrefabSO.Instance.GetObject(interactableObjData.type, interactableObjData.ID);
            //var obj = objPrefab.GetPooledInstance<BaseOperationObject>();
            var obj = Instantiate(objPrefab);
            //if (obj.GetType() == typeof(BaseOperationObject)) {
                obj.Init(interactableObjData.operation, interactableObjData.value);
            if (interactableObjData.hideStrength)
                ((BaseOperationObject)obj).HideStrength();
            //}

            interactableObjects.Add(obj);
        }

        for(int i = 0; i < interactableObjects.Count; i++)
        {
            interactableObjects[i].transform.SetParent(ObjectSpawnPositions[interactableObjects.Count - 1 - i]);
            interactableObjects[i].transform.localPosition = new Vector3(0, interactableObjects[i].transform.position.y, 0);
        }
    }

    public void SetDroppable(bool value)
    {
        collider.enabled = value;
    }

    public bool IsEmpty()
    {
        return playerSpawnPosition.childCount == 0 && IsAllObjectInteracted() && !IsRemainInteractableObject();
    }

    public bool IsRemainInteractableObject()
    {
        foreach (Transform pos in ObjectSpawnPositions)
        {
            if (pos.GetComponentsInChildren<BaseOperationObject>().Length > 0) return true;
        }
        return false;
    }

    public bool IsRemainEnemy()
    {
        List<IInteractableObject> remainEnemies = interactableObjects.FindAll(e => e.GetType() == typeof(Enemy));
        return remainEnemies.Count == 0 ? false : true;
    }

    public bool IsRemainGirl()
    {
        IInteractableObject remainPrincess = interactableObjects.Find(e => e.GetType() == typeof(Girl));
        return remainPrincess != null ? true : false;
    }

    public bool IsRemainItem()
    {
        IInteractableObject remainPrincess = interactableObjects.Find(e => e.GetType() == typeof(Item));
        return remainPrincess != null ? true : false;
    }

    public bool IsAllObjectInteracted()
    {
        return interactableObjects.Count == 0;
    }

    public float GetSpriteHeight()
    {
        return (sprite.bounds.size.y) * transform.localScale.x;
    }

    public void OnHover()
    {
        hoverEffect.SetActive(true);
        availableEffect.SetActive(false);
    }

    public void OnUnhover()
    {
        hoverEffect.SetActive(false);
        availableEffect.SetActive(true);
    }

    public void OnPlayerDrop(object param = null)
    {
        hoverEffect.SetActive(false);
        availableEffect.SetActive(false);
    }

    public void OnPlayerDrag(object param = null)
    {
        availableEffect.SetActive(collider.enabled);
    }

    //public void ReturnToPool()
    //{
    //    foreach (BaseOperationObject obj in interactableObjects)
    //    {
    //        Destroy(obj.gameObject);
    //    }
    //    interactableObjects.Clear();

    //    foreach (BaseOperationObject obj in interactedObjects)
    //    {
    //        Destroy(obj.gameObject);
    //    }
    //    interactedObjects.Clear();
    //    Destroy(gameObject);
    //}

    public void OnMouseEnter()
    {
        if (GameManager.Instance.Player.IsDragged)
        {
            OnHover();
        }
    }

    public void OnMouseExit()
    {
        if (GameManager.Instance.Player.IsDragged)
        {
            OnUnhover();
        }
    }
}
