using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Web.Data;
using Movies.Web.Models;
using Movies.Web.ViewModels;
using NToastNotify;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toastNotification;
        private List<string> _allowExtensions = new List<string>() { ".png", ".jpg" };
        private long _maxAllowedPosterSize = 2097152;
        public MoviesController(ApplicationDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        public async Task<ActionResult> Index()
        {
            var allMovies = await _context.Movies.OrderByDescending(m => m.Rate)
                .ToListAsync();
            return View(allMovies);
        }

        public async Task<ActionResult> Create()
        {
            var viewModel = new MovieFormViewModel()
            {
                Genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync(),
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
                return View("MovieForm", model);
            }

            var files = Request.Form.Files;

            if (!files.Any())
            {
                model.Genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Please select moive poster!");
                return View("MovieForm", model);
            }

            var poster = files.FirstOrDefault();

            if (poster is null)
            {
                model.Genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Please select moive poster!");
                return View("MovieForm", model);
            }
            if (!_allowExtensions.Contains(Path.GetExtension(poster.FileName).ToLower()))
            {
                model.Genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Only .PNG .JPG Images are allowed!");
                return View("MovieForm", model);
            }

            if (poster.Length > _maxAllowedPosterSize)
            {
                model.Genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
                ModelState.AddModelError("Poster", "Poster cannot be more than 1 MB!");
                return View("MovieForm", model);
            }

            using var dataStream = new MemoryStream();
            await poster.CopyToAsync(dataStream);

            _context.Movies.Add(new Movie()
            {
                Title = model.Title,
                GenreId = model.GenreId,
                Year = model.Year,
                Rate = model.Rate,
                StoryLine = model.StoryLine,
                Trailer = model.Trailer,
                Poster = dataStream.ToArray(),
            });

            _context.SaveChanges();

            _toastNotification.AddSuccessToastMessage("Movie created successfully");
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var movie = await _context.Movies.FindAsync(id);

            if (movie is null) return NotFound();

            var viewModel = new MovieFormViewModel()
            {
                Id = movie.Id,
                GenreId = movie.GenreId,
                Rate = movie.Rate,
                Title = movie.Title,
                Poster = movie.Poster,
                StoryLine = movie.StoryLine,
                Year = movie.Year,
                Trailer = movie.Trailer,
                Genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync(),
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MovieFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
                return View("MovieForm", model);
            }

            var movie = await _context.Movies.FindAsync(model.Id);

            if (movie is null) return NotFound();

            var files = Request.Form.Files;
            if (files.Any())
            {
                var poster = files.FirstOrDefault();

                if (poster is null)
                {
                    model.Genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
                    ModelState.AddModelError("Poster", "Please select moive poster!");
                    return View("MovieForm", model);
                }

                using var dataStream = new MemoryStream();
                await poster.CopyToAsync(dataStream);

                model.Poster = dataStream.ToArray();


                if (!_allowExtensions.Contains(Path.GetExtension(poster.FileName).ToLower()))
                {
                    model.Genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
                    ModelState.AddModelError("Poster", "Only .PNG .JPG Images are allowed!");
                    return View("MovieForm", model);
                }

                if (poster.Length > _maxAllowedPosterSize)
                {
                    model.Genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
                    ModelState.AddModelError("Poster", "Poster cannot be more than 1 MB!");
                    return View("MovieForm", model);
                }

                movie.Poster = model.Poster;
            }

            movie.Title = model.Title;
            movie.GenreId = model.GenreId;
            movie.Rate = model.Rate;
            movie.StoryLine = model.StoryLine;
			movie.Trailer = model.Trailer;
            movie.Year = model.Year;

            _context.SaveChanges();
            _toastNotification.AddSuccessToastMessage("Movie updated successfully");
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return BadRequest();

            var movie = await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);

            if (movie is null) return NotFound();

            return View(movie);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var movie = await _context.Movies.FindAsync(id);

            if (movie is null) return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok();
        }
    }
}
