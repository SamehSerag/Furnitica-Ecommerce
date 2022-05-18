using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AngularProject.Data;
using DotNetWebAPI.Models;
using DotNetWebAPI.Services;
using DotNetWebAPI.DTOs;
using AngularAPI.Dtos;
using AutoMapper;

namespace DotNetWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewRepository _repo;
        private readonly IMapper _mapper;


        public ReviewsController(IReviewRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<PaginationMetaData<ReviewDto>>> GetReviews([FromQuery] ReviewSearchModel searchModel)
        {
            var reviews = await _repo.GetAllReviewsAsync(searchModel);

            var reviewDto = _mapper.Map<IReadOnlyList<Review>, IReadOnlyList<ReviewDto>>
             (reviews);

            int count = _repo.CountAsync(searchModel).Result;

            PaginationMetaData<ReviewDto> paginationMetaData =
                new PaginationMetaData<ReviewDto>
                (count, searchModel.PageIndex,
                searchModel.PageSize,
                reviewDto);

            return paginationMetaData;

        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
            var review = await _repo.GetReviewByIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        [HttpGet("/User/{id}")]
        public async Task<ActionResult<Review>> GetUserReview(string id)
        {
            var review = await _repo.GetReviewByUserIdAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }


            try
            {
                await _repo.UpdateReviewAsync(review);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Review>> PostReview(Review review)
        {
            await _repo.AddReviewAsync(review);

            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            
            var review = await _repo.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            await _repo.DeleteReviewAsync(review);

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _repo.IsReviewExixtsAsync(id);
        }
    }
}
