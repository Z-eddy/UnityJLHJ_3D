using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("最大转向半径")] public float maxRadius = 20;
    
    // 动画对象
    private Animator _animator;

    // 玩家方向
    private Vector3 _direction = Vector3.zero;

    // 四元数转向
    private Quaternion _quaternion = Quaternion.identity;

    // 刚体对象
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        // 初始化刚体对象
        _rigidbody = GetComponent<Rigidbody>();
        // 初始化动画对象
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        // 获取玩家的方向
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        // 查看是否有移动
        var isWalking = !Mathf.Approximately(horizontal, 0) || !Mathf.Approximately(vertical, 0);
        if (isWalking)
        {
            // 更新玩家方向
            _direction.Set(horizontal, 0, vertical);
            // 方向归一化
            _direction.Normalize();
        }

        // 设置奔跑状态
        _animator.SetBool("IsWalking", isWalking);
        
        // 获得玩家需要的朝向
        var lookForward = Vector3.RotateTowards(transform.forward,_direction,Time.deltaTime*maxRadius,0);
        // 设置玩家朝向四元数
        _quaternion = Quaternion.LookRotation(lookForward);
    }

    private void OnAnimatorMove()
    {
        // 刚体移动
        _rigidbody.MovePosition(_rigidbody.position + _direction * _animator.deltaPosition.magnitude);
        // 刚体旋转
        _rigidbody.MoveRotation(_quaternion);
    }
}