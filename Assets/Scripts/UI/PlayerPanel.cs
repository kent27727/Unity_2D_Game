using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;

    Player _player;

    public void Bind(Player player)
    {
        _player = player;
    }

    void Update()
    {

        if (_player != null)
            _scoreText.SetText(_player.Coins.ToString());

    }
}
