using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    [Tooltip("巡逻路径")] public List<Transform> wayPoints;

    // 导航
    NavMeshAgent _navMeshAgent;

    // 当前巡逻点
    private int _currentWayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        if (wayPoints.Count > 0)
        {
            _navMeshAgent.SetDestination(wayPoints[_currentWayPointIndex].position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 到达目的地
        if (wayPoints.Count > 0 && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            // 更新目的地索引
            _currentWayPointIndex = (_currentWayPointIndex + 1) % wayPoints.Count;
            // 设置目的地
            _navMeshAgent.SetDestination(wayPoints[_currentWayPointIndex].position);
        }
    }
}