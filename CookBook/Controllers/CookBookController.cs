using CookBook.Model;
using CookBook.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CookBook.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CookBookController : ControllerBase
    {
       private  IRecipeRepository _repository;
        public CookBookController(IRecipeRepository repository)
        {
            this._repository = repository;
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRecipe(int? id)
        {
            if (id == null)
                return BadRequest();
            try
            {
                var recipe = _repository.GetRecipeById(id);
                if (recipe == null)
                    return NotFound();
                return Ok(recipe);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("")]
        public IActionResult GetAllRecipies()
        {
            try
            {
                var recipes = _repository.GetAllRecipes();
                if (recipes == null)
                    return NotFound();
                return Ok(recipes);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("AddRecipe")]
        public IActionResult AddRecipe([FromBody]Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.AddRecipe(recipe);
                    return Ok();

                }
                catch (Exception)
                {
                   return BadRequest();
                }
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateRecipe([FromBody]Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.UpdateRecipe(recipe);
                    return Ok();
                }
                catch (Exception)
                {

                    return BadRequest();
                }
                
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRecipe(int? id)
        {
            if (id == null)
                return BadRequest();
            try
            {
                _repository.DeleteRecipe(id);
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
