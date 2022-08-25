using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IMove
{
    [SerializeField] private float _speed = 2;
    [SerializeField] private float _maxHealth;
    [SerializeField] private Node currentNode;
    [SerializeField] private GameManager _gm;
    [SerializeField] private int _team;
    [SerializeField] private float _viewAngle;
    [SerializeField] private float _viewRadius;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private LayerMask _playerMask;
    [SerializeField] private float _radius;
    [SerializeField] private float _avoidWeight;
    [SerializeField] private float _timePrediction;
    [SerializeField] private Material _material;
    [SerializeField] private SkinnedMeshRenderer _skinMeshRenderer;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _abilityCooldown;
    private float _currentHealth;
    private bool _isAlive;
    private Rigidbody _rb;
    private AgentBFSDFS _agent;
    private FieldOfView _fov;
    private AnimController _animController;
    private FlockingManager _flockingManager;
    private ObstacleAvoidanceBehavior _obsAvoidBehavior;
    private GameObject leader;
    private GameObject _healingRift;
    private GameObject _AoE;
    private float _abilityTimer = 0;
    private List<GameObject> _visibleTargets;

    public Node CurrentNode { get => currentNode; set => currentNode = value; }
    public AgentBFSDFS Agent { get => _agent; set => _agent = value; }
    public GameManager Gm { get => _gm; set => _gm = value; }
    public int Team { get => _team; set => _team = value; }
    public float CurrentHealth { get => _currentHealth; set => _currentHealth = value; }
    public LayerMask ObstacleMask { get => _obstacleMask; set => _obstacleMask = value; }
    public LayerMask PlayerMask { get => _playerMask; set => _playerMask = value; }
    public float Radius { get => _radius; set => _radius = value; }
    public float AvoidWeight { get => _avoidWeight; set => _avoidWeight = value; }
    public Rigidbody Rb { get => _rb; set => _rb = value; }
    public FieldOfView Fov { get => _fov; set => _fov = value; }
    public float TimePrediction { get => _timePrediction; set => _timePrediction = value; }
    public AnimController AnimController { get => _animController; set => _animController = value; }
    public bool IsAlive { get => _isAlive; set => _isAlive = value; }
    public Material Material { get => _material; set => _material = value; }
    public GameObject Leader { get => leader; set => leader = value; }
    public Collider Collider { get => _collider; set => _collider = value; }
    public float AbilityCooldown { get => _abilityCooldown; set => _abilityCooldown = value; }
    public float AbilityTimer { get => _abilityTimer; set => _abilityTimer = value; }
    public GameObject HealingRift { get => _healingRift; set => _healingRift = value; }
    public GameObject AoE { get => _AoE; set => _AoE = value; }
    public List<GameObject> VisibleTargets { get => _visibleTargets; set => _visibleTargets = value; }

    private void Awake()
    {
        _flockingManager = GetComponent<FlockingManager>();
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<AgentBFSDFS>();
        _animController = GetComponent<AnimController>();
        _fov = GetComponent<FieldOfView>();
        _obsAvoidBehavior = GetComponent<ObstacleAvoidanceBehavior>();
        _fov.Owner = this;
        _fov.ViewAngle = _viewAngle;
        _fov.ViewRadius = _viewRadius;
        _isAlive = true;
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
        _skinMeshRenderer.material = _material;
        StartCoroutine("FindTargetsWithDelay", 0.2f);
    }
    private void Update()
    {
        if (Gm.FinishPanel.activeSelf) Destroy(gameObject);
        if (_rb.velocity.magnitude > 0.1) transform.forward = Vector3.Lerp(transform.forward, new Vector3(_rb.velocity.x, 0, _rb.velocity.z), 5 * Time.deltaTime);
    }
    public void Move(Vector3 dir)
    {
        dir.y = 0;
        Vector3 obsDir = _obsAvoidBehavior.GetDir();
        obsDir = new Vector3(obsDir.x, 0, obsDir.z);
        Vector3 flokingDir = _flockingManager.GetFlokingDir();
        flokingDir = new Vector3(flokingDir.x, 0, flokingDir.z);
        dir = dir + obsDir + flokingDir;
        _rb.velocity = dir.normalized * _speed;
        
    }
    public void GetDamage(float dmg)
    {
        if (dmg <= 0) return;
        _currentHealth -= dmg;
    }
    public void GetHeal(float heal)
    {
        if (heal <= 0) return;
        _currentHealth += heal;
        if (_currentHealth > _maxHealth) _currentHealth = _maxHealth;
        Debug.Log("aaahhh");
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            _visibleTargets = _fov.FindVisibleTargets();
        }
    }
}
