using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Allure.PlayerController.Api.Domain.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum Gender
{
    [EnumMember(Value = "male")]
    Male,

    [EnumMember(Value = "female")]
    Female
}