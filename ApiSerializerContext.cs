using System.Collections.Generic;
using System.Text.Json.Serialization;
using Amazon.Lambda.APIGatewayEvents;

namespace Shared;

[JsonSerializable(typeof(APIGatewayProxyRequest))]
[JsonSerializable(typeof(APIGatewayProxyResponse))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(Dictionary<string, string>))]
public partial class ApiSerializerContext : JsonSerializerContext
{
}