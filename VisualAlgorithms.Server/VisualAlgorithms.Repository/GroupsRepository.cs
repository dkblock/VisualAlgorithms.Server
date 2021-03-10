﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VisualAlgorithms.Database;
using VisualAlgorithms.Entities;

namespace VisualAlgorithms.Repository
{
    public class GroupsRepository
    {
        private readonly ApplicationContext _context;

        public GroupsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<int> AddGroup(GroupEntity group)
        {
            var result = await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<IEnumerable<GroupEntity>> GetAllGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<IEnumerable<GroupEntity>> GetGroups(Expression<Func<GroupEntity, bool>> predicate)
        {
            return await _context.Groups.Where(predicate).ToListAsync();
        }

        public async Task<GroupEntity> GetGroupById(int id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task RemoveGroup(int id)
        {
            var group = await GetGroupById(id);
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGroup(GroupEntity group)
        {
            var groupToUpdate = await GetGroupById(group.Id);
            _context.Entry(groupToUpdate).CurrentValues.SetValues(group);
            await _context.SaveChangesAsync();
        }
    }
}
