namespace CoreMvcTemplate.Configurations
{
    public static class RouteConfigurations
    {
        public static void ConfigureRoutes(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //Special Routes
            endpoints.MapControllerRoute(
                name: "page",
                pattern: "/blog/{title}",
                defaults: new { controller = "Blog", action = "Detail" });

            endpoints.MapControllerRoute(
                name: "urunDetay",
                pattern: "/urunler/{title}",
                defaults: new { controller = "Product", action = "Detail" });

            endpoints.MapControllerRoute(
                name: "urunListe",
                pattern: "/urunler",
                defaults: new { controller = "Product", action = "Index" });

            endpoints.MapControllerRoute(
                name: "Conteact",
                pattern: "/iletisim",
                defaults: new { controller = "StaticPages", action = "Contact" });

            endpoints.MapControllerRoute(
                name: "pdffile",
                pattern: "/dosyalar/{title}",
                defaults: new { controller = "PdfViewer", action = "Detail" });

            endpoints.MapControllerRoute(
                name: "yayinlar",
                pattern: "/yayinlar",
                defaults: new { controller = "PdfViewer", action = "Index" });

        }
    }
}
