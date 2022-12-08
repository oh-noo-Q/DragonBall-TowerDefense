using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesDataSO", menuName = "Data/EnemiesDataSO")]
public class EnemiesDataSO : ScriptableObject
{
    List<EnemySO> enemySO;
}
