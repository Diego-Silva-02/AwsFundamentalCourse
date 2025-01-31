namespace SqsPublisher
{
    internal class CostumerCreated
    {
        public required Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required string GitHubUserName { get; set; }
        public required DateTime DateOfBirth { get; set; }
    }
}
