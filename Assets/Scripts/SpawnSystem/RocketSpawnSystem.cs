using Game;

namespace SpawnSystem
{
    public class RocketSpawnSystem : ISpawnSystem, ITickable
    {
        EntitySpawner<Rocket> entitySpawner;
        EntityFactory<Rocket> factory;
        SpawnStrategy strategy;
        RocketData data;
        bool spawningEnabled;
        
        float currentRocketSpawnInterval;
        int maxDistanceTraveled;
        float timer;

        public RocketSpawnSystem(RocketData data, EntityFactory<Rocket> factory, SpawnStrategy strategy)
        {
            entitySpawner = new EntitySpawner<Rocket>(factory, strategy, data);
            this.factory = factory;
            this.strategy = strategy;
            this.data = data;
            currentRocketSpawnInterval = data.SpawnInterval;
        }
        
        public void Init()
        {
            GameManager.Instance.OnDistanceChanged += AdjustRocketSpawnInterval;
            GameManager.Instance.OnGameOver += CancelRocketSpawning;
        }

        public void Tick(float deltaTime)
        {
            if (!spawningEnabled) return;
            
            timer += deltaTime;
            if (timer >= currentRocketSpawnInterval)
            {
                timer = 0;
                SpawnRocket();
            }
        }

        public void Dispose()
        {
            GameManager.Instance.OnDistanceChanged -= AdjustRocketSpawnInterval;
            GameManager.Instance.OnGameOver -= CancelRocketSpawning;
        }
        
        void AdjustRocketSpawnInterval(int distance)
        {
            if (distance <= maxDistanceTraveled)
                return;

            maxDistanceTraveled = distance;

            if (maxDistanceTraveled % data.DistanceIntervalForDifficultyIncrease != 0)
                return;

            currentRocketSpawnInterval *= data.IntervalMultiplier;
        }
        
        void SpawnRocket()
        {
            entitySpawner.Spawn();
        }
        
        void CancelRocketSpawning(long score)
        {
            spawningEnabled = false;
        }
    }
}