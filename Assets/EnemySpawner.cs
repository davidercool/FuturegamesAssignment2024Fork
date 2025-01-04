using Mechadroids;
using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public AIEntitiesHandler aiEntitiesHandler;
    [SerializeField] private EnemyGroup enemyGroup;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            aiEntitiesHandler.SpawnEnemyGroup(enemyGroup);
        }
    }
}
