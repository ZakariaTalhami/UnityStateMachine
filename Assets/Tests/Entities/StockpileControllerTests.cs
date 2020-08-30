using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace Tests
{
    public class StockpileControllerTests
    {

        [Test]
        public void StoreWithoutExceeding()
        {
            IStockpile stockpile = new StockPileController(ResourceType.Gold, 200);
            ResourceCollection storeAmount = new ResourceCollection();
            storeAmount.AddToResource(ResourceType.Gold, 20);

            bool isStored = stockpile.Store(storeAmount, out ResourceCollection stored);

            Assert.IsTrue(isStored);
            Assert.AreEqual(storeAmount.TotalResourceAmount(), stored.TotalResourceAmount());
        }

        [Test]
        public void StoreWithExceeding()
        {
            IStockpile stockpile = new StockPileController(ResourceType.Gold, 10);
            ResourceCollection storeAmount = new ResourceCollection();
            storeAmount.AddToResource(ResourceType.Gold, 20);

            bool isStored = stockpile.Store(storeAmount, out ResourceCollection stored);

            Assert.IsTrue(isStored);
            Assert.AreNotEqual(storeAmount.TotalResourceAmount(), stored.TotalResourceAmount());
            Assert.AreEqual(10, stored.TotalResourceAmount());
        }

        [Test]
        public void StoreFailure()
        {
            IStockpile stockpile = new StockPileController(ResourceType.Gold, 0);
            ResourceCollection storeAmount = new ResourceCollection();
            storeAmount.AddToResource(ResourceType.Wood, 20);

            bool isStored = stockpile.Store(storeAmount, out ResourceCollection stored);

            Assert.IsFalse(isStored);
            Assert.AreEqual(0, stored.TotalResourceAmount());
        }

        [Test]
        public void StoreWrongTypeOfResource()
        {
            IStockpile stockpile = new StockPileController(ResourceType.Gold, 200);
            ResourceCollection storeAmount = new ResourceCollection();
            storeAmount.AddToResource(ResourceType.Wood, 20);

            bool isStored = stockpile.Store(storeAmount, out ResourceCollection stored);

            Assert.IsTrue(isStored);
            Assert.AreEqual(0, stored.TotalResourceAmount());
        }

        [Test]
        public void TakeWithOutExceeding()
        {
            IStockpile stockpile = new StockPileController(ResourceType.Gold, 200);
            ResourceCollection stockpileInitialCollection = new ResourceCollection();
            stockpileInitialCollection.AddToResource(ResourceType.Gold, 30);
            stockpile.Store(stockpileInitialCollection, out ResourceCollection _);
            ResourceCollection takeAmount = new ResourceCollection();
            takeAmount.AddToResource(ResourceType.Gold, 20);

            bool isTaken = stockpile.Take(takeAmount, out ResourceCollection taken);

            Assert.IsTrue(isTaken);
            Assert.AreEqual(takeAmount.TotalResourceAmount(), taken.TotalResourceAmount());
        }

        [Test]
        public void TakeWithExceeding()
        {
            IStockpile stockpile = new StockPileController(ResourceType.Gold, 200);
            ResourceCollection stockpileInitialCollection = new ResourceCollection();
            stockpileInitialCollection.AddToResource(ResourceType.Gold, 15);
            stockpile.Store(stockpileInitialCollection, out ResourceCollection _);
            ResourceCollection takeAmount = new ResourceCollection();
            takeAmount.AddToResource(ResourceType.Gold, 20);

            bool isTaken = stockpile.Take(takeAmount, out ResourceCollection taken);

            Assert.IsTrue(isTaken);
            Assert.AreNotEqual(takeAmount.TotalResourceAmount(), taken.TotalResourceAmount());
            Assert.AreEqual(15, taken.TotalResourceAmount());
        }
  
        [Test]
        public void TakeFailure() 
        {
            IStockpile stockpile = new StockPileController(ResourceType.Gold, 200);
            ResourceCollection takeAmount =  new ResourceCollection();
            takeAmount.AddToResource(ResourceType.Wood, 20);

            bool isTaken = stockpile.Take(takeAmount, out ResourceCollection taken);

            Assert.IsFalse(isTaken);
            Assert.AreEqual(0, taken.TotalResourceAmount());
        }

        [Test]
        public void TakeWrongTypeOfResource()
        {
            IStockpile stockpile = new StockPileController(ResourceType.Gold, 200);
            ResourceCollection stockpileInitialCollection = new ResourceCollection();
            stockpileInitialCollection.AddToResource(ResourceType.Gold, 30);
            stockpile.Store(stockpileInitialCollection, out ResourceCollection _);
            ResourceCollection takeAmount = new ResourceCollection();
            takeAmount.AddToResource(ResourceType.Wood, 20);

            bool isTaken = stockpile.Take(takeAmount, out ResourceCollection taken);

            Assert.IsTrue(isTaken);
            Assert.AreEqual(0, taken.TotalResourceAmount());
        }
    }

}