using UnityEngine;

[CreateAssetMenu]
public class RoomData : ScriptableObject
{
    public string roomName;
    [Header("Minimum 1 enemy")] public RoomEnemy[] roomEnemies;

    public void SpawnEnemy()
    {
        GameObject
            enemyPF = Resources.Load<GameObject>(
                "Prefabs/PF_Enemy (AB)"); // Temporary mesure since I currently use a single PF for the enemies
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