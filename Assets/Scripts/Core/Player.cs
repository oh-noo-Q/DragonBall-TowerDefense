using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Spine.Unity;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Floor currentFloor;
    [SerializeField] int strength = 21;
    [SerializeField] TextMeshPro strengthText;

    [SerializeField] TextMeshPro poisonedText;
    [SerializeField] Animator animator;

    [SerializeField] private Collider2D collider;
    private bool isDragged = false;

    [SerializeField] Weapon weapon;

    //Poisoned Effect
    [SerializeField] int maxPosionedTime = 4;
    [SerializeField] private Poison poison = null;
    private int poisonedTime = 0;
    private Vector3 poisonedStartPosition;
    private Color poisonedStartColor;
    private Sequence poisonEffect;

    private Vector3 spriteDragStartPosition;
    private Vector3 mouseDragStartPosition;

    //Attack anim
    int maxTypeAttack = 9;
    int attackAnim;
    public IInteractableObject currentObjInteract;
    int dieEnemyAnim = 1;

    [SerializeField] Transform coinTrans;
    [SerializeField] Coin coinPrf;

    public TextMeshPro StrengthText => strengthText;
    public bool IsDragged => isDragged;
    public Weapon Weapon
    {
        get => weapon;
        set => weapon = value;
    }
    public int Strength
    {
        get => strength;
        set
        {
            int oldStrength = strength;
            strength = value;
            if (gameObject.activeInHierarchy)
            {
                StartCoroutine(AnimateStrengthText(oldStrength));
            } else
            {
                strengthText.text = strength.ToString();
            }
        }
    }

    public Poison Poison
    {
        get => poison;
        set => poison = value;
    }

    private void Start()
    {
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        //currentFloor = transform.parent.GetComponent<Floor>();
        camera = GameManager.Instance.MainCamera;
        poisonedStartPosition = poisonedText.transform.localPosition;
        poisonedStartColor = poisonedText.color;
        //SetCharacterState("test");
    }

    private void OnEnable()
    {
        UpdateStrengthText(strength);
        //strengthText.gameObject.SetActive(true);
        poison = null;
        poisonedTime = 0;
    }
    public void Attack()
    {
        animator.SetBool("Block", false);
        attackAnim = Random.Range(0, maxTypeAttack) + 1;
        animator.SetInteger("Attack", attackAnim);
        if(attackAnim != 8 && attackAnim != 9)
            ((Enemy)currentObjInteract).GetHitAnim(attackAnim);
    }

    public void EndAttack()
    {
        ((Enemy)currentObjInteract).EndAnim();
    }

    public void GetHitEnemy()
    {
        ((Enemy)currentObjInteract).GetHitAnim(attackAnim);
    }

    public void FinalHit()
    {
        collider.enabled = true;
        if(attackAnim == 4 || attackAnim == 6 || attackAnim == 8)
            ((Enemy)currentObjInteract).GetDieAnim(2);
        else ((Enemy)currentObjInteract).GetDieAnim(1);
        AddStrength(((BaseOperationObject)currentObjInteract).Value);
    }

    public void ActiveCollider()
    {
        collider.enabled = true;
    }

    public void GetOnceHit()
    {
        ((Enemy)currentObjInteract).GetOnceHit();
    }

    public void ResetAttack()
    {
        animator.SetInteger("Attack", 0);
    }

    //Sound
    public void Punch01()
    {
        SoundManager.instance.PlaySingle(SoundType.Punch);
    }

    public void Punch02()
    {
        SoundManager.instance.PlaySingle(SoundType.Punch02);
    }

    public void Punch03()
    {
        SoundManager.instance.PlaySingle(SoundType.Punch03);
    }

    public void Punch04()
    {
        SoundManager.instance.PlaySingle(SoundType.Punch04);
    }

    public void CritPunch()
    {
        SoundManager.instance.PlaySingle(SoundType.CritPunch);
    }

    public void CritDap()
    {
        SoundManager.instance.PlaySingle(SoundType.CritDap);
    }
    public void Kame()
    {
        SoundManager.instance.PlaySingle(SoundType.Kame);
        EventDispatcher.Instance.PostEvent(EventID.Shaking, null);
    }

    public void MiniKame()
    {
        SoundManager.instance.PlaySingle(SoundType.MiniKame);
    }

    public void FlyKame()
    {
        SoundManager.instance.PlaySingle(SoundType.FlyKame);
    }

    public void Fly()
    {
        animator.SetBool("Fly", true);
    }


    public void StopFly()
    {
        animator.SetBool("Fly", false);
    }

    private void UpdateStrengthText(int strength)
    {
        strengthText.text = strength.ToString();
    }

    private IEnumerator AnimateStrengthText(int oldStrength)
    {
        float timeStamp = 0f;
        float time = 0.5f;
        float rate = 0f;

        int strengthText = oldStrength;
        while (timeStamp <= time)
        {
            timeStamp += Time.deltaTime;
            rate = timeStamp / time;

            strengthText = (int)Mathf.Lerp(oldStrength, strength, rate);
            UpdateStrengthText(strengthText);
            yield return null;
        }
    }

    public void AddStrength(int value)
    {
        Strength += value;
        //UpdateStrengthText();
    }

    public void SubtractStrength(int value)
    {
        if (Strength <= value)
        {
            Die();
            return;
        }
        Strength -= value;
        //UpdateStrengthText();
    }

    public void MultiplyStrength(int value)
    {
        Strength *= value;
        //UpdateStrengthText();
    }

    public void DivideStrength(int value)
    {
        Strength /= value;
        //UpdateStrengthText();
    }

    public void Die()
    {
        strengthText.gameObject.SetActive(false);
        animator.SetBool("Die", true);
        if (UserData.NumberPeanut > 0)
        {
            EventDispatcher.Instance.PostEvent(EventID.OnShowRevive);
        }
        else
        {
            this.PostEvent(EventID.OnLoseLevel);
            transform.SetParent(null);
        }
    }

    public void Block()
    {
        animator.SetBool("Block", true);
    }

    public void GetHit()
    {
        animator.SetBool("GetHit", true);
    }

    public void EndGetHit()
    {
        animator.SetBool("GetHit", false);
    }

    public void Revive()
    {
        ActiveCollider();
        animator.SetBool("Die", false);
        strengthText.gameObject.SetActive(true);
        int oldStrength = strength;
        strength = (int)(strength * 1.2f);
        StartCoroutine(AnimateStrengthText(oldStrength));
    }

    public void Merge(int value)
    {
        int oldStrength = strength;
        strength = strength * 2 + value;
        StartCoroutine(AnimateStrengthText(oldStrength));
    }

    private Vector3 oldPosBeforeSlap;
    public void Slap()
    {
        oldPosBeforeSlap = transform.position;
        animator.SetTrigger("Slap");
    }

    void EndSlap()
    {
        collider.enabled = true;
        transform.DOLocalMove(oldPosBeforeSlap, 0.3f);
    }

    public void OnDrop()
    {
        if (GameManager.Instance.GameMode != GameMode.PLAYING) return;
        Vector2 ray = camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Floor"))
            {
                Floor floor = hit.collider.GetComponent<Floor>();
                if (floor != null)
                {
                    Floor previousFloor = currentFloor;
                    floor.OnPlayerDrop();
                    currentFloor = floor;
                    transform.SetParent(currentFloor.PlayerSpawnPosition);
                    transform.localPosition = Vector3.zero;

                    if (previousFloor != null) EventDispatcher.Instance.PostEvent(EventID.OnPlayerDrop, previousFloor);

                    int objCount = currentFloor.InteractableObjects.Count;
                    if (objCount > 0)
                    {
                        collider.enabled = false;
                        Poisoned();
                        IInteractableObject obj = currentFloor.InteractableObjects[0];
                        currentObjInteract = obj;
                        if(obj.transform.TryGetComponent<Trap>(out Trap trap))
                        {
                            transform.DOMoveX(trap.transform.position.x - 0.5f, 0.3f);
                        }
                        else if (Vector3.Distance(obj.transform.position, transform.position) > 1)
                            transform.DOMoveX(obj.transform.position.x - 1.5f, 0.3f);

                        StartCoroutine(DelayToInteract(obj));
                    }
                }

            }
            else if(hit.collider.CompareTag("Room"))
            {
                Floor room = hit.collider.GetComponent<Floor>();
                if (room != null)
                {
                    transform.SetParent(room.ObjectSpawnPositions[1]);
                    transform.localPosition = Vector3.zero;
                    if (room.HaveEarn)
                    {
                        earnCoinCor = StartCoroutine(StartEarnCoin());
                        Dance(0);
                    }
                    else
                    {
                        if(earnCoinCor != null) StopCoroutine(earnCoinCor);
                    }
                }
            }
            else
            {
                transform.localPosition = Vector3.zero;
            }
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
        this.PostEvent(EventID.OnPlayerDrop);
    }

    Coroutine earnCoinCor;
    float timeToEarn = 0;
    IEnumerator StartEarnCoin()
    {
        while(timeToEarn < 5f)
        {
            timeToEarn += Time.deltaTime;
            yield return null;
        }
        UserData.CurrentCoin += 5;
        CollectCoin();
        timeToEarn = 0;
        earnCoinCor = StartCoroutine(StartEarnCoin());
    }

    IEnumerator DelayToInteract(IInteractableObject obj)
    {
        yield return ExtensionClass.GetWaitForSeconds(1f);
        if (obj.transform.TryGetComponent<Enemy>(out Enemy enemy))
        {
            obj.InteractWithPlayer();
            if (!enemy.IsWin)
            {
                currentFloor.InteractableObjects.Remove(obj);
                currentFloor.InteractedObjects.Add(obj);
            }
        }
        else
        {
            currentFloor.InteractableObjects.Remove(obj);
            currentFloor.InteractedObjects.Add(obj);
            obj.InteractWithPlayer();
        }
    }

    private void Poisoned()
    {
        if (poison == null) return;
        poison.InteractWithPlayer();
        ShowEffectText(poison.Value);
        poisonedTime++;
        if (poisonedTime >= maxPosionedTime - 1)
        {
            poisonedTime = 0;
            poison = null;
        }
    }

    public void ShowEffectText(int value)
    {
        poisonEffect.Kill();
        poisonedText.transform.localPosition = poisonedStartPosition;
        poisonedText.color = poisonedStartColor;
        poisonedText.text = "-" + value.ToString();

        //textEffect.color = typeUpgrade ? colorUpgrade : colorSell;

        poisonedText.gameObject.SetActive(true);

        poisonEffect = DOTween.Sequence();

        poisonEffect.Append(poisonedText.transform.DOMoveY(poisonedText.transform.position.y + 1f, 1.5f));
        poisonEffect.Join(poisonedText.DOFade(0.25f, 1f));
        poisonEffect.OnComplete(() =>
        {
            poisonedText.gameObject.SetActive(false);
            poisonedText.transform.localPosition = poisonedStartPosition;
        });

    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.GameMode != GameMode.PLAYING) return;
        isDragged = true;
        collider.enabled = false;

        mouseDragStartPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        spriteDragStartPosition = transform.position;
        this.PostEvent(EventID.OnPlayerDrag);
    }

    private void OnMouseDrag()
    {
        if (isDragged)
        {
            transform.position = spriteDragStartPosition + (camera.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPosition);
            animator.SetInteger("Dance", 0);
            if(earnCoinCor != null) StopCoroutine(earnCoinCor);
            animator.SetBool("Movie", true);
            animator.SetBool("Down", false);
            //Vector2 ray = camera.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
            //if (hit.collider != null && hit.collider.CompareTag("Floor"))
            //{

            //}
        }

    }

    private void OnMouseUp()
    {
        OnDrop();
        animator.SetBool("Down", true);
        animator.SetBool("Movie", false);
        if(currentObjInteract == null) 
            collider.enabled = true;
        isDragged = false;
    }

    public void Dance(int type)
    {
        if (type == 0)
        {
            int ran = Random.Range(1, 3);
            animator.SetInteger("Dance", ran);
        }
        else
            animator.SetInteger("Dance", type);
    }

    public void CollectCoin()
    {
        var coinClone = coinPrf.GetPooledInstance<Coin>();
        coinClone.transform.SetParent(transform);
        coinClone.transform.position = coinTrans.position;
        Vector3 oldPos = coinTrans.position;

        coinClone.transform.DOMoveY(coinTrans.position.y + 1f, 0.5f).OnComplete(() =>
        {
            coinClone.ReturnToPool();
        });
        coinClone.transform.DORotate(new Vector3(0, 360, 0) , 0.5f, RotateMode.Fast);
    }

    public void RandomSkin()
    {
        CharacterDictionary characterDic = CharacterManagerDataSO.Instance.characterDic;
        int id = Random.Range(0, characterDic.Count);
        CharacterDataSO data = characterDic[(Character)id];

    }

    public void ShowStrength(bool isShow)
    {
        if(isShow)
            strengthText.gameObject.SetActive(true);
        else strengthText.gameObject.SetActive(false);
    }
}
