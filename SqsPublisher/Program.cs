using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using SqsPublisher;

var sqsClient = new AmazonSQSClient();

var customer = new CostumerCreated
{
    Id = Guid.NewGuid(),
    Email = "diegosilva22a@gmail.com",
    FullName = "Diego Silva",
    DateOfBirth = new DateTime(2001, 8, 28),
    GitHubUserName = "Diego-Silva-02"
};

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessgeType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(CostumerCreated)
            }
        }
    }
};

var response  = await sqsClient.SendMessageAsync(sendMessageRequest);

Console.WriteLine();