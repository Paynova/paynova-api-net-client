using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Paynova.Api.Client.Testing
{
    public class MyFactAttribute : FactAttribute
    {
        internal const string PriorityMemberName = "Priority";

        public int Priority { get; set; }

        public MyFactAttribute()
        {
            Priority = 0;
        }
    }
}
