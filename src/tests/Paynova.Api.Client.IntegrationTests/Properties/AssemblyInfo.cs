using System.Reflection;
using System.Runtime.InteropServices;
using Xunit;

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("1b53ad0d-5a05-4cd3-b049-4d3d71c001d6")]


// https://github.com/tomaszeman/Xunit.Extensions.Ordering#setup-ordering
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]