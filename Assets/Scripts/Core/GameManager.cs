using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelDataSO levelData;
    [SerializeField] int maxLevel = 2;

    [SerializeField] Camera mainCamera;
    [SerializeField] Player player;
    [SerializeField] List<Tower> towerList = new List<Tower>();
    [SerializeField] Tower currentMainTower;
    [SerializeField] Tower enemyTower;
    [SerializeField] Enemy giantBoss;
    [SerializeField] Princess princess;
    [SerializeField] CastleController castle;
    [SerializeField] ShopController shop;

    [SerializeField] Tower towerPrefab;

    [SerializeField] Transform levelMap;

    private GameMode gameMode = GameMode.MENU;
    private Vector3 camMainMenuPos = new Vector3(-64, -2.5f, 0);

    public Camera MainCamera => mainCamera;
    public List<Tower> TowerList => towerList;
    public Player Player => player;
    public GameMode GameMode {
        get => gameMode;
        set => gameMode = value;
    }

    public void SetMainPlayer(Player main)
    {
        player = main;
        player.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    public Tower CurrentMainTower
    {
        get => currentMainTower;
        set
        {
            currentMainTower.SetInteractable(false);
            currentMainTower = value;
            CheckNextTower();
        }
    }

    public Princess Princess
    {
        get => princess;
        set => princess = value;
    }

    private void Start()
    {

    }


    private GameManager()
    {

    }

    private void Awake()
    {
        Instance = this;
        mainCamera.transform.localPosition = camMainMenuPos;
        mainCamera.orthographicSize = 10f;
        // Set up user data
        if (UserData.DragonBall.Count <= 0) UserData.NewDragonBall();
        if (UserData.CharacterMerge.Count <= 0 ||
            UserData.CharacterMerge.Count != CharacterManagerDataSO.Instance.characterDic.Count)
            UserData.NewCharacterMerge(CharacterManagerDataSO.Instance.characterDic.Count);

        this.RegisterListener(EventID.OnWinLevel, Win);
        this.RegisterListener(EventID.OnLoadLevel, LoadLevel);
        this.RegisterListener(EventID.OnPlayerInteract, CheckTowerDone);
    }

    private void OnDestroy()
    {
        this.RemoveListener(EventID.OnWinLevel, Win);
        this.RemoveListener(EventID.OnLoadLevel, LoadLevel);
        this.RemoveListener(EventID.OnPlayerInteract, CheckTowerDone);
    }

    public static GameManager Instance { get; private set; }

    private void CheckNextTower()
    {
        int enemyTowerIndex = towerList.IndexOf(CurrentMainTower) + 1;
        if (enemyTowerIndex < towerList.Count)
        {
            enemyTower = towerList[enemyTowerIndex];
            enemyTower.SetInteractable(true);
            mainCamera.transform.DOMoveX((enemyTower.transform.position.x + currentMainTower.transform.position.x) / 2f, 1f);
            return;
        }

        if (giantBoss != null)
        {
            StartCoroutine(CameraZoomIn(1f));
            player.currentObjInteract = giantBoss;
            mainCamera.transform.DOMoveX(currentMainTower.transform.position.x + 7.5f, 1f).OnComplete(() =>
            {
                player.transform.SetParent(null);
                //currentMainTower.ReturnToPool();
                //Destroy(currentMainTower.gameObject);
                currentMainTower.gameObject.SetActive(false);
                //player.transform.DOScale(player.transform.localScale * 3f, 2f).OnComplete(() =>
                //{
                //    giantBoss.InteractWithPlayer();
                //});
                player.Fly();
                Vector3 posAttackBoss;
                posAttackBoss = giantBoss.transform.position - ((GiantBoss)giantBoss).playerPos;
                player.transform.DOMove(posAttackBoss, 1f).OnComplete(() =>
                {
                    player.StopFly();
                    giantBoss.InteractWithPlayer();
                });
            });
        }
        else
        {
            this.PostEvent(EventID.OnWinLevel);
        }
    }

    private IEnumerator CameraZoomOut(float time)
    {
        float timeStamp = 0f;
        float percentCompleted = 0f;

        while (timeStamp <= time)
        {
            timeStamp += Time.deltaTime;
            percentCompleted = timeStamp / time;
            mainCamera.orthographicSize = Mathf.Lerp(15f, 20f, percentCompleted);
            yield return null;
        }
    }

    private IEnumerator CameraZoomIn(float time)
    {
        float timeStamp = 0f;
        float percentCompleted = 0f;

        while (timeStamp <= time)
        {
            timeStamp += Time.deltaTime;
            percentCompleted = timeStamp / time;
            mainCamera.orthographicSize = Mathf.Lerp(15f, 13f, percentCompleted);
            yield return null;
        }
    }

    public void LoadLevel(object param = null)
    {
        ClearLevel();
        int odd = UserData.CurrentLevel % maxLevel;
        int currentLevel = odd == 0 ? maxLevel : odd;

        levelData = Resources.Load<LevelDataSO>("LevelData/Level" + currentLevel);

        player.Strength = levelData.playerStrength;

        List<TowerData> towerDataList = levelData.Towers;

        for (int towerIndex = 0; towerIndex < towerDataList.Count; towerIndex++)
        {
            //Tower tower = towerPrefab.GetPooledInstance<Tower>();
            Tower tower = Instantiate(towerPrefab, levelMap);
            tower.Setup(towerDataList[towerIndex]);
            tower.transform.SetParent(levelMap);
            tower.transform.localPosition = new Vector3(towerIndex * 8f, 0f, 0f);
            towerList.Add(tower);
        }
        player.ActiveCollider();
        player.transform.SetParent(towerList[0].FloorList[0].PlayerSpawnPosition);
        player.transform.localPosition = Vector3.zero;
        player.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        player.gameObject.SetActive(true);

        SetupTower();

        if (levelData.giantBossID >= 0)
        {
            Enemy giantBossPrefab = GiantBossSO.Instance.giantBosses[(BossType)levelData.giantBossID];
            //giantBoss = giantBossPrefab.GetPooledInstance<Enemy>();
            giantBoss = Instantiate(giantBossPrefab);
            giantBoss.Init(Operation.ADD, levelData.giantBossStrength);
            giantBoss.transform.SetParent(levelMap);
            giantBoss.transform.position = new Vector3(towerList[towerList.Count - 1].transform.position.x + 10f, 5f, 0f);
            giantBoss.transform.localScale = GiantBossSO.Instance.giantBosses[(BossType)levelData.giantBossID].transform.localScale;
            //gameMode = GameMode.AUTOPLAY;
        }
        else
        {
            giantBoss = null;
            //gameMode = GameMode.PLAYING;
        }
        gameMode = GameMode.AUTOPLAY;
    }

    public void GameIntro()
    {
        //if (gameMode != GameMode.AUTOPLAY) return;
        if (towerList.Count < 2) return;
        float oldCameraPosX = mainCamera.transform.position.x;
        float cameraDestinationX;
        if (giantBoss != null)
        {
            cameraDestinationX = giantBoss.transform.position.x;
        }
        else
        {
            cameraDestinationX = towerList[towerList.Count - 1].transform.position.x;
        }
        mainCamera.transform.DOMoveX(cameraDestinationX, 3f).OnComplete(() =>
        {
            mainCamera.transform.DOMoveX(oldCameraPosX, 2f).OnComplete(() =>
            {
                gameMode = GameMode.PLAYING;
                UIManager.Instance.GameplayUI.ShowButtons(true);
            });
        });
    }

    [ContextMenu("Return To Pool")]
    private void ClearLevel()
    {
        player.transform.SetParent(null);
        foreach (Tower tower in towerList)
        {
            //tower.ReturnToPool();
            Destroy(tower.gameObject);
        }

        towerList.Clear();

        if (giantBoss != null)
        {
            Destroy(giantBoss.gameObject);
            giantBoss = null;
        }

        if (princess != null)
        {
            Destroy(princess.gameObject);
            princess = null;
        }

        mainCamera.transform.localPosition = Vector3.zero;
        mainCamera.orthographicSize = 15f;

        player.gameObject.SetActive(false);
        player.StrengthText.gameObject.SetActive(true);
        player.Weapon = null;
    }
    public void SetupTower()
    {
        if (towerList.Count < 2) return;
        //foreach(Tower tower in towerList)
        //{
        //    tower.SetupFloor();
        //}
        currentMainTower = towerList[0];
        currentMainTower.isOccupied = true;
        enemyTower = towerList[1];
        currentMainTower.SetInteractable(true);
        enemyTower.SetInteractable(true);
    }

    private void CheckTowerDone(object param)
    {
        if (enemyTower == towerList[towerList.Count - 1] && giantBoss == null) 
        {
            enemyTower.CheckEnemyClear();
        }
        else
        {
            enemyTower.CheckClear();
        }
    }

    private void Win(object param)
    {
        player.transform.SetParent(null);
        mainCamera.transform.localPosition = new Vector3(-64, -6.3f, 0);
        mainCamera.orthographicSize = 13f;
        shop.ModelDance();
        gameMode = GameMode.AUTOPLAY;
        UserData.CurrentLevel++;
    }

    public void ShowAllStrength()
    {
        foreach(Tower tower in towerList)
        {
            foreach(Floor floor in tower.FloorList)
            {
                foreach(var ob in floor.InteractableObjects)
                {
                    ((BaseOperationObject)ob).ShowStrength();
                }
            }
        }
    }

    public void JoinCastle()
    {
        ClearLevel();
        UIManager.Instance.ShowCastleUI();
        castle.gameObject.SetActive(true);

        mainCamera.transform.localPosition = new Vector3(-35f, 1f, 0);
        mainCamera.orthographicSize = 15f;
        player.transform.SetParent(castle.waitFloor.ObjectSpawnPositions[1]);
        player.transform.localPosition = Vector3.zero;
        player.gameObject.SetActive(true);
        castle.JoinCastle();
        gameMode = GameMode.PLAYING;
    }


    public void ShowShop()
    {
        UIManager.Instance.ShowShopUI();
        mainCamera.transform.localPosition = new Vector3(-64f, -10f, 0);
        mainCamera.orthographicSize = 15f;
        player.ShowStrength(false);
        shop.JoinShop();
    }

    public void HideShop()
    {
        shop.LeaveShop();
    }
    public void ReturnMainMenu()
    {
        gameMode = GameMode.AUTOPLAY;
        mainCamera.transform.localPosition = camMainMenuPos;
        mainCamera.orthographicSize = 10f;
        if (castle.isActive)
            castle.LeaveCastle();
    }
}

public enum GameMode
{
    MENU,
    AUTOPLAY,
    PLAYING
}
