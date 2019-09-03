namespace Workflow.Domain.Configuration
{
    /// <summary>
    /// Starting point 
    /// </summary>
    public class DraftFactory
    {
        public Draft Create(string data, string author)
        {
            return new Draft(data, author);
        }
    }
}