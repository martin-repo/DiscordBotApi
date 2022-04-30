// -------------------------------------------------------------------------------------------------
// <copyright file="QueryBuilder.cs" company="kpop.fan">
//   Copyright (c) kpop.fan. All rights reserved.
// </copyright>
// -------------------------------------------------------------------------------------------------

namespace DiscordBotApi.Rest
{
    using System.Globalization;
    using System.Text;

    internal class QueryBuilder
    {
        private readonly StringBuilder _builder = new();

        private bool _hasQuery;

        public QueryBuilder(string pathWithoutQuery)
        {
            _builder.Append(pathWithoutQuery);
        }

        public void Add(string key, bool? value)
        {
            if (value == null)
            {
                return;
            }

            Add(key, value.Value.ToString());
        }

        public void Add(string key, int? value)
        {
            if (value == null)
            {
                return;
            }

            Add(key, value.Value.ToString(CultureInfo.InvariantCulture));
        }

        public void Add(string key, double? value)
        {
            if (value == null)
            {
                return;
            }

            Add(key, value.Value.ToString(CultureInfo.InvariantCulture));
        }

        public void Add(string key, ulong? value)
        {
            if (value == null)
            {
                return;
            }

            Add(key, value.Value.ToString());
        }

        public void Add(string key, string? value)
        {
            if (value == null)
            {
                return;
            }

            AddQuerySeparator();
            _builder.Append($"{key}={value}");
        }

        public override string ToString()
        {
            return _builder.ToString();
        }

        private void AddQuerySeparator()
        {
            if (_hasQuery)
            {
                _builder.Append('&');
                return;
            }

            _builder.Append('?');
            _hasQuery = true;
        }
    }
}