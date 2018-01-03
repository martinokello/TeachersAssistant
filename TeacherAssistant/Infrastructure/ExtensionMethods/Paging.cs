using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeachersAssistant.Domain.Model.Models;

namespace TeacherAssistant.Infrastructure
{
    public class Paging
    {
        public Paging(PagingInfo pagingInfo)
        {
            PagingInfoDetails = pagingInfo;
        }



        public PagingInfo PagingInfoDetails { get; set; }

        public int NextPage()
        {
            var tmpCurrentPage = PagingInfoDetails.CurrentPage;

            if (PagingInfoDetails.CurrentPageIndex < PagingInfoDetails.NumberOfPages - 1)
            {
                tmpCurrentPage++;
            }
            else
            {
                tmpCurrentPage = PagingInfoDetails.NumberOfPages;
            }
            return tmpCurrentPage;
        }

        public int PreviousPage()
        {
            var tmpCurrentPage = PagingInfoDetails.CurrentPage;

            if (PagingInfoDetails.CurrentPageIndex > 0)
            {
                tmpCurrentPage--;
            }
            else
            {
                tmpCurrentPage = 1;
            }
            return tmpCurrentPage;
        }


        public int NextSetPages()
        {
            var startPage = (StartDisplayFromPage() + 3);

            return startPage;
        }


        public int PreviousSetPages()
        {
            var startPage = (StartDisplayFromPage() - 1);

            return startPage;
        }
        public bool EnableNextPageLink()
        {
            if (PagingInfoDetails.CurrentPageIndex < PagingInfoDetails.NumberOfPages - 1) return true;
            return false;
        }

        public bool EnablePreviousPageLink()
        {
            if (PagingInfoDetails.CurrentPageIndex > 0) return true;
            return false;
        }

        public bool EnableNextSetPageLinks()
        {
            return PagingInfoDetails.NumberOfPages - StartDisplayFromPage() >=
                   PagingInfoDetails.NumberOfPagesToDisplay;
        }

        public bool EnablePreviousSetPageLinks()
        {
            return PagingInfoDetails.CurrentPage > PagingInfoDetails.NumberOfPagesToDisplay;
        }

        public int StartDisplayFromPage()
        {
            if (PagingInfoDetails.CurrentPageIndex > PagingInfoDetails.NumberOfPages)
                return PagingInfoDetails.NumberOfPages;

            var div = PagingInfoDetails.CurrentPage / PagingInfoDetails.NumberOfPagesToDisplay;
            var mod = PagingInfoDetails.CurrentPage % PagingInfoDetails.NumberOfPagesToDisplay;

            var startPage = 0;
            if (mod > 0)
                startPage = div * PagingInfoDetails.NumberOfPagesToDisplay + 1;
            else
                startPage = div * PagingInfoDetails.NumberOfPagesToDisplay -
                            (PagingInfoDetails.NumberOfPagesToDisplay - 1);
            if (startPage < 0) startPage = 1;
            return startPage;
        }
    }
}