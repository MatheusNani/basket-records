using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using basket.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace basket.Api.Data
{
    public class BasketGameRepository : IBasketGameRepository
    {

        private readonly DataContext _context;

        public BasketGameRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<BasketGame> Add(BasketGame game)
        {

            await _context.AddAsync(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<RecordGame> AddRecord(BasketGame newGame)
        {
            if (CheckRecord(newGame).Result != null)
            {
                var record = new RecordGame
                {
                    Points = newGame.Points
                };

                await _context.AddAsync(record);
                await _context.SaveChangesAsync();
                return record;
            }

            return null;
        }

        private async Task<BasketGame> CheckRecord(BasketGame newGame)
        {
            var gameList = await _context.BasketGame.ToListAsync();

            if (gameList.Count > 1)
            {
                var recordList = await _context.RecordGame.ToListAsync();

                if (recordList.Count >= 1)
                {
                    var lastRecord = recordList.OrderByDescending(o => o.Id).FirstOrDefault();

                    if (newGame.Points > lastRecord.Points)
                    {
                        return newGame;
                    }
                }
                else
                {
                    gameList.OrderBy(o => o.Id);
                    var oldGame = gameList[gameList.Count - 2];
                    if (newGame.Points > oldGame.Points)
                    {
                        return newGame;
                    }
                }
            }

            return null;

        }

        public Task<List<BasketGame>> List()
        {

            return _context.BasketGame.ToListAsync();
        }

        public async Task<List<RecordGame>> ListRecords()
        {
            return await _context.RecordGame.ToListAsync();
        }
    }
}