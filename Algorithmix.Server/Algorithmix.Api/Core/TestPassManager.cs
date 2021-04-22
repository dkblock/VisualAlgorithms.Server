﻿using Algorithmix.Mappers;
using Algorithmix.Models.Tests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithmix.Api.Core
{
    public class TestPassManager
    {
        private readonly TestManager _testManager;
        private readonly TestQuestionManager _questionManager;
        private readonly UserAnswerMapper _userAnswerMapper;
        private readonly UserAnswerManager _userAnswerManager;
        private readonly UserTestResultManager _userTestResultManager;

        public TestPassManager(
            TestManager testManager,
            TestQuestionManager questionManager,
            UserAnswerMapper userAnswerMapper,
            UserAnswerManager userAnswerManager,
            UserTestResultManager userTestResultManager)
        {
            _testManager = testManager;
            _questionManager = questionManager;
            _userAnswerMapper = userAnswerMapper;
            _userAnswerManager = userAnswerManager;
            _userTestResultManager = userTestResultManager;
        }

        public async Task<TestQuestion> GetNextTestQuestion(UserAnswerPayload userAnswerPayload, int testId, string userId)
        {
            if (userAnswerPayload == null)
                return await HandleTestStart(testId, userId);

            var currentQuestion = await _questionManager.GetTestQuestion(userAnswerPayload.QuestionId);
            var userAnswerData = _userAnswerMapper.ToData(userAnswerPayload, userId);
            await _userAnswerManager.CreateUserAnswer(userAnswerData, currentQuestion);

            if (currentQuestion.NextQuestionId != null)
                return await _questionManager.GetTestQuestion((int)currentQuestion.NextQuestionId);
            else
            {
                await _userTestResultManager.CreateUserTestResult(testId, userId);
                return null;
            }
        }

        private async Task<TestQuestion> HandleTestStart(int testId, string userId)
        {
            var test = await _testManager.GetTest(testId);
            var firstQuestion = await _questionManager.GetTestQuestion(test.Questions.First().Id);
            var questionIds = test.Questions.Select(q => q.Id);

            await _userAnswerManager.DeleteUserAnswers(questionIds, userId);

            return firstQuestion;
        }

        public async Task<TestQuestion> GetPreviousTestQuestion(int currentQuestionId, string userId)
        {
            var currentQuestion = await _questionManager.GetTestQuestion(currentQuestionId);

            if (currentQuestion.PreviousQuestionId == null)
                return null;

            var previousQuestion = await _questionManager.GetTestQuestion((int)currentQuestion.PreviousQuestionId);
            await _userAnswerManager.DeleteUserAnswers(new List<int> { previousQuestion.Id }, userId);

            return previousQuestion;
        }
    }
}
