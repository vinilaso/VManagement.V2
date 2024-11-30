using System.Text;

namespace VManagement.Commons.Utility
{
    public class DelimitedStringBuilder
    {
        private const string DEFAULT_DELIMITER = " ";
        private readonly string _delimiter;
        private readonly StringBuilder _builder = new StringBuilder();
        private bool InParenthesis = false;

        public DelimitedStringBuilder() 
        {
            _delimiter = DEFAULT_DELIMITER;
        }

        public DelimitedStringBuilder(string initialValue) : this()
        {
            _builder = new StringBuilder(initialValue);
        }

        public DelimitedStringBuilder(string initialValue, string delimiter) : this(initialValue)
        {
            _delimiter = delimiter;
        }

        public DelimitedStringBuilder Append(string value)
        {
            if (value.IsNullOrEmpty())
                return this;

            if (_builder.Length > 0)
                _builder.Append(_delimiter);

            _builder.Append(value);
            return this;
        }

        public DelimitedStringBuilder Append(char value)
        {
            if (_builder.Length > 0)
                _builder.Append(_delimiter);

            _builder.Append(value);
            return this;
        }

        public DelimitedStringBuilder OpenParenthesis()
        {
            if (!InParenthesis)
                this.Append('(');

            InParenthesis = true;
            return this;
        }

        public DelimitedStringBuilder CloseParenthesis()
        {
            if (InParenthesis)
                this.Append(')');

            InParenthesis = false;
            return this;
        }

        public DelimitedStringBuilder AppendJoin(string separator, IEnumerable<string> values)
        {
            this.Append(string.Join(separator, values));
            return this;
        }

        public override string ToString() => _builder.ToString();

        public void Clear() => _builder.Clear();
    }
}
