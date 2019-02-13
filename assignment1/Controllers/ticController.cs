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
        /// <summary>
        /// This method takes the input payload and returns the next gameboar
        /// </summary>
        /// <param name="game">The existing gameboard positions.</param>  

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
