namespace WebApplication1.DTOs
{
    /*
     * Что DTO, что Domain Model абсолютно идентичны, но 
     * сделаем дто потому что почему бы и нет
     */
    public class GenreDTO
    {
        public int id { get; set; }
        public string GenreName { get; set; }
    }
}
