namespace MVCHamburgerciOtomasyonu.Service.Services.Abstract
{
    public interface IDashboardService
    {
        Task<List<int>> GetYearlyOrderCounts();
        Task<List<int>> GetYearlyUserOrderCounts();
        Task<int> GetTotalMenuCount();
        Task<int> GetTotalMenuSizeCount();
        Task<int> GetTotalExtraCount();
        Task<int> GetUserOrderCount();
        Task<int> GetUserOrderCompletedCount();
        Task<int> GetUserOrderCancelCount();
       

        //Task<int> GetTotalEducationCount();
        //Task<int> GetTotalAboutCount();
    }
}
