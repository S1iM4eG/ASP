namespace ToDoList
{
    public class TODO
    {
        public string Description { get; set; }
        public bool DONE { get; set; }
        public DateTime AssignTime { get; set; }
        public override bool Equals(object? other)
        {
            return other is TODO ?
                this.Description.Equals((other as TODO).Description, StringComparison.OrdinalIgnoreCase) :
                false;
        }
    }
}
