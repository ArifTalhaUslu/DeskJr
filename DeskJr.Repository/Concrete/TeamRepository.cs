using System;
using CoordinateApp.Repositories.Concrete;
using DeskJr.Data;
using DeskJr.Entity.Models;
using DeskJr.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DeskJr.Repository.Concrete
{
	public class TeamRepository : GenericRepository<Team>, ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

