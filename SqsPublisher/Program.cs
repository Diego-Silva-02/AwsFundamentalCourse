using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using SqsPublisher;

var sqsClient = new AmazonSQSClient();

var customer = new CostumerCreated
{
    Id = Guid.NewGuid(),
    Email = "email@email.com",
    FullName = "FullName",
    DateOfBirth = new DateTime(2001, 8, 28),
    GitHubUserName = "GitHubUserName"
};

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(CostumerCreated)
            }
        }
    }
};

var response  = await sqsClient.SendMessageAsync(sendMessageRequest);

Console.WriteLine();