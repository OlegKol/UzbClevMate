namespace UzClevMate.MvcLogic.Apps.WebApp._Common.CommonViewModels
{
    public class ListOptionViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ListOptionViewModel()
        {

        }

        public ListOptionViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}