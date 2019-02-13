using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ticTacToeLogic;

namespace assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ticController : ControllerBase
    {
        [HttpPost]
        public void Post([FromBody] char[] game)
        {
            logicEngineBase input = new logicEngineBase()
            {
                gameBoardResponse = game
            };

            logicEngine engineOne = new logicEngine();

            engineOne.ExecuteMove(input.gameBoardResponse);


        


        }
    }
}
