using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RandomWalker : MonoBehaviour {

    // TODO: 加速度とかも何とかしたい。

    protected string _name;
    private Vector2 _initialVelocity;
    private Vector2 _currentPosition;
    private Vector2 _currentVelocity;
    private Vector2 _predictedPosition;

    public string Name { get { return _name; } set { _name = value; } }
    public Vector2 InitialVelocity { get { return _initialVelocity; } set { _initialVelocity = value; } }
    public Vector2 Position { get { return _currentPosition; } set { _currentPosition = value; } }


    protected void OnEnable()
    {
        EnvironmentManager.Instance.AddWalker(this);
    }

    protected void Start()
    {
        _currentVelocity = _initialVelocity;
    }

    protected void Update()
    {
        Walk();
    }


    /// <summary>
    /// proceed one frame
    /// </summary>
    public void Walk()
    {
        // TODO: ノイズで速度ベクトルを少しずらす処理

        _currentPosition = PredictPosition();
        transform.position = new Vector3(_currentPosition.x, 0, _currentPosition.y);

      
        SendPosition(_currentPosition);
    }

    protected abstract void SendPosition(Vector2 position); 

    private Vector2 PredictPosition()
    {
        Vector2 regionHalf = EnvironmentManager.Instance.EnvironmentRegion.Get() * 0.5f;

        // 位置予測
        Vector2 predicted = _currentPosition + _currentVelocity * Time.deltaTime;

        // over x
        if (predicted.x < -regionHalf.x || regionHalf.x < predicted.x)
        {
            _currentVelocity.x *= -1;
        }

        if (predicted.y < -regionHalf.y || regionHalf.y < predicted.y)
        {
            _currentVelocity.y *= -1;
        }

        return _currentPosition + _currentVelocity * Time.deltaTime;
    }


    private void OnDestroy()
    {
        if (EnvironmentManager.Instance)
        {
            EnvironmentManager.Instance.RemoveWalker(this);
        }
    }

}
