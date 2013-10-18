using System.Collections.Generic;

namespace MyProducts.Web.Api.DTOs.Personalization
{
    public class UiClaimsData
    {
        public string UserName { get; set; }
        public Capabilities Capabilities { get; set; }
        public Constraints Constraints { get; set; }
        public NameValueClaims NameValueClaims { get; set; }
    }

    public class Capabilities : List<string>
    { }

    public class Constraints : List<Constraint>
    { }

    public class Constraint
    {
        public string Name { get; set; }
    }

    public class NumericConstraint : Constraint
    {
        public double UpperLimit { get; set; }
        public double LowerLimit { get; set; }
    }

    public class NameValueClaims : List<NameValueClaim>
    { }

    public class NameValueClaim
    {
        public NameValueClaim()
        {
        }

        public NameValueClaim(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}