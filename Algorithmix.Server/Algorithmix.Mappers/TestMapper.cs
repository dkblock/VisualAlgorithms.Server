﻿using Algorithmix.Entities.Test;
using Algorithmix.Models.Algorithms;
using Algorithmix.Models.Tests;
using Algorithmix.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithmix.Mappers
{
    public class TestMapper
    {
        public TestEntity ToEntity(TestPayload testPayload)
        {
            return new TestEntity
            {
                Name = testPayload.Name,
                IsPublished = false,
                UpdatedDate = DateTime.Now,
                AlgorithmId = testPayload.AlgorithmId
            };
        }

        public TestEntity UpdateEntity(TestEntity testEntity, TestPayload testPayload)
        {
            testEntity.Name = testPayload.Name;
            testEntity.AlgorithmId = testPayload.AlgorithmId;
            testEntity.IsPublished = false;
            testEntity.UpdatedDate = DateTime.Now;

            return testEntity;
        }

        public PublishedTestEntity ToEntity(Test test)
        {
            return new PublishedTestEntity
            {
                Id = test.Id,
                Name = test.Name,
                CreatedDate = DateTime.Now,
                CreatedBy = test.CreatedBy.Id,
                AlgorithmId = test.Algorithm.Id
            };
        }

        public Test ToModel(TestEntity testEntity)
        {
            if (testEntity == null)
                return null;

            return new Test
            {
                Id = testEntity.Id,
                Name = testEntity.Name,
                IsPublished = testEntity.IsPublished,
                CreatedDate = testEntity.CreatedDate,
                UpdatedDate = testEntity.UpdatedDate,
                CreatedBy = new ApplicationUser { Id = testEntity.CreatedBy },
                Algorithm = new Algorithm() { Id = testEntity.AlgorithmId },
                Questions = new List<TestQuestion>()
            };
        }

        public Test ToModel(PublishedTestEntity testEntity)
        {
            if (testEntity == null)
                return null;

            return new Test
            {
                Id = testEntity.Id,
                Name = testEntity.Name,
                CreatedDate = testEntity.CreatedDate,
                CreatedBy = new ApplicationUser { Id = testEntity.CreatedBy },
                Algorithm = new Algorithm() { Id = testEntity.AlgorithmId },
                Questions = new List<TestQuestion>()
            };
        }        

        public IEnumerable<Test> ToModelsCollection(IEnumerable<TestEntity> testEntities)
        {
            return testEntities.Select(entity => ToModel(entity));
        }

        public IEnumerable<Test> ToModelsCollection(IEnumerable<PublishedTestEntity> testEntities)
        {
            return testEntities.Select(entity => ToModel(entity));
        }
    }
}
