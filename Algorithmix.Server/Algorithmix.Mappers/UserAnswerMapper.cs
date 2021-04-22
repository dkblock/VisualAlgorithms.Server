﻿using Algorithmix.Entities;
using Algorithmix.Models.Tests;
using System.Collections.Generic;
using System.Linq;

namespace Algorithmix.Mappers
{
    public class UserAnswerMapper
    {
        public UserAnswerData ToData(UserAnswerPayload userAnswerPayload, string userId)
        {
            return new UserAnswerData
            {
                Answers = userAnswerPayload.Answers,
                QuestionId = userAnswerPayload.QuestionId,
                UserId = userId
            };
        }

        public UserAnswerEntity ToEntity(UserAnswerData userAnswerData)
        {
            return new UserAnswerEntity
            {
                QuestionId = userAnswerData.QuestionId,
                UserId = userAnswerData.UserId,
                Value = userAnswerData.Value,
                IsCorrect = userAnswerData.IsCorrect
            };
        }

        public UserAnswer ToDomain(UserAnswerEntity userAnswerEntity)
        {
            return new UserAnswer
            {
                Value = userAnswerEntity.Value,
                IsCorrect = userAnswerEntity.IsCorrect,
                Question = new TestQuestion { Id = userAnswerEntity.QuestionId }
            };
        }

        public IEnumerable<UserAnswer> ToDomainCollection(IEnumerable<UserAnswerEntity> userAnswerEntities)
        {
            return userAnswerEntities.Select(ua => ToDomain(ua));
        }
    }
}
