namespace InstrumentSite.Utilities
{
    public class SaveImageUtil
    {
        public static async Task<string> SaveImageFileAsync(IFormFile imageFile)
        {
            var uploadsDirectory = Path.Combine("wwwroot", "uploads", "products");
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var filePath = Path.Combine(uploadsDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return relative path
            return Path.Combine("uploads/products", fileName).Replace("\\", "/");
        }


    }
}
