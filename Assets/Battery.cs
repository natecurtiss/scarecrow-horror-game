using System.Collections.Generic;
using UnityEngine;

namespace Scarecrow
{
    class Battery : MonoBehaviour
    {
        static readonly List<Transform> _occupiedSpawns = new();
        [SerializeField] Transform[] _spawnpoints;
        Transform _spawn;

        void Start()
        {
            var notOccupied = true;
            while (notOccupied)
            {
                var spawn = _spawnpoints[Random.Range(0, _spawnpoints.Length)];
                if (!_occupiedSpawns.Contains(spawn))
                {
                    _spawn = spawn;
                    _occupiedSpawns.Add(_spawn);
                    notOccupied = false;
                }
            }
            transform.position = _spawn.position;
        }

        Transform Furthest()
        {
            var longest = Mathf.Infinity;
            var furthest = _spawnpoints[0];
            foreach (var point in _spawnpoints)
            {
                if (_occupiedSpawns.Contains(point))
                    continue;
                var dist = (transform.position - point.position).sqrMagnitude;
                if (dist > longest)
                {
                    furthest = point;
                    longest = dist;
                }
            }
            return furthest;
        }
        
        public void Use()
        {
            _occupiedSpawns.Remove(_spawn);
            _spawn = Furthest();
            _occupiedSpawns.Add(_spawn);
            transform.position = _spawn.position;
        }
    }
}
