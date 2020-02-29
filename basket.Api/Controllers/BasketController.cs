using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using basket.Api.Data;
using basket.Api.Dtos;
using basket.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Basket.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {

        public IBasketGameRepository _repo { get; }

        public BasketController(IBasketGameRepository repo)
        {
            _repo = repo;
        }

        // POST 
        [HttpPost("add")]
        public async Task<IActionResult> Add(BasketGameDto basketGameDto)
        {

            int _points = 0;

            int.TryParse(basketGameDto.Points, out _points);


            if (_points < 0)
            {
                return BadRequest("A pontuação deve ser igual ou maior que (0)zero");
            }

            try
            {
                var gameToCreate = new BasketGame
                {
                    GameDate = basketGameDto.GameDate,
                    Points = _points
                };

                var createdGame = await _repo.Add(gameToCreate);
                await _repo.AddRecord(createdGame);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao tentar registar jogo: " + ex.Message);
            }

        }

        // GET 
        [HttpGet("gameResult")]
        public async Task<IActionResult> GetGamesResult()
        {

            try
            {
                object gamesResult = null;

                var games = await _repo.List();
                var recordList = await _repo.ListRecords();
                var gamesPlayed = games.Count();
                var totalPoints = games.Sum(s => s.Points);
                var averagePoints = totalPoints / gamesPlayed;
                var lowestScore = games.OrderBy(o => o.Points).Select(s => s.Points).FirstOrDefault();
                var highestScore = games.OrderByDescending(o => o.Points).Select(s => s.Points).FirstOrDefault();
                var records = recordList.Count();
                var firstGameDate = games.OrderBy(o => o.GameDate).Select(s => s.GameDate).FirstOrDefault().ToString("dd/MM/yyyy");
                var lastGameDate = games.OrderByDescending(o => o.GameDate).Select(s => s.GameDate).FirstOrDefault().ToString("dd/MM/yyyy");

                gamesResult = new
                {
                    gamesPlayed,
                    totalPoints,
                    averagePoints,
                    highestScore,
                    lowestScore,
                    records,
                    firstGameDate,
                    lastGameDate
                };

                return Ok(gamesResult);
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao tentar recuperar resultados: " + ex.Message);
            }
        }
    }
}