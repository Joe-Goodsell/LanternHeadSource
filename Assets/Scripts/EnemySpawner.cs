using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	[SerializeField] private GameObject player;
	[SerializeField] private Tilemap tilemap;
	[SerializeField] private float spawnRadius = 5f;
	[SerializeField] public int numEnemiesAlive = 0;
	[SerializeField] private float maxEnemiesAlive = 5f;
	[SerializeField] private int spawnGracePeriod = 600; // How many frames before attempting to respawn an enemy
	[SerializeField] private GameObject potionPrefab;
	[SerializeField] private GameObject fuelPrefab;
	[SerializeField] private int itemDropChance = 25;
	[SerializeField] private int maxEnemiesIncreaseSeconds = 15;
	private int gracePeriod = 0;
	private int maxSpawnAttempts = 10;
	private List<Vector3> existingTilePositions = new List<Vector3>();
	private float timeSinceLastIncrement = 0.0f;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		timeSinceLastIncrement += Time.deltaTime;

		if (timeSinceLastIncrement > maxEnemiesIncreaseSeconds) {
			timeSinceLastIncrement -= maxEnemiesIncreaseSeconds;
			maxEnemiesAlive += 1;
		}

		if (gracePeriod > 0) {
			gracePeriod--;
		} else if (numEnemiesAlive < maxEnemiesAlive) {
			SpawnEnemy();
			gracePeriod = spawnGracePeriod;
		}
	}

	// Check if position is on the tilemap
	private bool IsOnTilemap(Vector3 position)
	{
		return tilemap.HasTile(tilemap.WorldToCell(position));
	}

	private void SpawnEnemy() {
		Debug.Log("Spawning enemy...");
		Vector3 spawnPosition = new Vector3();
		int spawnAttempts = 0;
		// Generate random nearby positions until one is on the tilemap or until max attempts reached
		do {
			spawnPosition = new Vector3(
				Random.Range(transform.position.x - spawnRadius, transform.position.x + spawnRadius),
				Random.Range(transform.position.y - spawnRadius, transform.position.y + spawnRadius),
				0
			);
			spawnAttempts++;
		} while (!IsOnTilemap(spawnPosition) && spawnAttempts < maxSpawnAttempts);

		// Spawn enemy if max attempts not reached, and increment numEnemiesAlive
		if (spawnAttempts < maxSpawnAttempts) {
			var newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
			newEnemy.GetComponent<EnemyBehaviour>().spawner = this;
			newEnemy.GetComponent<EnemyController>().target = player;
			numEnemiesAlive++;
			Debug.Log("Spawn Successful");
		} else {
			Debug.Log("Spawn Failed");
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