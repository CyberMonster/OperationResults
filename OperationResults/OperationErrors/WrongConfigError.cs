namespace OperationResults.OperationErrors
{
    public class WrongConfigError : OperationError
    {
        public string SectionName { get; }

        public WrongConfigError(string sectionName) : base($"Wrong configure detected. Wrong section: {sectionName}")
            => SectionName = sectionName;

        public WrongConfigError(string sectionName, string message) : base(message)
            => SectionName = sectionName;

        public override string Message
        {
            get => $"Wrong configure detected. Wrong section: {SectionName}{(string.IsNullOrWhiteSpace(base.Message) ? $" AdditionalData: {base.Message}" : string.Empty)}";
            protected set => base.Message = value;
        }
    }
}
