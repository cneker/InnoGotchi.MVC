﻿namespace InnoGotchi.MVC.RequestFeatures
{
    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviuos { get; set; }
        public bool HasNext { get; set; }
    }
}
