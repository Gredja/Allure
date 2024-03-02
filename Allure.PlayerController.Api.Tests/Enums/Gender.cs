using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Allure.PlayerController.Api.Tests.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum Gender
{
    [EnumMember(Value = "male")]
    Male,

    [EnumMember(Value = "female")]
    Female
}