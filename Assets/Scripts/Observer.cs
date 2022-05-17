using System;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [Tooltip("玩家的位置")] public Transform player;
    [Tooltip("游戏结束组件")] public GameEnding gameEnding;

    // 查看是否进入了视野
    private bool _isInSight;

    // Update is called once per frame
    private void Update()
    {
        if (_isInSight)
        {

            // 玩家与观察者之间的向量
            var direction = player.position - transform.position + Vector3.up;
            // 创建射线
            var ray = new Ray(transform.position, direction);
            // 发出射线并触碰到的碰撞体
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.transform == player)
                {
                    Debug.Log("玩家进入观察者视野");
                }
            }
        }
    }

    // 进入触发区
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player) _isInSight = true;
    }

    // 离开触发区
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player) _isInSight = false;
    }
}