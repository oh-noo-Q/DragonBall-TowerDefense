using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum Operation
{
    ADD,
    SUBSTRACT,
    MULTIPLY,
    DIVIDE,
    NONE
}

public enum ObjectType
{
    ENEMY,
    TRAP,
    ITEM,
    DragonBall,
    SHIELD,
    KEY
}

public class BaseOperationObject : IInteractableObject
{
    [SerializeField] protected Operation operation;
    [SerializeField] protected int value;
    [SerializeField] protected TextMeshPro valueText;
    [SerializeField] protected GameObject questionMark;

    private GameManager GameManager => GameManager.Instance;

    public Operation Operation
    {
        get => operation;
        set => operation = value;
    }

    public int Value
    {
        get => value;
    }

    private void OnEnable()
    {
        //valueText.gameObject.SetActive(true);
        //UpdateValueText();
        //this.RegisterListener(EventID.OnLoadLevel, ReturnToPool);
    }

    private void OnDisable()
    {
        //this.RemoveListener(EventID.OnLoadLevel, ReturnToPool);
    }

    public override void Init(Operation operation, int value)
    {
        this.operation = operation;
        this.value = value;
        UpdateValueText();
    }

    protected virtual void UpdateValueText()
    {
        if (valueText == null) return;
        valueText.transform.localScale = Vector3.one * 2f;
        valueText.gameObject.SetActive(true);
        string operationStr = "";
        switch (operation)
        {
            case Operation.ADD:
                operationStr = "+";
                break;
            case Operation.SUBSTRACT:
                operationStr = "-";
                break;
            case Operation.MULTIPLY:
                operationStr = "X";
                break;
            case Operation.DIVIDE:
                operationStr = "÷";
                break;
            case Operation.NONE:
                valueText.gameObject.SetActive(false);
                break;
        }
        valueText.text = operationStr + value.ToString();
        if (value <= 0) valueText.gameObject.SetActive(false);
    }

    public override void InteractWithPlayer()
    {
        Player player = GameManager.Player;
        switch (operation)
        {
            case Operation.ADD:
                break;
            case Operation.SUBSTRACT:
                if (value >= player.Strength) player.Die();
                else
                {
                    GameManager.Player.SubtractStrength(value);
                    Destroy(gameObject, 1f);
                }
                break;
            case Operation.MULTIPLY:
                GameManager.Player.MultiplyStrength(value);
                break;
            case Operation.DIVIDE:
                GameManager.Player.DivideStrength(value);
                Destroy(gameObject, 1f);
                break;
            case Operation.NONE:
                break;
        }
    }

    public void HideStrength()
    {
        if (questionMark != null)
        {
            valueText.gameObject.SetActive(false);
            questionMark.SetActive(true);
        }
    }

    public void ShowStrength()
    {
        if (questionMark != null)
        {
            questionMark.SetActive(false);
            valueText.gameObject.SetActive(true);
        }
    }
}
