using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Scarecrow
{
    class Spawner : MonoBehaviour
    {
        Transform[] _spawners;

        [SerializeField] Pickup _pickupPrefab;
        [SerializeField] int _maxPickups = 5;
        [SerializeField] float _wallDistance = 10f;
        [SerializeField] float _forwardOffset = 0.1f;
        [SerializeField] float _upwardOffset = 1f;
        [SerializeField] UnityEvent _onSpawn;
        
        void Awake()
        {
            _spawners = new Transform[transform.childCount];
            for (var i = 0; i < _spawners.Length; i++) 
                _spawners[i] = transform.GetChild(i);
        }

        void Start()
        {
            var spawners = _spawners.ToList();
            for (var i = 0; i < _maxPickups; i++)
            {
                var spawner = Random.Range(0, spawners.Count);
                var hits = new List<RaycastHit>();
                for (var d = 0; d < 4; d++)
                {
                    var dir = d switch
                    {
                        0 => Vector3.right,
                        1 => Vector3.left,
                        2 => Vector3.forward,
                        _ => Vector3.back
                    };
                    if (Physics.Raycast(spawners[spawner].position, dir, out var hit, _wallDistance))
                        hits.Add(hit);
                }
                var wall = hits[Random.Range(0, hits.Count)];
                var point = wall.point + wall.normal * _forwardOffset + Vector3.up * _upwardOffset;
                var rot = Quaternion.LookRotation(wall.normal, Vector3.up);
                var pickup = Instantiate(_pickupPrefab, point, rot);
                pickup.transform.SetParent(spawners[spawner], true);
                spawners.Remove(spawners[spawner]);
                _onSpawn.Invoke();
            }
        }
    }
}
