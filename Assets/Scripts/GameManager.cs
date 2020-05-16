using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, Player> players = new Dictionary<int, Player>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SpawnPlayer(int _id, string _username, Vector2 _position)
    {
        GameObject _player;

        if (_id == Client.instance.myId)
        {
            Debug.Log("LOCAL PREFAB");
            _player = Instantiate(localPlayerPrefab);
        }
        else
        {
            Debug.Log("CLIENT SERVER PREFAB");
            _player = Instantiate(playerPrefab);
        }

        _player.transform.position = new Vector3(_position.x, _position.y, 0);
        _player.GetComponent<Player>().id = _id;
        _player.GetComponent<Player>().username = _username;
        players.Add(_id, _player.GetComponent<Player>());
    }
}
