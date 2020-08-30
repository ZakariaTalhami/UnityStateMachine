using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace Tests
{
    public class ResourceCollectionTests
    {
        [Test]
        public void ResourceCollectionAddResource()
        {
            ResourceCollection resourceCollection = new ResourceCollection();
            resourceCollection.AddToResource(ResourceType.Stone, 20);

            Assert.AreEqual(resourceCollection.GetResourceAmount(ResourceType.Stone), 20);
        }

        [Test]
        public void ResourceCollectionFromDictionary()
        {
            Dictionary<ResourceType, int> collection = new Dictionary<ResourceType, int>();
            collection[ResourceType.Wood] = 20;
            ResourceCollection resourceCollection = new ResourceCollection(collection);

            Assert.AreEqual(resourceCollection.GetResourceAmount(ResourceType.Wood), 20);
        }

        [Test]
        public void ResourceCollectionTakeResource_Success()
        {
            ResourceCollection resourceCollection = new ResourceCollection();
            resourceCollection.AddToResource(ResourceType.Wood, 20);

            bool takeStatus = resourceCollection.TakeResource(ResourceType.Wood, 5);

            Assert.IsTrue(takeStatus);
            Assert.AreEqual(resourceCollection.GetResourceAmount(ResourceType.Wood), 15);
        }

        [Test]
        public void ResourceCollectionTakeResource_Fail()
        {
            ResourceCollection resourceCollection = new ResourceCollection();

            bool takeStatus = resourceCollection.TakeResource(ResourceType.Wood, 20);

            Assert.IsFalse(takeStatus);
            Assert.AreEqual(resourceCollection.GetResourceAmount(ResourceType.Wood), 0);
        }

        [Test]
        public void ResourceCollectionTotalAmount() 
        {
            ResourceCollection resourceCollection = new ResourceCollection();
            resourceCollection.AddToResource(ResourceType.Wood, 20);
            resourceCollection.AddToResource(ResourceType.Stone, 25);
            resourceCollection.AddToResource(ResourceType.Gold, 35);

            int totalResourceAmount = resourceCollection.TotalResourceAmount();
            
            Assert.AreEqual(totalResourceAmount, 80);
        }

        [Test]
        public void ResourceCollectionAddition() 
        {
            ResourceCollection woodResourceCollection = new ResourceCollection();
            ResourceCollection emptyResourceCollection = new ResourceCollection();
            woodResourceCollection.AddToResource(ResourceType.Wood, 20);

            ResourceCollection addedResourceCollection = emptyResourceCollection + woodResourceCollection;
            int woodAmount = addedResourceCollection.GetResourceAmount(ResourceType.Wood);

            Assert.AreEqual(woodAmount, 20);
            Assert.AreNotSame(addedResourceCollection, emptyResourceCollection);
            Assert.AreNotSame(addedResourceCollection, woodResourceCollection);
        }

        [Test]
        public void ResourceCollectionSubtraction() 
        {
            ResourceCollection requiredResourceCollection = new ResourceCollection();
            ResourceCollection storageResourceCollection = new ResourceCollection();

            requiredResourceCollection.AddToResource(ResourceType.Wood, 5);
            requiredResourceCollection.AddToResource(ResourceType.Stone, 20);
            storageResourceCollection.AddToResource(ResourceType.Wood, 20);


            storageResourceCollection -= requiredResourceCollection;
            int woodAmount = storageResourceCollection.GetResourceAmount(ResourceType.Wood);
            int stoneAmount = storageResourceCollection.GetResourceAmount(ResourceType.Stone);

            Assert.AreEqual(woodAmount, 15);
            Assert.AreEqual(stoneAmount, -20);
        }
    }
}
