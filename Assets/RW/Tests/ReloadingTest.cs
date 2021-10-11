using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ReloadingTest
    {

        private Game game;
        private Ship ship;

        [SetUp]
        public void Setup()
        {
            GameObject gameGameObject =
                MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
            game = gameGameObject.GetComponent<Game>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.Destroy(game.gameObject);
           
        }
        

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator ReadingBulletCount()
        {
       
           
                game.GetShip().ShootLaser();
                game.GetShip().countBullets++;
                game.GetShip().reloader();
               
            
            yield return new WaitForSeconds(0.4f);
            // no bullets going

            UnityEngine.Assertions.Assert.AreEqual(game.GetShip().countBullets, 0);
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            
        }
    }
}
