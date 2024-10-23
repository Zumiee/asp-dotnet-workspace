namespace WebApplication1.Entities;

public class Game
{
    public int Id { get; set; } 

    public required string Name { get; set; }   

    public int GenreID { get; set; }    

    public Genre? Genre{ get; set; }        
}
