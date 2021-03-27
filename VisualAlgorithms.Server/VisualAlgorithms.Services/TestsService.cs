﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisualAlgorithms.Mappers;
using VisualAlgorithms.Models.Tests;
using VisualAlgorithms.Repository;

namespace VisualAlgorithms.Services
{
    public class TestService
    {
        private readonly TestMapper _testMapper;
        private readonly TestRepository _testRepository;

        public TestService(TestMapper testMapper, TestRepository testRepository)
        {
            _testMapper = testMapper;
            _testRepository = testRepository;
        }

        public async Task<Test> CreateTest(TestPayload testPayload)
        {
            var testEntity = _testMapper.ToEntity(testPayload);
            var createdTest = await _testRepository.CreateTest(testEntity);

            return _testMapper.ToModel(createdTest);
        }

        public async Task<bool> Exists(int id)
        {
            return await _testRepository.GetTestById(id) != null;
        }

        public async Task<Test> GetTest(int id)
        {
            var testEntity = await _testRepository.GetTestById(id);
            return _testMapper.ToModel(testEntity);
        }

        public async Task<IEnumerable<Test>> GetTests()
        {
            var testEntities = await _testRepository.GetAllTests();
            return _testMapper.ToModelsCollection(testEntities);
        }

        public async Task<IEnumerable<Test>> GetTests(string algorithmId)
        {
            var testEntities = await _testRepository.GetTests(t => t.AlgorithmId == algorithmId);
            return _testMapper.ToModelsCollection(testEntities);
        }

        public async Task<IEnumerable<Test>> GetTests(IEnumerable<string> algorithmIds)
        {
            var testEntities = await _testRepository.GetTests(t => algorithmIds.Contains(t.AlgorithmId));
            return _testMapper.ToModelsCollection(testEntities);
        }

        public async Task DeleteTest(int id)
        {
            await _testRepository.DeleteTest(id);
        }

        public async Task<Test> UpdateTest(int id, TestPayload testPayload)
        {
            var testEntity = _testMapper.ToEntity(testPayload, id);
            var updatedTest = await _testRepository.UpdateTest(testEntity);

            return _testMapper.ToModel(updatedTest);
        }
    }
}
