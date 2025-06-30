using Services.Interface;

namespace Services
{
    public class ProgramLanguageService : IProgramLanguageService
    {
        public Guid id { get; set; }
        private List<string> languages = new List<string>() {
            "C",
            "C++",
            "C#",
            "JAVA",
            "GO"
        };
        public ProgramLanguageService()
        {
            id = Guid.NewGuid();
        }
        public List<string> GetLanguages()
        {
            return languages;
        }

        public Guid GetID()
        {
            return id;
        }
    }
}
