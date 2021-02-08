﻿namespace VisualAlgorithms.Entities
{
    public class AlgorithmTimeComplexity
    {
        public int Id { get; set; }
        public string SortingBestTime { get; set; }
        public string SortingAverageTime { get; set; }
        public string SortingWorstTime { get; set; }
        public string SearchingAverageTime { get; set; }
        public string SearchingWorstTime { get; set; }
        public string InsertionAverageTime { get; set; }
        public string InsertionWorstTime { get; set; }
        public string DeletionAverageTime { get; set; }
        public string DeletionWorstTime { get; set; }
        public int AlgorithmId { get; set; }
    }
}
