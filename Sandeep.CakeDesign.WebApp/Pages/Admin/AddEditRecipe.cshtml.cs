using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sandeep.CakeDesign.Repository.DataAccessLayer;

namespace Sandeep.CakeDesign.WebApp.Pages.Admin
{
    [Authorize]
    public class AddEditRecipeModel : PageModel
    {
        private readonly IRecipesService _recipesService;

        [FromRoute]
        public long? Id { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please upload an Image")]
        public IFormFile Image { get; set; }

        public bool IsNewRecipe => (Id == null);

        [BindProperty]
        public Recipe Recipe { get; set; }

        public AddEditRecipeModel(IRecipesService recipesService)
        {
            _recipesService = recipesService;
        }
        public async Task OnGetAsync()
        {
            Recipe = await _recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var recipe = await _recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();

            recipe.Name = Recipe.Name;
            recipe.Description = Recipe.Description;
            recipe.Directions = Recipe.Directions;
            await UpdateImageData(recipe);
            await _recipesService.SaveAsync(recipe);
            return RedirectToPage("/Recipe", new { id = recipe.Id });
        }

        private async Task UpdateImageData(Recipe recipe)
        {
            if (Image == null) return;

            await using var stream = new System.IO.MemoryStream();
            await Image.CopyToAsync(stream);
            recipe.Image = stream.ToArray();
            recipe.ImageContentType = Image.ContentType;
        }

        public async Task<IActionResult> OnPostDelete()
        {
            if (!Id.HasValue) return RedirectToPage("/Index");
            await _recipesService.DeleteAsync(Id.Value);
            return RedirectToPage("/Index");

        }
    }
}