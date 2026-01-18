using Fusion;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class PlayerListUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject Panel;
    public TMP_Text PlayerEntryPrefab;
    public Transform ContentRoot;

    private NetworkRunner _runner;
    private readonly List<TMP_Text> _entries = new();

    private void Start()
    {
        Panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Panel.SetActive(true);
            Refresh();
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Panel.SetActive(false);
        }
    }

    private void Refresh()
    {
        Clear();

        if (_runner == null)
            return;

        foreach (var playerRef in _runner.ActivePlayers)
        {
            if (_runner.TryGetPlayerObject(playerRef, out var obj))
            {
                var data = obj.GetComponent<PlayerData>();
                if (data == null)
                    continue;

                var entry = Instantiate(PlayerEntryPrefab, ContentRoot);
                entry.text = data.PlayerName;
                _entries.Add(entry);
                Debug.Log(_entries);
            }
        }
    }

    private void Clear()
    {
        foreach (var e in _entries)
            Destroy(e.gameObject);

        _entries.Clear();
    }
    
    public void SetRunner(NetworkRunner runner)
    {
        _runner = runner;
    }
}
