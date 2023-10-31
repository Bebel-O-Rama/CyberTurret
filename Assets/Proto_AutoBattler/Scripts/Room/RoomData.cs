using UnityEngine;

[CreateAssetMenu]
public class RoomData : ScriptableObject
{
    public string roomName;
    public Vector3 objectiveHolderPosition;
    [Header("Minimum 1 enemy")] public RoomEnemy[] roomEnemies;

    public void SpawnEnemy()
    {
        // Fetching the pf like that is temporary 
        GameObject objHolderPF = Resources.Load<GameObject>("Prefabs/PF_ObjectiveHolder");
        GameObject enemyPF = Resources.Load<GameObject>("Prefabs/PF_Enemy");

        Instantiate(objHolderPF, objectiveHolderPosition, Quaternion.identity);
        foreach (var enemy in roomEnemies)
        {
            enemy.enemyData.SpawnEnemy(enemyPF, enemy.initialPosition, enemy.healthMultiplier, enemy.speedMultiplier,
                enemy.damageMultipler);
        }
    }

    // Otherwise the values for the enemy modifiers are set to 0 when editing a RoomData in the editor
    private void OnValidate()
    {
        if (roomEnemies == null || roomEnemies.Length == 0)
            roomEnemies = new RoomEnemy[1];
    }
}