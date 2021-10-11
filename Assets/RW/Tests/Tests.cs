using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class Tests
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
        game.GetSpawner().BeginSpawning();
      
        GameObject newlaser = game.GetShip().SpawnLaser();
        newlaser.GetComponent<Laser>().spawnShrapnel();
        
        yield return new WaitForSeconds(0.5f);
        Assert.Greater(game.shrapnelCounter, 0);
    }  
} 