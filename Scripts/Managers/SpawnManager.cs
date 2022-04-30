using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltBalance.Managers
{
    public class SpawnManager : Manager<SpawnManager>
    {
        [Header("Spawn Settings")]
        public Transform Player;
        public Transform Platform;
        public Transform ObstacleContainer;
        [System.Serializable]
        public class ObstacleType
        {
            public string Name;
            public int StartSpawnScore;
            public GameObject ObstaclePrefab;
        }
        public ObstacleType[] ObstacleTypes;
        public float SpawnYPos;
        List<GameObject> SpawnableObstacles;
        Vector2 CornerOfPlatfrom;
        Vector2 OtherCornerOfPlatfrom;

        [Header("Difficulty")]
        [Range(0f, 1f)]
        public float PlayerSpawnRatio;
        public float SpawnDelay;
        public float SpawnDelayDecreaseAmountPerSpawn;
        public float MinSpawnDelay;
        float SpawnTimer;

        // Start is called before the first frame update
        void Start()
        {
            Init(this);
            SpawnableObstacles = new List<GameObject>();

            CornerOfPlatfrom = new Vector2(Platform.localScale.x, Platform.localScale.z) / 2f;
            OtherCornerOfPlatfrom = -CornerOfPlatfrom;

            SpawnTimer = SpawnDelay;
        }

        // Update is called once per frame
        void Update()
        {
            foreach (ObstacleType OBT in ObstacleTypes)
            {
                if(GameManager.Instance.Score >= OBT.StartSpawnScore && !SpawnableObstacles.Contains(OBT.ObstaclePrefab))
                {
                    SpawnableObstacles.Add(OBT.ObstaclePrefab);
                }
            }

            SpawnTimer -= Time.deltaTime;
            if(SpawnTimer <= 0f)
            {
                SpawnTimer = SpawnDelay;
                if(SpawnDelay > MinSpawnDelay) { SpawnDelay -= SpawnDelayDecreaseAmountPerSpawn; }
                Spawn();
            }
        }

        public void Spawn()
        {
            Vector3 RandomPos = new Vector3(Random.Range(CornerOfPlatfrom.x, OtherCornerOfPlatfrom.x), SpawnYPos, Random.Range(CornerOfPlatfrom.y, OtherCornerOfPlatfrom.y));
            Vector3 PlayerPos = (Player != null) ? new Vector3(Player.position.x, SpawnYPos, Player.position.x) : RandomPos;
            bool SpawnRandom = Random.value > PlayerSpawnRatio;
            int ObstacleIndex = Random.Range(0, SpawnableObstacles.Count);
            Instantiate(SpawnableObstacles[ObstacleIndex], (SpawnRandom) ? RandomPos : PlayerPos, SpawnableObstacles[ObstacleIndex].transform.rotation, ObstacleContainer);
        }
    }
}