using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class SniperTests
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
    public IEnumerator AsteroidTakesTwoHits()
    {
        game.NewGame();
        Game.GameOver();

        GameObject ship = game.GetShip().gameObject;
        ship.GetComponent<Ship>().countBullets = 0;

        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        asteroid.transform.position = asteroid.transform.position - new Vector3(20.0f, -20.0f, 0.0f);
        asteroid.GetComponent<Asteroid>().health = 2;

        GameObject laser = game.GetShip().SpawnLaser();
        laser.GetComponent<Laser>().isShrapnel = false;
        laser.GetComponent<Laser>().isSniperBullet = false;

        laser.transform.position = asteroid.transform.position;

        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNotNull(asteroid);

        asteroid.transform.position = Vector3.zero;
        GameObject lasertwo = game.GetShip().SpawnLaser();
        lasertwo.GetComponent<Laser>().isShrapnel = false;
        lasertwo.GetComponent<Laser>().isSniperBullet = false;
        lasertwo.transform.position = Vector3.zero;

        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator AsteroidDestroyedInstantly()
    {
        game.NewGame();
        Game.GameOver();

        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        asteroid.GetComponent<Asteroid>().health = 2;

        GameObject laser = game.GetShip().SpawnLaser();
        laser.GetComponent<Laser>().isShrapnel = false;
        laser.GetComponent<Laser>().isSniperBullet = true;
        laser.transform.position = Vector3.zero;

        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }

    [UnityTest]
    public IEnumerator ShipSpawnsSniperBulletOnTenthShot()
    {
        game.NewGame();
        Game.GameOver();

        GameObject ship = game.GetShip().gameObject;
        ship.GetComponent<Ship>().countBullets = 10;

        GameObject laser = game.GetShip().SpawnLaser();
        laser.GetComponent<Laser>().isShrapnel = false;

        yield return new WaitForSeconds(0.1f);

        UnityEngine.Assertions.Assert.IsTrue(laser.GetComponent<Laser>().isSniperBullet);


    }
}