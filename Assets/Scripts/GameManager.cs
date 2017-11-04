﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
    private bool hasLevelStarted = false;
    public bool HasLevelStarted { get { return hasLevelStarted; } set { hasLevelStarted = value; }}

    private bool isGamePlaying = false;
    public bool IsGameOver { get { return isGamePlaying; } set { isGamePlaying = value; }}

    private bool isGameOver = false;
    public bool IsGamePlaying { get { return isGameOver; } set { isGameOver = value; }}

    private bool hasLevelFinished = false;
    public bool HasLevelFinished { get { return hasLevelFinished; } set { hasLevelFinished = value; }}

    public float delay = 1f;

    private Board board;
    private PlayerManager player;

    private void Awake()
    {
        board = Object.FindObjectOfType<Board>().GetComponent<Board>();
        player = Object.FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
    }

    private void Start()
    {
        if (board != null && player != null)
        {
            StartCoroutine(RunGameLoop());
        }
        else
        {
            Debug.LogWarning("GAMEMANAGER Error: Board or player missing");
        }
    }

    public void PlayLevel()
    {
        hasLevelStarted = true;
    }

    private IEnumerator RunGameLoop()
    {
        yield return StartCoroutine(StartLevelRoutine());
        yield return StartCoroutine(PlayLevelRoutine());
        yield return StartCoroutine(EndLevelRoutine());
    }

    private IEnumerator StartLevelRoutine()
    {
        Debug.Log("START LEVEL");
        player.playerInput.InputEnabled = false;

        while(!hasLevelStarted)
        {
            yield return null;
        }
    }

    private IEnumerator PlayLevelRoutine()
    {
        Debug.Log("PLAY LEVEL");
        isGamePlaying = true;
        yield return new WaitForSeconds(delay);
        player.playerInput.InputEnabled = true;

        while(!isGameOver)
        {
            yield return null;
        }
    }

    private IEnumerator EndLevelRoutine()
    {
        Debug.Log("END LEVEL");
        player.playerInput.InputEnabled = false;

        while(!hasLevelFinished)
        {
            yield return null;
        }

        RestartLevel();
    }

    private void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}