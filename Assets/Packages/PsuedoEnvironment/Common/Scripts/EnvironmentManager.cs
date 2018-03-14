using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrefsGUI;


public class EnvironmentManager : SingletonMonoBehaviour<EnvironmentManager> {

    public Camera _camera;

    public PrefsVector2 EnvironmentRegion = new PrefsVector2("Environment Region (Width x Height)", new Vector2(10f, 10f));

    public PrefsFloat Scale = new PrefsFloat("Scale", 0.1f);

    [SerializeField] private PrefsBool _fitY = new PrefsBool("Fit camera Y axis", true);


    [SerializeField] private RandomWalker _walker;

    [SerializeField] private PrefsFloat _initialVelocity = new PrefsFloat("Initial Velocity", 0.1f);    // norm of velocity

    [SerializeField] private List<RandomWalker> _walkers = new List<RandomWalker>();
    private int id = 0;

    private Transform _parentOfWalkers;

	// Use this for initialization
	void Start () {
        _parentOfWalkers = GameObject.Find("Objects").transform;

	}
	
	// Update is called once per frame
	void Update () {

        // ボタンが押されたら
        if (_fitY)
        {
            _camera.orthographicSize = EnvironmentRegion.Get().y * 0.5f;
        }
        else
        {
            _camera.orthographicSize = EnvironmentRegion.Get().x * 0.5f / _camera.aspect;
        }
       

	}

    public void DebugMenuGUI()
    {
        EnvironmentRegion.OnGUI();
        Scale.OnGUI();
        _initialVelocity.OnGUI();
        _fitY.OnGUI();

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Remove"))
        {
            if(_walkers.Count > 0)
            {
                var removed = _walkers.GetRange(0, 1);
                _walkers.Remove(removed[0]);
                Destroy(removed[0].gameObject);
            }
            
        }

        if (GUILayout.Button("Add"))
        {
            var obj = Instantiate(_walker, new Vector3(Random.value, 0, Random.value), Quaternion.identity, _parentOfWalkers);
            // 
            obj.GetComponent<RandomWalker>().Name = id.ToString();
            obj.InitialVelocity = Random.insideUnitCircle * _initialVelocity;
            obj.Position = new Vector2(Random.Range(-EnvironmentRegion.Get().x * 0.5f, EnvironmentRegion.Get().x * 0.5f), Random.Range(-EnvironmentRegion.Get().y * 0.5f, EnvironmentRegion.Get().y * 0.5f) );

            id++;
        }

        GUILayout.EndHorizontal();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(EnvironmentRegion.Get().x, 0, EnvironmentRegion.Get().y));
    }

    public void AddWalker(RandomWalker walker)
    {
        _walkers.Add(walker);
        Debug.Log(string.Format("<color=cyan>Successfully Added: {0}</color>", walker.Name));
    }

    public void RemoveWalker(RandomWalker walker)
    {
        _walkers.Remove(walker);
        Debug.Log(string.Format("<color=red>Successfully Removed: {0}</color>", walker.Name));
    }
}
