using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Walker
{
    public string name;
    public Vector2 position;

    public readonly string oscNameX;
    public readonly string oscNameY;
    public readonly string OscName;


    public Walker() { }

    public Walker(string name, Vector2 position, string oscNameX, string oscNameY)
    {
        this.name = name;
        this.position = position;
        this.oscNameX = oscNameX;
        this.oscNameY = oscNameY;

        this.OscName = oscNameX + "," + oscNameY;
    }
}

public abstract class RandomWalker : MonoBehaviour {

    // TODO: 加速度とかも何とかしたい。
    public Walker walker = new Walker();
    
    private Vector2 _initialVelocity;
    private Vector2 _currentVelocity;
    private Vector2 _predictedPosition;

    public string Name { get { return walker.name; } set { walker.name = value; } }
    public Vector2 InitialVelocity { get { return _initialVelocity; } set { _initialVelocity = value; } }
    public Vector2 Position { get { return walker.position; } set { walker.position = value; } }


    protected void OnEnable()
    {
        EnvironmentManager.Instance.AddWalker(this);
    }

    protected void Start()
    {
        _currentVelocity = _initialVelocity;

        this.walker = new Walker(this.walker.name, this.walker.position, "/human" + this.walker.name + "/position/x", "/human" + walker.name + "/position/y");
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

        walker.position = PredictPosition();
        transform.position = new Vector3(walker.position.x, 0, walker.position.y);

        // 各々送信していると重たいので、ひとまとめにして送信する方法をとる。
        // SendPosition(walker);
    }

    protected abstract void SendPosition(Walker position); 

    private Vector2 PredictPosition()
    {
        Vector2 regionHalf = EnvironmentManager.Instance.EnvironmentRegion.Get() * 0.5f;

        // 位置予測
        Vector2 predicted = walker.position + _currentVelocity * Time.deltaTime;

        // over x
        if (predicted.x < -regionHalf.x || regionHalf.x < predicted.x)
        {
            _currentVelocity.x *= -1;
        }

        if (predicted.y < -regionHalf.y || regionHalf.y < predicted.y)
        {
            _currentVelocity.y *= -1;
        }

        return walker.position + _currentVelocity * Time.deltaTime;
    }


    private void OnDestroy()
    {
        if (EnvironmentManager.Instance)
        {
            EnvironmentManager.Instance.RemoveWalker(this);
        }
    }

}
