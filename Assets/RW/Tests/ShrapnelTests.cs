﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ShrapnelTests
{

    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject =
          MonoBehaviour.Instantiate(
            Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

     [UnityTest]
    public IEnumerator ShrapnelMade()
    {
        game.NewGame();
        Game.GameOver();

        
      
        GameObject newlaser = game.GetShip().SpawnLaser();
        newlaser.GetComponent<Laser>().spawnShrapnel();
        
        yield return new WaitForSeconds(0.1f);
        Assert.Greater(game.shrapnelCounter, 0);
    }  
} 