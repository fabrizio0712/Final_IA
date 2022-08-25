using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _maxAmount;
    [SerializeField] private int _minAmount;
    [SerializeField] private List<Node> _nodes;
    [SerializeField] private Node _spawnTeam1;
    private int _amountTeam1;
    [SerializeField] private Material _team1NPC;
    [SerializeField] private Material _team1Boss;
    [SerializeField] private Node _spawnTeam2;
    private int _amountTeam2;
    [SerializeField] private Material _team2NPC;
    [SerializeField] private Material _team2Boss;
    [SerializeField] private GameObject _character;
    [SerializeField] private GameObject _boss;
    [SerializeField] private GameObject _healingRift;
    [SerializeField] private GameObject _AoE;
    private GameObject _bossTeam1;
    private GameObject _bossTeam2;

    [SerializeField] private GameObject _menuPanel;
    [SerializeField] private GameObject _playPanel;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private Text _textTeam1;
    [SerializeField] private Text _textTeam2;
    [SerializeField] private Text _textFinish;

    bool _gameStarted = false;
    int value;

    public GameObject FinishPanel { get => _finishPanel; }

    void Start()
    {
        _amountTeam1 = _minAmount;
        _amountTeam2 = _minAmount;
        _textTeam2.text = _amountTeam2.ToString();
        _textTeam1.text = _amountTeam1.ToString();
    }

    void Update()
    {
        if (_gameStarted)
        {
            if (_bossTeam1.GetComponent<NPC>().IsAlive)
            {
                if (!_bossTeam2.GetComponent<NPC>().IsAlive)
                {
                    _gameStarted = false;
                    Debug.Log("Team 1 Win");
                    _finishPanel.SetActive(true);
                    _textFinish.text = "Team 1 Wins";
                }
            }
            else
            {
                _gameStarted = false;
                Debug.Log("Team 2 Win");
                _finishPanel.SetActive(true);
                _textFinish.text = "Team 2 Wins";
            }
        }
    }
    public Node GetRandomNode() 
    {
        value = Random.Range(0, _nodes.Count);
        return _nodes[value];
    }
    public void Play()
    {
        _menuPanel.SetActive(false);
        _playPanel.SetActive(true);
    }
    public void Back()
    {
        _playPanel.SetActive(false);
        _menuPanel.SetActive(true);
    }
    public void StartPlay()
    {
        _playPanel.SetActive(false);

        _bossTeam1 = Instantiate(_boss, _spawnTeam1.transform.position + new Vector3(Random.RandomRange(-0.5f, 0.5f), 0, Random.RandomRange(-0.5f, 0.5f)), Quaternion.identity);
        _bossTeam1.GetComponent<NPC>().Material = _team1Boss;
        _bossTeam1.GetComponent<NPC>().Gm = this;
        _bossTeam1.GetComponent<NPC>().Team = 1;
        _bossTeam1.GetComponent<NPC>().HealingRift = _healingRift;
        _bossTeam1.GetComponent<NPC>().AoE = _AoE;

        _bossTeam2 = Instantiate(_boss, _spawnTeam2.transform.position + new Vector3(Random.RandomRange(-0.5f, 0.5f), 0, Random.RandomRange(-0.5f, 0.5f)), Quaternion.identity);
        _bossTeam2.GetComponent<NPC>().Material = _team2Boss;
        _bossTeam2.GetComponent<NPC>().Gm = this;
        _bossTeam2.GetComponent<NPC>().Team = 2;
        _bossTeam2.GetComponent<NPC>().HealingRift = _healingRift;
        _bossTeam2.GetComponent<NPC>().AoE = _AoE;


        for (int i = 0; i < _amountTeam1; i++)
        {
            GameObject temp = Instantiate(_character, _spawnTeam1.transform.position + new Vector3(Random.RandomRange(-0.5f, 0.5f), 0, Random.RandomRange(-0.5f, 0.5f)), Quaternion.identity);
            temp.GetComponent<NPC>().Gm = this;
            temp.GetComponent<NPC>().Team = 1;
            temp.GetComponent<NPC>().Material = _team1NPC;
            temp.GetComponent<NPC>().Leader = _bossTeam1;
        }
        for (int i = 0; i < _amountTeam2; i++)
        {
            GameObject temp = Instantiate(_character, _spawnTeam2.transform.position + new Vector3(Random.RandomRange(-0.5f, 0.5f), 0, Random.RandomRange(-0.5f, 0.5f)), Quaternion.identity);
            temp.GetComponent<NPC>().Gm = this;
            temp.GetComponent<NPC>().Team = 2;
            temp.GetComponent<NPC>().Material = _team2NPC;
            temp.GetComponent<NPC>().Leader = _bossTeam2;
        }
        _gameStarted = true;

    }
    public void BackMenu()
    {
        _finishPanel.SetActive(false);
        _menuPanel.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PlusTeam1()
    {
        if (_amountTeam1 < _maxAmount)
        {
            _amountTeam1++;
            _textTeam1.text = _amountTeam1.ToString();
        }
    }
    public void PlusTeam2()
    {
        if (_amountTeam2 < _maxAmount)
        {
            _amountTeam2++;
            _textTeam2.text = _amountTeam2.ToString();
        }
    }
    public void MinusTeam1()
    {
        if (_amountTeam1 > _minAmount) 
        {
            _amountTeam1--;
            _textTeam1.text = _amountTeam1.ToString();
        }
    }
    public void MinusTeam2()
    {
        if (_amountTeam2 > _minAmount) 
        {
            _amountTeam2--;
            _textTeam2.text = _amountTeam2.ToString();
        }

    }
}
