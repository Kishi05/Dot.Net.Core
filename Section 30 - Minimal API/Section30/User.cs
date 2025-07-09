using System.Runtime.CompilerServices;

namespace Section30
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"ID : {Id} - Name {Name}";
        }
    }
}
