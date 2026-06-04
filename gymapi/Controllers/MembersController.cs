using GymManagement.Api.DTOs.Member;
using GymManagement.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GymManagement.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/members")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? query)
        {
            var members = await _memberService.GetAllMembersAsync(query);
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _memberService.GetMemberByIdAsync(id);
            if (member == null)
            {
                return NotFound(new { message = $"Member with ID {id} not found." });
            }
            return Ok(member);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMemberDto createMemberDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newMember = await _memberService.CreateMemberAsync(createMemberDto);
            return CreatedAtAction(nameof(GetById), new { id = newMember.MemberId }, newMember);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateMemberDto updateMemberDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _memberService.UpdateMemberAsync(id, updateMemberDto);
            if (!updated)
            {
                return NotFound(new { message = $"Member with ID {id} not found." });
            }

            return Ok(new { message = "Member updated successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _memberService.DeleteMemberAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = $"Member with ID {id} not found." });
            }

            return Ok(new { message = "Member deleted successfully." });
        }
    }
}
