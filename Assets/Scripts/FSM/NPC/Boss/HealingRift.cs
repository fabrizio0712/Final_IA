using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingRift : MonoBehaviour, IAbility
{
    [SerializeField] private float _duration;
    [SerializeField] private float _healing;
    private int _team;
    private List<GameObject> _insideCharacters;
    

    void Start()
    {
        _insideCharacters = new List<GameObject>();
    }

    void Update()
    {
        if (_duration <= 0) Destroy(gameObject);
        foreach(GameObject character in _insideCharacters) 
        {
            if (character && character.GetComponent<NPC>().IsAlive)
            {
                float frameHeal = Time.deltaTime * _healing;
                character.GetComponent<NPC>().GetHeal(frameHeal);
            }
        }
        _duration -= Time.deltaTime;
    }
    public void Initialized(int Team)
    {
        _team = Team;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NPC>()) 
        { 
            if (!_insideCharacters.Contains(other.gameObject))
            {
                _insideCharacters.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<NPC>())
        {
            if (!_insideCharacters.Contains(other.gameObject))
            {
                _insideCharacters.Add(other.gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_insideCharacters.Contains(other.gameObject))
        {
            _insideCharacters.Remove(other.gameObject);
        }
    }
}
