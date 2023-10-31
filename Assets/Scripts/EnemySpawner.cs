using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject rangedEnemyPrefab;
	[SerializeField] private GameObject player;
	[SerializeField] private Tilemap tilemap;
	[SerializeField] private float spawnRadius = 5f;
	[SerializeField] public int numEnemiesAlive = 0;
	[SerializeField] private int maxEnemiesAlive = 5;
	[SerializeField] private int maxEnemiesIncreaseRate = 60; // How often to increase maxEnemiesAlive in seconds
	[SerializeField] private float spawnGracePeriod = 7.5f; // How many seconds before attempting to respawn an enemy
	[SerializeField] private GameObject potionPrefab;
	[SerializeField] private GameObject fuelPrefab;
	[SerializeField] private int itemDropChance = 25;
	private float maxEnemiesIncreaseTimer = 0f;
	private float gracePeriod = 0f;
	private int maxSpawnAttempts = 10;
	private List<Vector3> existingTilePositions = new List<Vector3>();

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame

	void Update()
	{
		// Decrase gracePeriod and spawn enemy if gracePeriod is less than 0
		gracePeriod -= Time.deltaTime;
		if (gracePeriod <= 0 && numEnemiesAlive < maxEnemiesAlive) {
			if (SpawnEnemy()) {
				gracePeriod = spawnGracePeriod;
			} else {
				gracePeriod = spawnGracePeriod / 4;
			}
		}

		// Increase maxEnemiesAlive every 15 seconds
		maxEnemiesIncreaseTimer += Time.deltaTime;
		if (maxEnemiesIncreaseTimer >= maxEnemiesIncreaseRate) {
			maxEnemiesAlive++;
			maxEnemiesIncreaseTimer = 0f;
		}
	}

	// Check if position is on the tilemap
	private bool IsOnTilemap(Vector3 position)
	{
		return tilemap.HasTile(tilemap.WorldToCell(position));
	}

	private bool SpawnEnemy() {
		Debug.Log("Spawning enemy...");
		Vector3 spawnPosition = new Vector3();
		int spawnAttempts = 0;
		// Generate random nearby positions until one is on the tilemap or until max attempts reached
		do {
			spawnPosition = new Vector3(
				Random.Range(player.transform.position.x - spawnRadius, player.transform.position.x + spawnRadius),
				Random.Range(player.transform.position.y - spawnRadius, player.transform.position.y + spawnRadius),
				0
			);
			spawnAttempts++;
		} while (!IsOnTilemap(spawnPosition) && spawnAttempts < maxSpawnAttempts);

		// Spawn enemy if max attempts not reached, and increment numEnemiesAlive
		if (spawnAttempts < maxSpawnAttempts) {
            GameObject prefab;
            bool isMelee;
            if (Random.Range(0, 100) > 25)
            {
                isMelee = true; 
                prefab = enemyPrefab;
            } else {
                isMelee = false;
                prefab = rangedEnemyPrefab;
            }
                
			var newEnemy = Instantiate(prefab, spawnPosition, Quaternion.identity);
			newEnemy.GetComponent<EnemyBehaviour>().spawner = this;
            if (isMelee)
            {
			    newEnemy.GetComponent<EnemyController>().target = player;
            } else {
			    newEnemy.GetComponent<RangedEnemyController>().target = player;
            }
			numEnemiesAlive++;
			Debug.Log("Spawn Successful");
			return true;
		} else {
			Debug.Log("Spawn Failed");
			return false;
		}
	}

	// Currently called by enemies on death
	public void SpawnItem(Vector3 position) {
		if (Random.Range(0, 100) >= itemDropChance) {
			Debug.Log("Item Dropped");
			if (Random.Range(0, 100) >= 50) {
				Instantiate(potionPrefab, position, Quaternion.identity);
			} else {
				Instantiate(fuelPrefab, position, Quaternion.identity);
			}
		}
	}
}
