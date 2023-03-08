using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using restAPI_Swagger_demo.Depository;
using restAPI_Swagger_demo.Models;

namespace restAPI_Swagger_demo.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SqliteController : ControllerBase
    {
        private readonly ICRUD_sqlite _sqlite;

        public SqliteController(ICRUD_sqlite sqlite)
        {
            _sqlite = sqlite;
        }

        [HttpGet]
        public async Task<IActionResult>? Get()
        {
            var data = await _sqlite.Gets();
            return new JsonResult(data);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult>? Get(int id)
        {
            var data = await _sqlite.Get(id);
            if (data != null)
            {
                return new JsonResult(data);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(tBook book)
        {
            var isOK = await _sqlite.Post(book);
            if (!string.IsNullOrWhiteSpace(isOK))
            {
                return BadRequest(isOK);
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(tBook book)
        {
            var isOK = await _sqlite.Put(book.Id, book);
            if (!string.IsNullOrWhiteSpace(isOK))
            {
                return BadRequest(isOK);
            }

            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<tBook> patchBook)
        {
            if (patchBook != null)
            {
                var book = await _sqlite.Get(id);

                if (book == null)
                {
                    return NotFound();
                }

                patchBook.ApplyTo(book, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var isOk = await _sqlite.Patch(id, book);
                if (string.IsNullOrWhiteSpace(isOk))
                {
                    return Ok();
                }
                else
                {
                    return NotFound(isOk);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isOk = await _sqlite.Delete(id);
            if (string.IsNullOrWhiteSpace(isOk))
            {
                return Ok();
            }
            else
            {
                return NotFound(isOk);
            }
        }
    }
}
