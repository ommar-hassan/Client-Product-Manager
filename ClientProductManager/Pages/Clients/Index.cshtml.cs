using Microsoft.AspNetCore.Mvc.RazorPages;
using ClientProductManager.Services;

namespace ClientProductManager.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly IClientService _clientService;

        public IndexModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IEnumerable<ClientViewModel> Clients { get;set; } = default!;
        public PaginationInfo Pagination { get; set; } = default!;

        public async Task OnGetAsync(int pageNumber = 1, int pageSize = 5)
        {
            var (clientViewModel, totalCount) = await _clientService.GetClientsAsync(pageNumber,pageSize);
            Clients = clientViewModel;
            Pagination = new PaginationInfo(pageNumber, pageSize, totalCount);
        }
    }
}
