namespace FinalExam_B14.Areas.Admin.ViewModels.Common
{
    public class PaginationVM<T>
    {
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }
        public List<T> Items { get; set; }
    }
}
