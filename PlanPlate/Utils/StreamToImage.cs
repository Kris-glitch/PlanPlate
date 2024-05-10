namespace PlanPlate.Utils
{
    public static class StreamToImage
    {
        public static async Task<string> ConvertStreamToImage(Stream imageStram)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                await imageStram.CopyToAsync(ms);
                byte[] imageBytes = ms.ToArray();


                string base64Image = Convert.ToBase64String(imageBytes);

                return $"data:image/png;base64,{base64Image}";
                
            }
        }
    }
}
