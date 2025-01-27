namespace InstrumentSite.Utilities
{
    public class SaveImageUtil
    {
        public static async Task<string> SaveImageFileAsync(IFormFile imageFile, HttpRequest request)
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

            // Build full URL
            var baseUrl = $"{request.Scheme}://{request.Host}";
            return Path.Combine(baseUrl, "uploads/products", fileName).Replace("\\", "/");
        }



    }
}
