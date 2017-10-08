using UnityEngine;
using System.Collections;

public class WorldGeneration : MonoBehaviour {

	[SerializeField]
	GameObject[] worldTiles;

	GameObject worldTileInstance;


	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SpawnTile(GameObject tile, Vector3 offset, float rotation)
	{
		worldTileInstance = Instantiate(tile) as GameObject;
		worldTileInstance.name = tile.name;

		worldTileInstance.transform.rotation = Quaternion.Euler(new Vector3(worldTileInstance.transform.rotation.eulerAngles.x, rotation, worldTileInstance.transform.rotation.eulerAngles.z));
		worldTileInstance.transform.position = new Vector3(tile.transform.position.x + offset.x, tile.transform.position.y + offset.y, tile.transform.position.z + offset.z);

		worldTileInstance.transform.SetParent(transform);
	}

}
