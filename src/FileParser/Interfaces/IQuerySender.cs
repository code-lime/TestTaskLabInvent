using System.Text.Json.Nodes;

namespace FileParser.Interfaces;

public interface IQuerySender
{
    void SendRawMessage(string message);
    void SendJsonNode(JsonNode node);
    void SendRawObject(object raw);
}
