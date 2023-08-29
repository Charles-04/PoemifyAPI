using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poemify.Models.DTOs.Request
{
    public record RequestParams
    {
        private int _page;
        private int _pageSize;
        private string? _key;

        public int Page { get { return _page; } set { _page = value; } }
        public int PageSize { get { return _pageSize; } set { _pageSize = value; } }
        public string? Key { get { return _key; } set { _key = value; } }
    }
}
