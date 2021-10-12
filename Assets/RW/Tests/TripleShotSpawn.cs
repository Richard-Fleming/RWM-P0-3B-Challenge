using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TripleShotSpawn
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
    public IEnumerator TripleShot()
    {
        game.NewGame();
        Game.GameOver();

        GameObject[] asteroid = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            asteroid[i] = game.GetSpawner().SpawnAsteroid();
            asteroid[i].GetComponent<Asteroid>().health = 1;
            asteroid[i].transform.position = game.GetShip().transform.position;
        }
        asteroid[0].transform.position = new Vector2(asteroid[0].transform.position.x, 0.0f);
        Debug.Log(asteroid[0].transform.position);

        asteroid[1].transform.position = new Vector2(asteroid[1].transform.position.x, 4.0f);
        Debug.Log(asteroid[1].transform.position);
        
        asteroid[2].transform.position = new Vector2(asteroid[2].transform.position.x, 8.0f);
        Debug.Log(asteroid[2].transform.position);

        game.GetShip().GetComponent<Ship>().hasShrapnel = false;
        game.GetShip().ShootTripleShot();
        Debug.Log(game.GetShip().transform.position);

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < 3; i++)
        {
            UnityEngine.Assertions.Assert.IsNull(asteroid[i]);
        }
    }

}
