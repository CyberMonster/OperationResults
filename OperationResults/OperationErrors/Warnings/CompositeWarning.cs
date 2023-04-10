using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OperationResults.OperationErrors.Warnings
{
    public class AggregateWarning : Warning
    {
        public Stack<Warning> Warnings { get; } = new();

        public AggregateWarning(string messsage = null) : base(messsage) { }
        public AggregateWarning(Stack<Warning> warnings, string messsage = null) : base(messsage)
            => Warnings = warnings;

        public void Add(Warning warning)
            => Warnings.Push(warning);

        public override string ToString()
        {
            var builder = new StringBuilder(base.ToString());
            var warningNumber = 0;

            if (Warnings?.Any() ?? false)
            {
                builder.Append("\nInner warnings:\n");
                foreach (var warning in Warnings)
                {
                    builder.AppendLine().Append(warningNumber).Append(". ").Append(warning.ToString());
                    ++warningNumber;
                }
            }

            return builder.ToString();
        }
    }
}
