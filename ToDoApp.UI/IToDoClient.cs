namespace ToDoApp.UI
{
    public interface IToDoClient
    {
       
        string BaseUrl { get; set; }
        bool ReadResponseAsString { get; set; }

        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ListToDoUsersViewModel> CreateAsync(CreateToDoUserCommand command);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ListToDoUsersViewModel> CreateAsync(CreateToDoUserCommand command, System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task IndexAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task IndexAsync(System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ListToDoItemsOfUserViewModel> ListAsync();

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ListToDoItemsOfUserViewModel> ListAsync(System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<CreateToDoItemViewModel> CreateAsync(CreateToDoItemCommand command);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<CreateToDoItemViewModel> CreateAsync(CreateToDoItemCommand command, System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task UpdateAsync(UpdateToDoItemCommand command);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task UpdateAsync(UpdateToDoItemCommand command, System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ListToDoUsersViewModel> ListAllUsersAsync(ListToDoUsersQuery query);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ListToDoUsersViewModel> ListAllUsersAsync(ListToDoUsersQuery query, System.Threading.CancellationToken cancellationToken);

        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ListToDoItemsViewModel> ListAllItemsAsync(ListToDoItemsQuery query);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<ListToDoItemsViewModel> ListAllItemsAsync(ListToDoItemsQuery query, System.Threading.CancellationToken cancellationToken);
    }
}