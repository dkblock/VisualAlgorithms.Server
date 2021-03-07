﻿using VisualAlgorithms.Domain;
using VisualAlgorithms.Entities;

namespace VisualAlgorithms.Mappers
{
    public class AlgorithmTimeComplexitiesMapper
    {
        public AlgorithmTimeComplexity ToDomain(AlgorithmTimeComplexityEntity timeComplexityEntity)
        {
            if (timeComplexityEntity == null)
                return null;

            return new AlgorithmTimeComplexity
            {
                Id = timeComplexityEntity.Id,
                AlgorithmId = timeComplexityEntity.AlgorithmId,
                DeletionAverageTime = timeComplexityEntity.DeletionAverageTime,
                DeletionWorstTime = timeComplexityEntity.DeletionWorstTime,
                InsertionAverageTime = timeComplexityEntity.InsertionAverageTime,
                InsertionWorstTime = timeComplexityEntity.InsertionWorstTime,
                SearchingAverageTime = timeComplexityEntity.SearchingAverageTime,
                SearchingWorstTime = timeComplexityEntity.SearchingWorstTime,
                SortingAverageTime = timeComplexityEntity.SortingAverageTime,
                SortingBestTime = timeComplexityEntity.SortingBestTime,
                SortingWorstTime = timeComplexityEntity.SortingWorstTime
            };
        }

        public AlgorithmTimeComplexityEntity ToEntity(AlgorithmTimeComplexity timeComplexity)
        {
            return new AlgorithmTimeComplexityEntity
            {
                Id = timeComplexity.Id,
                AlgorithmId = timeComplexity.AlgorithmId,
                DeletionAverageTime = timeComplexity.DeletionAverageTime,
                DeletionWorstTime = timeComplexity.DeletionWorstTime,
                InsertionAverageTime = timeComplexity.InsertionAverageTime,
                InsertionWorstTime = timeComplexity.InsertionWorstTime,
                SearchingAverageTime = timeComplexity.SearchingAverageTime,
                SearchingWorstTime = timeComplexity.SearchingWorstTime,
                SortingAverageTime = timeComplexity.SortingAverageTime,
                SortingBestTime = timeComplexity.SortingBestTime,
                SortingWorstTime = timeComplexity.SortingWorstTime
            };
        }
    }
}