using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static System.Environment;

namespace Sandeep.CakeDesign.Repository.DataAccessLayer
{
    public class Recipe
    {
        public long Id { get; set; }
        [Required(ErrorMessage ="Please Enter Name of a Recipe")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Description of a Recipe")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Enter Directions of a Recipe")]
        public string Directions { get; set; }
        [Required(ErrorMessage = "Please Enter Ingredients of a Recipe")]
        public string Ingredients { get; set; }

        public IEnumerable<string> DirectionsList => (Directions ?? string.Empty).Split(NewLine);

        public IEnumerable<string> IngredientsList => (Ingredients ?? string.Empty).Split(NewLine);

        #region Image

        public byte[] Image { get; set; }

        public string ImageContentType { get; set; }

        public string GetInlineImageSrc ()
        {
            if (Image == null || ImageContentType == null)
                return null;

            var base64Image = Convert.ToBase64String(Image);
            return $"data:{ImageContentType};base64,{base64Image}";
        }

        public void SetImage(IFormFile file)
        {
            if (file == null)
                return;

            ImageContentType = file.ContentType;

            using var stream = new System.IO.MemoryStream();
            file.CopyTo(stream);
            Image = stream.ToArray();
        }

        #endregion
    }
}
