﻿using System.Collections.Generic;
using System.Linq;
using VisualAlgorithms.Entities;
using VisualAlgorithms.Models.Algorithms;
using VisualAlgorithms.Models.Tests;

namespace VisualAlgorithms.Mappers
{
    public class TestsMapper
    {
        public TestEntity ToEntity(TestPayload testPayload, int? id = null)
        {
            return new TestEntity
            {
                Id = id ?? 0,
                Name = testPayload.Name,
                AlgorithmId = testPayload.AlgorithmId
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
                Algorithm = new Algorithm() { Id = testEntity.AlgorithmId },
                Questions = new List<TestQuestion>()
            };
        }

        public IEnumerable<Test> ToModelsCollection(IEnumerable<TestEntity> testEntities)
        {
            return testEntities.Select(entity => ToModel(entity));
        }
    }
}
