using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] public GameObject enemyPrefab;
	[SerializeField] private GameObject player;
	[SerializeField] public Tilemap tilemap;
	[SerializeField] public float spawnRadius = 5f;
	[SerializeField] private int numEnemiesAlive = 0;
	[SerializeField] private int maxEnemiesAlive = 5;
	[SerializeField] private int spawnGracePeriod = 600; // How many frames before attempting to respawn an enemy
	private int gracePeriod = 0;
	public int maxSpawnAttempts = 10;
	private List<Vector3> existingTilePositions = new List<Vector3>();

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
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
			newEnemy.GetComponent<EnemyController>().target = player;
			numEnemiesAlive++;
			Debug.Log("Spawn Successful");
		} else {
			Debug.Log("Spawn Failed");
		}
	}
}
