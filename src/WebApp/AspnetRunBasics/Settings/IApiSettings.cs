namespace AspnetRunBasics.Settings
{
    public interface IApiSettings
    {
        public string BaseAddress { get; set; }
        public string CatalogPath { get; set; }
        public string BasketPath { get; set; }
        public string OrderPath { get; set; }
    }
}
