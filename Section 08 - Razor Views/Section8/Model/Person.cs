namespace Section8.Model
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public Lobby lobby { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Others 
    }

    public enum Lobby
    {
        General,
        VIP,
        Waiting,
        Hold
    }

}
