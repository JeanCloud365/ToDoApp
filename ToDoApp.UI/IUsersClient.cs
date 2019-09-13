namespace ToDoApp.UI
{
    public interface IUsersClient
    {
        string BaseUrl { get; set; }
        bool ReadResponseAsString { get; set; }

        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ListToDoUsersViewModel> CreateAsync(CreateToDoUserCommand command);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ListToDoUsersViewModel> CreateAsync(CreateToDoUserCommand command, System.Threading.CancellationToken cancellationToken);
    }
}