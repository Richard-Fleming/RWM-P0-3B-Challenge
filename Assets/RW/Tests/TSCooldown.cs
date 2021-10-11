using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TSCooldown
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
    public IEnumerator Cooldown()
    {
        game.GetShip().ShootTripleShot();
        yield return new WaitForSeconds(1.5f);

        UnityEngine.Assertions.Assert.AreEqual(game.GetShip().canTriple, false);
    }
}
